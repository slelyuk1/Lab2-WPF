using System.Windows.Data;
using AgeZodiacCalculator.Model.Data;
using Shared.Converter.CustomValueConverter.Adapter;

namespace AgeZodiacCalculator.Converter.CustomValueConverter
{
    [ValueConversion(typeof(AgeInfo), typeof(string))]
    public class AgeInfoValueConverterAdapter : ValueConverterAdapter<AgeInfo>
    {
    }
}