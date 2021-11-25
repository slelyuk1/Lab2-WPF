using System.ComponentModel;
using Shared.Converter.CustomTypeConverter;

namespace Shared.Model.Data
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