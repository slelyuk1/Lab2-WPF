using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace UserStorage.Tool
{
    public static class EnumHelper
    {
        public static string GetDescription(Enum value)
        {
            MemberInfo? enumMember = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            DescriptionAttribute? descriptionAttribute = enumMember == null
                ? default
                : enumMember.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            return descriptionAttribute == null ? value.ToString() : descriptionAttribute.Description;
        }
    }
}