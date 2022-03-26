using System;

namespace Runnable.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RunnableAttribute : Attribute
    {
        public string DisplayName { get; set; }
        public object[] MethodParameters { get; set; } = Array.Empty<object>();
        public object[] ConstructorParameters { get; set; } = Array.Empty<object>();

        public RunnableAttribute(string name)
        {
            DisplayName = name;
        }
    }
}