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

        private DateTime _selectedDate;

        public ConverterBasedPickDateModel(ILogger<ConverterBasedPickDateModel> logger, DateTime? selectedTime = null)
        {
            _logger = logger;
            SelectedDate = selectedTime ?? DateTime.Now;
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

        public ChineseSign? ChineseSign => ConvertDateToEnum<ChineseSign>();

        public WesternSign? WesternSign => ConvertDateToEnum<WesternSign>();

        private T? ConvertDateToEnum<T>()
        {
            if (!IsTimeValid(SelectedDate))
            {
                return default;
            }

            Type expectedType = typeof(T);
            TypeConverter converter = TypeDescriptor.GetConverter(expectedType);
            Type fromType = SelectedDate.GetType();
            if (!converter.CanConvertFrom(fromType))
            {
                _logger.LogWarning("Converter {Converter} cannot convert from {FromType}", converter, fromType);
                return default;
            }

            object? converted = converter.ConvertFrom(SelectedDate);
            if (converted is not T sign)
            {
                _logger.LogWarning("Converted result is not of expected type {ExpectedType}: {Converted}", expectedType, fromType);
                return default;
            }

            return sign;
        }

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