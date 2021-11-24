using System.Windows.Data;
using AgeZodiacCalculator.Converter.CustomValueConverter.Adapter;
using AgeZodiacCalculator.Info;

namespace Shared.Converter.CustomValueConverter.Adapter
{
    [ValueConversion(typeof(ChineseSign), typeof(string))]
    public class ChineseSignValueConverterAdapter : ValueConverterAdapter<ChineseSign>
    {
    }

    [ValueConversion(typeof(WesternSign), typeof(string))]
    public class WesternSignValueConverterAdapter : ValueConverterAdapter<WesternSign>
    {
    }
}