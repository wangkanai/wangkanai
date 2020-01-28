using System;

namespace Wangkanai.Detection.UI
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class ResponsiveDefaultUIAttribute : Attribute
    {
        public Type Template { get; }

        public ResponsiveDefaultUIAttribute(Type implementationTemplate)
        {
            Template = implementationTemplate;
        }
    }
}