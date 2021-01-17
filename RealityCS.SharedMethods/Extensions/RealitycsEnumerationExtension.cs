using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace RealityCS.SharedMethods.Extensions
{
    public static class RealitycsEnumerationExtension
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if(name!=null)
            {
                FieldInfo field = type.GetField(name);
                if(field!=null)
                {
                    if(Attribute.GetCustomAttribute(field,typeof(DescriptionAttribute)) is DescriptionAttribute attr)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        public static bool ValidateEnumValue<TEnum>(this int enumValue)
        {
            return Enum.IsDefined(typeof(TEnum), enumValue);
        }
    }
}
