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


        // todo maybe better algo
        // int day = BirthDate.Day;
        // int month = BirthDate.Month - 1;
        // var sign = (WesternSigns) month;
        // bool shift;

        // if (month == 1)
        // {
        // shift = day >= 20;
        // }
        // else if (month == 0 || (month >= 2 && month <= 5))
        // {
        // shift = day >= 21;
        // }
        // else if (month >= 6 && month <= 11)
        // {
        // shift = day >= 22;
        // }
        // else
        // {
        // throw new ArgumentException("Inappropriate format of month !");
        // }

        // return shift ? EnumHelper.GetDescription(sign + 1) : EnumHelper.GetDescription(sign);

        public WesternSign ConvertFromDate(DateTime time)
        {
            int day = time.Day;
            return time.Month switch
            {
                1 => day < 21 ? WesternSign.Capricorn : WesternSign.Aquarius,
                2 => day < 20 ? WesternSign.Aquarius : WesternSign.Pisces,
                3 => day < 21 ? WesternSign.Pisces : WesternSign.Aries,
                4 => day < 21 ? WesternSign.Aries : WesternSign.Taurus,
                5 => day < 21 ? WesternSign.Taurus : WesternSign.Gemini,
                6 => day < 21 ? WesternSign.Gemini : WesternSign.Cancer,
                7 => day < 22 ? WesternSign.Cancer : WesternSign.Leo,
                8 => day < 22 ? WesternSign.Leo : WesternSign.Virgo,
                9 => day < 22 ? WesternSign.Virgo : WesternSign.Libra,
                10 => day < 22 ? WesternSign.Libra : WesternSign.Scorpio,
                11 => day < 22 ? WesternSign.Scorpio : WesternSign.Sagittarius,
                12 => day < 22 ? WesternSign.Sagittarius : WesternSign.Capricorn,
                _ => throw new ArgumentException("Inappropriate format of month")
            };
        }

        public WesternSignConverter() : base(typeof(WesternSign))
        {
        }
    }
}