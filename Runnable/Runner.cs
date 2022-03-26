using System;
using System.Linq;
using System.Reflection;
using Runnable.Attributes;

namespace Runnable
{
    public static class Runner
    {
        public static void Run()
        {
            if (!Environment.UserInteractive || Console.LargestWindowWidth == 0)
                throw new NotSupportedException();

            var runnables = GetRunnableMethods();

            for (int i = 0; i < runnables.Length; i++)
                Console.WriteLine($"{i} - {runnables[i].Attribute.DisplayName}");

            while (true)
            {
                var keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape)
                    break;

                if (int.TryParse(keyInfo.KeyChar.ToString(), out int index))
                {
                    if (index >= 0 && index < runnables.Length)
                    {
                        Console.Clear();
                        RunMethod(runnables[index]);
                        break;
                    }
                }
            }
        }

        private static void RunMethod(RunnableMethod runnable)
        {
            var method = runnable.Method;
            var attribute = runnable.Attribute;

            var type = method.DeclaringType;

            object instance = null;

            if (!method.IsStatic && !(type is null))
            {
                var constructorParameters = attribute.ConstructorParameters;

                bool validConstructorParameters = type.GetConstructors()
                    .All(info => AreValidParameters(info.GetParameters(), constructorParameters));

                if (!validConstructorParameters)
                {
                    PrintError("Invalid constructor parameters.");
                    return;
                }

                instance = Activator.CreateInstance(type, attribute.ConstructorParameters);
            }

            var methodParameters = attribute.MethodParameters;

            if (!AreValidParameters(method.GetParameters(), methodParameters))
            {
                PrintError("Invalid method parameters.");
                return;
            }

            method.Invoke(instance, attribute.MethodParameters);
        }

        private static bool AreValidParameters(ParameterInfo[] requiredParameters, object[] parameters)
        {
            if (requiredParameters.Length != parameters.Length)
                return false;

            for (int i = 0; i < requiredParameters.Length; i++)
                if (requiredParameters[i].ParameterType != parameters[i].GetType())
                    return false;

            return true;
        }

        private static RunnableMethod[] GetRunnableMethods()
        {
            var runnables = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                            from type in assembly.DefinedTypes
                            from method in type.GetMethods()
                            from attribute in method.GetCustomAttributes<RunnableAttribute>()
                            select new RunnableMethod(attribute, method);

            return runnables.ToArray();
        }

        private static void PrintError(string msg)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = originalColor;
        }
    }
}