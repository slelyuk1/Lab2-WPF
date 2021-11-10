using System.Windows.Data;
using AgeZodiacCalculator.Info;

namespace AgeZodiacCalculator.Converter.CustomValueConverter.Adapter
{
    [ValueConversion(typeof(AgeInfo), typeof(string))]
    public class AgeInfoValueConverterAdapter : TypeToValueConverterAdapter<AgeInfo>
    {
    }

    [ValueConversion(typeof(ChineseSign), typeof(string))]
    public class ChineseSignValueConverterAdapter : TypeToValueConverterAdapter<ChineseSign>
    {
    }

    [ValueConversion(typeof(WesternSign), typeof(string))]
    public class WesternSignValueConverterAdapter : TypeToValueConverterAdapter<WesternSign>
    {
    }
}