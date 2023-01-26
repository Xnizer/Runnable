using System;

namespace Runnable.Attributes
{
    public class MethodParametersAttribute : Attribute
    {
        public object[] Parameters { get; }

        public MethodParametersAttribute(params object[] parameters)
        {
            Parameters = parameters;
        }
    }
}
