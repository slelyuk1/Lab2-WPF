using System.ComponentModel;
using AgeZodiacCalculator.Converter;

namespace AgeZodiacCalculator.Info
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