using System;
using System.Linq;
using System.Reflection;
using Runnable.Attributes;
using Runnable.IO;

namespace Runnable
{
    public static class Runner
    {
        public static void Run()
        {
            Run(new ConsoleTerminal());
        }

        public static void Run<Terminal>() where Terminal : ITerminal
        {
            var terminal = (ITerminal)Activator.CreateInstance(typeof(Terminal));

            Run(terminal);
        }

        public static void Run(ITerminal terminal)
        {
            var runnables = GetRunnableMethods();

            if (runnables.Length == 0)
            {
                terminal.PrintMessage("No runnable methods found.", MessageType.Error);
                return;
            }

            for (int i = 0; i < runnables.Length; i++)
                terminal.PrintMessage($"{i + 1} - {runnables[i].Attribute.DisplayName}", MessageType.Text);

            if (terminal.ReadInt(1, runnables.Length, out int pos))
            {
                terminal.Clear();

                object result = RunMethod(runnables[pos - 1], terminal);

                if (result != null)
                    terminal.PrintMessage($"\"{runnables[pos - 1].Attribute.DisplayName}\" returned:\n{result}", MessageType.Text);
                else
                    terminal.PrintMessage($"\"{runnables[pos - 1].Attribute.DisplayName}\" returned no value.", MessageType.Text);
            }
        }

        private static RunnableMethod[] GetRunnableMethods()
        {
            var runnables = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                            from type in assembly.DefinedTypes
                            from method in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public  | BindingFlags.Static | BindingFlags.Instance)
                            from attribute in method.GetCustomAttributes<RunnableAttribute>()
                            select new RunnableMethod(attribute, method);

            return runnables.ToArray();
        }

        private static object RunMethod(RunnableMethod runnable, ITerminal terminal)
        {
            var method = runnable.Method;
            var constructorParametersAttribute = method.GetCustomAttribute<ConstructorParametersAttribute>();
            var methodParametersAttribute = method.GetCustomAttribute<MethodParametersAttribute>();

            var type = method.DeclaringType;

            object instance = null;

            if (!method.IsStatic && !(type is null))
            {
                var constructorParameters = constructorParametersAttribute?.Parameters ?? Array.Empty<object>();                    

                bool validConstructorParameters = type.GetConstructors()
                    .Any(constructor => ValidParameters(constructor.GetParameters(), constructorParameters));

                if (!validConstructorParameters)
                {
                    terminal.PrintMessage("Invalid constructor parameters.", MessageType.Error);
                    return null;
                }

                instance = Activator.CreateInstance(type, constructorParameters);
            }

            var methodParameters = methodParametersAttribute?.Parameters ?? Array.Empty<object>();

            if (!ValidParameters(method.GetParameters(), methodParameters))
            {
                terminal.PrintMessage("Invalid method parameters.", MessageType.Error);
                return null;
            }

            return method.Invoke(instance, methodParameters);
        }

        private static bool ValidParameters(ParameterInfo[] requiredParameters, object[] parameters)
        {
            if (requiredParameters.Length != parameters.Length)
                return false;

            for (int i = 0; i < requiredParameters.Length; i++)
                if (requiredParameters[i].ParameterType != parameters[i].GetType())
                    return false;

            return true;
        }
    }
}