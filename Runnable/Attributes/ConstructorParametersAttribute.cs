using System;

namespace Runnable.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ConstructorParametersAttribute : Attribute
    {
        public object[] Parameters { get; }

        public ConstructorParametersAttribute(params object[] parameters)
        {
            Parameters = parameters;
        }
    }
}
