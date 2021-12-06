using System;
using System.ComponentModel;
using AgeZodiacCalculator.Model.Data;
using Microsoft.Extensions.Logging;
using Shared.Model.Data;
using Shared.Tool.ViewModel;

namespace AgeZodiacCalculator.Model.UI.Impl
{
    internal class ConverterBasedPickDateModel : ObservableItem, IPickDateModel
    {
        private readonly ILogger<ConverterBasedPickDateModel> _logger;

        private readonly TypeConverter _chineseSignConverter;
        private readonly TypeConverter _westernSignConverter;

        private DateTime _selectedDate;

        public ConverterBasedPickDateModel(ILogger<ConverterBasedPickDateModel> logger, DateTime? selectedTime = null)
        {
            _logger = logger;
            SelectedDate = selectedTime ?? DateTime.Now;
            _chineseSignConverter = TypeDescriptor.GetConverter(typeof(ChineseSign));
            _westernSignConverter = TypeDescriptor.GetConverter(typeof(WesternSign));
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (!IsTimeValid(value))
                {
                    _logger.LogWarning("Invalid SelectedDate (in future or >135 years in past) was set: {SelectedDate}", value);
                }

                _selectedDate = value;
            }
        }

        public AgeInfo? Age
        {
            get
            {
                if (!IsTimeValid(SelectedDate))
                {
                    return null;
                }

                TimeSpan span = DateTime.Now - SelectedDate;
                int daysDifference = span.Days;
                int years = Math.Abs(daysDifference / 365);
                int months = (12 - SelectedDate.Month + DateTime.Now.Month) % 12;
                int days = Math.Abs(DateTime.Now.Day - SelectedDate.Day);

                return new AgeInfo(days, months, years);
            }
        }

        public ChineseSign? ChineseSign => IsTimeValid(SelectedDate) ? (ChineseSign) _chineseSignConverter.ConvertFrom(SelectedDate) : null;

        public WesternSign? WesternSign => IsTimeValid(SelectedDate) ? (WesternSign) _westernSignConverter.ConvertFrom(SelectedDate) : null;

        private static bool IsTimeValid(DateTime date)
        {
            TimeSpan span = DateTime.Now - date;
            int daysDifference = span.Days;
            if (daysDifference < 0)
            {
                return false;
            }

            int years = daysDifference / 365;
            return years < 135;
        }
    }
}