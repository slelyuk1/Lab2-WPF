using System;
using System.ComponentModel;
using System.Globalization;
using Shared.Model.Data;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeMadeStatic.Global

namespace Shared.Converter.CustomTypeConverter
{
    public class WesternSignConverter : EnumConverter
    {
        private static readonly WesternSign[] WesternSignsInChronology =
        {
            WesternSign.Capricorn, WesternSign.Aquarius, WesternSign.Pisces,
            WesternSign.Aries, WesternSign.Taurus, WesternSign.Gemini,
            WesternSign.Cancer, WesternSign.Leo, WesternSign.Virgo,
            WesternSign.Libra, WesternSign.Scorpio, WesternSign.Sagittarius
        };

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(DateTime) || sourceType.IsSubclassOf(typeof(DateTime)) || base.CanConvertFrom(context, sourceType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is DateTime dateValue)
            {
                return ConvertFromDate(dateValue);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public WesternSign ConvertFromDate(DateTime time)
        {
            int day = time.Day;
            int month = time.Month - 1;
            int borderDay;

            switch (month)
            {
                case 1:
                    borderDay = 20;
                    break;
                case 0:
                case >= 2 and <= 5:
                    borderDay = 21;
                    break;
                case >= 6 and <= 11:
                    borderDay = 22;
                    break;
                default:
                    throw new InvalidOperationException($"Month number is not expected: {month}");
            }

            bool shift = day >= borderDay;
            int signIndex = (shift ? month + 1 : month) % 12;
            return WesternSignsInChronology[signIndex];
        }

        public WesternSignConverter() : base(typeof(WesternSign))
        {
        }
    }
}