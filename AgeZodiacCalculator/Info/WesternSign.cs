using System.ComponentModel;
using AgeZodiacCalculator.Converter;

namespace AgeZodiacCalculator.Info
{
    [TypeConverter(typeof(WesternSignConverter))]
    public enum WesternSign
    {
        Capricorn,
        Aquarius,
        Pisces,
        Aries,
        Taurus,
        Gemini,
        Cancer,
        Leo,
        Virgo,
        Libra,
        Scorpio,
        Sagittarius
    }
}