using System;
using System.Reflection;

using Framework.Attributes;

namespace Framework.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get the string of a enum type.
        /// </summary>
        /// <param name="value">The enum that you want the string from.</param>
        /// <returns>The StringValueAttribute, if not existing returns empty string.</returns>
        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            StringValueAttribute attribute = (StringValueAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(StringValueAttribute));

            return attribute?.Value ?? string.Empty;
        }
        
        /// <summary>
        /// Get the char of a enum type.
        /// </summary>
        /// <param name="value">The enum that you want the char from.</param>
        /// <returns>The CharValueAttribute, if not existing returns empty char ('\0').</returns>
        public static char GetCharValue(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());
            CharValueAttribute attribute = (CharValueAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(CharValueAttribute));

            return attribute?.Value ?? '\0'; // '\0' is a default value if the attribute is not found
        }
    }
}
