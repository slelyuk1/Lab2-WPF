using System;
using System.ComponentModel;
using AgeZodiacCalculator.Info;

namespace AgeZodiacCalculator.Model.Impl
{
    internal class ConverterBasedPickDateModel : IPickDateModel
    {
        public DateTime SelectedDate { get; set; }
        private readonly TypeConverter _chineseSignConverter;
        private readonly TypeConverter _westernSignConverter;

        public ConverterBasedPickDateModel(DateTime selectedTime, TypeConverter chineseSignConverter, TypeConverter westernSignConverter)
        {
            SelectedDate = selectedTime;
            _chineseSignConverter = chineseSignConverter;
            _westernSignConverter = westernSignConverter;
        }

        public AgeInfo? CalculateAge()
        {
            TimeSpan timeSpan = DateTime.Now - SelectedDate;
            int substr = timeSpan.Days;
            if (substr <= 0)
            {
                // todo logging
                return null;
            }

            int years = substr / 365;
            int months = (12 - SelectedDate.Month + DateTime.Now.Month) % 12;
            int days = Math.Abs(DateTime.Now.Day - SelectedDate.Day);

            if (years > 135)
            {
                // todo logging
                return null;
            }

            return new AgeInfo(days, months, years);
        }

        public ChineseSign? CalculateChineseSign()
        {
            // todo logging
            return (ChineseSign) _chineseSignConverter.ConvertFrom(SelectedDate);
        }

        public WesternSign? CalculateWesternSign()
        {
            // todo logging
            return (WesternSign) _westernSignConverter.ConvertFrom(SelectedDate);
        }
    }
}