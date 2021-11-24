using System.Windows.Data;
using AgeZodiacCalculator.Info;

namespace AgeZodiacCalculator.Converter.CustomValueConverter.Adapter
{
    [ValueConversion(typeof(AgeInfo), typeof(string))]
    public class AgeInfoValueConverterAdapter : ValueConverterAdapter<AgeInfo>
    {
    }
}