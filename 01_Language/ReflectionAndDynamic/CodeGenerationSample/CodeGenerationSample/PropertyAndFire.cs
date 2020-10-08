using System;
using System.Runtime.CompilerServices;

namespace CodeGenerationSample
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class PropertyWithNotificationAttribute : Attribute
    {
        readonly string _name;
        readonly Type _type;

        // This is a positional argument
        public PropertyWithNotificationAttribute(string name, Type type)
        {
            _name = name;
            _type = type;
        }

        public string Name => _name;
        public Type Type => _type;

        // TODO: fire a list of changes supplied with attri
        public int NamedInt { get; set; }
    }

}
