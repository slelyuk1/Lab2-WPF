using System;
using System.ComponentModel;
using AgeZodiacCalculator.Model.Data;
using Shared.Model.Data;
using Shared.Tool.ViewModel;

namespace AgeZodiacCalculator.Model.UI.Impl
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

        // todo conversion with normal error
        public ChineseSign? ChineseSign => IsTimeInFutureDay(SelectedDate) ? null : (ChineseSign) _chineseSignConverter.ConvertFrom(SelectedDate);

        public WesternSign? WesternSign => IsTimeInFutureDay(SelectedDate) ? null : (WesternSign) _westernSignConverter.ConvertFrom(SelectedDate);

        private static bool IsTimeInFutureDay(DateTime time)
        {
            TimeSpan span = DateTime.Now - time;
            return span.Days <= 0;
        }
    }
}