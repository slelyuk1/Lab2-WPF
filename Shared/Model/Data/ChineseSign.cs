using System.ComponentModel;
using Shared.Converter.CustomTypeConverter;

namespace Shared.Model.Data
{
    [TypeConverter(typeof(ChineseSignConverter))]
    public enum ChineseSign
    {
        Monkey,
        Rooster,
        Dog,
        Pig,
        Rat,
        Ox,
        Tiger,
        Rabbit,
        Dragon,
        Snake,
        Horse,
        Sheep
    }
}