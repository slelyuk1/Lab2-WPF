using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using AgeZodiacCalculator.Info;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeMadeStatic.Global

namespace AgeZodiacCalculator.Converter
{
    public class AgeInfoConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
        }

        [SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
        public override object? ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is not AgeInfo ageValue)
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }

            if (typeof(string) == destinationType)
            {
                return InternalConvertToString(ageValue);
            }

            return base.ConvertTo(context, culture, ageValue, destinationType);
        }

        protected string InternalConvertToString(AgeInfo? value)
        {
            return value != null ? $"{value.Years} years, {value.Months} months, {value.Days} days" : "";
        }
    }
}