using System;
using System.ComponentModel;
using AgeZodiacCalculator.Info;
using Shared.Tool;

namespace AgeZodiacCalculator.Model.Impl
{
    internal class ConverterBasedPickDateModel : ObservableItem, IPickDateModel
    {
        private readonly TypeConverter _chineseSignConverter;
        private readonly TypeConverter _westernSignConverter;

        public ConverterBasedPickDateModel(DateTime selectedTime, TypeConverter chineseSignConverter, TypeConverter westernSignConverter)
        {
            SelectedDate = selectedTime;
            _chineseSignConverter = chineseSignConverter;
            _westernSignConverter = westernSignConverter;
        }

        public DateTime SelectedDate { get; set; }

        public AgeInfo? Age
        {
            get
            {
                if (IsTimeInFutureDay(SelectedDate))
                {
                    // todo logging
                    return null;
                }

                TimeSpan span = DateTime.Now - SelectedDate;
                int daysDifference = span.Days;
                int years = daysDifference / 365;
                int months = (12 - SelectedDate.Month + DateTime.Now.Month) % 12;
                int days = Math.Abs(DateTime.Now.Day - SelectedDate.Day);

                return years <= 135 ? new AgeInfo(days, months, years) : null;
            }
        }

        // todo logging
        public ChineseSign? ChineseSign => IsTimeInFutureDay(SelectedDate) ? null : (ChineseSign) _chineseSignConverter.ConvertFrom(SelectedDate);

        // todo logging
        public WesternSign? WesternSign => IsTimeInFutureDay(SelectedDate) ? null : (WesternSign) _westernSignConverter.ConvertFrom(SelectedDate);

        private static bool IsTimeInFutureDay(DateTime time)
        {
            TimeSpan span = DateTime.Now - time;
            return span.Days <= 0;
        }
    }
}