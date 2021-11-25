using System.Windows.Data;
using Shared.Model.Data;

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