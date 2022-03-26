using System.Reflection;
using Runnable.Attributes;

namespace Runnable
{
    internal class RunnableMethod
    {
        public RunnableAttribute Attribute { get; }
        public MethodInfo Method { get; }

        public RunnableMethod(RunnableAttribute attribute, MethodInfo method)
        {
            Attribute = attribute;
            Method = method;
        }
    }
}