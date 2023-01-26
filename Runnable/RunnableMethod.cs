using System.Reflection;
using Runnable.Attributes;

namespace Runnable
{
    internal class RunnableMethod
    {
        public MethodInfo Method { get; }
        public RunnableAttribute RunnableAttribute { get; }
        public ConstructorParametersAttribute ConstructorParametersAttribute { get; }
        public MethodParametersAttribute MethodParametersAttribute { get; }

        public RunnableMethod(
            MethodInfo method,
            RunnableAttribute runnableAttribute,
            ConstructorParametersAttribute constructorParametersAttribute,
            MethodParametersAttribute methodParametersAttribute)
        {
            Method = method;
            RunnableAttribute = runnableAttribute;
            ConstructorParametersAttribute = constructorParametersAttribute;
            MethodParametersAttribute = methodParametersAttribute;
        }
    }
}