using System;
using System.ComponentModel;
using System.Globalization;
using AgeZodiacCalculator.Info;

// ReSharper disable MemberCanBeMadeStatic.Global
// ReSharper disable MemberCanBePrivate.Global

namespace AgeZodiacCalculator.Converter.CustomTypeConverter
{
    internal class ChineseSignConverter : EnumConverter
    {
        private static readonly ChineseSign[] ChineseSignsInChronology =
        {
            ChineseSign.Monkey, ChineseSign.Rooster, ChineseSign.Dog, ChineseSign.Pig, ChineseSign.Rat, ChineseSign.Ox,
            ChineseSign.Tiger, ChineseSign.Rabbit, ChineseSign.Dragon, ChineseSign.Snake, ChineseSign.Horse,
            ChineseSign.Sheep
        };

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return typeof(DateTime) == sourceType || sourceType.IsSubclassOf(typeof(DateTime)) || base.CanConvertFrom(context, sourceType);
        }


        public override object? ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is DateTime dateValue)
            {
                return ConvertFromDate(dateValue);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public ChineseSign? ConvertFromDate(DateTime time)
        {
            int index = time.Year % 12;
            if (index < ChineseSignsInChronology.Length)
            {
                return ChineseSignsInChronology[time.Year % 12];
            }

            return null;
        }

        public ChineseSignConverter() : base(typeof(ChineseSign))
        {
        }
    }
}