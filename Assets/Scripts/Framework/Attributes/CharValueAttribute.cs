using System;

namespace Framework.Attributes
{
    public sealed class CharValueAttribute : Attribute
    {
        public char Value { get; }

        public CharValueAttribute(char value) => Value = value;
    }
}