using System;

namespace Runnable.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RunnableAttribute : Attribute
    {
        public string DisplayName { get; }

        public RunnableAttribute(string name)
        {
            DisplayName = name;
        }
    }
}