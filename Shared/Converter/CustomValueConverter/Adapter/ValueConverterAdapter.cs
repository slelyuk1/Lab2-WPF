using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace Shared.Converter.CustomValueConverter.Adapter
{
    public class ValueConverterAdapter<T> : IValueConverter
    {
        private const string UnknownStringValue = "Unknown";
        private readonly Type _converterType;

        public ValueConverterAdapter()
        {
            _converterType = typeof(T);
        }

        public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(_converterType);
            if (!converter.CanConvertTo(targetType))
            {
                throw new InvalidOperationException($"Converter from type {_converterType} cannot convert to type: {targetType}");
            }

            if (value != null)
            {
                return converter.ConvertTo(null, culture, value, targetType) ?? UnknownStringValue;
            }

            return UnknownStringValue;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(_converterType);
            if (!converter.CanConvertFrom(value.GetType()))
            {
                throw new InvalidOperationException($"Converter from type {_converterType} cannot convert from type: {targetType}");
            }

            return converter.ConvertFrom(value);
        }
    }
}