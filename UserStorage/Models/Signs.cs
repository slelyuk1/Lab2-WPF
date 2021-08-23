using System.ComponentModel;

namespace UserStorage.Models
{
    public enum ChineseSigns
    {
        [Description("Monkey")]
        Monkey,
        [Description("Rooster")]
        Rooster,
        [Description("Dog")]
        Dog,
        [Description("Pig")]
        Pig,
        [Description("Rat")]
        Rat,
        [Description("Ox")]
        Ox,
        [Description("Tiger")]
        Tiger,
        [Description("Rabbit")]
        Rabbit,
        [Description("Dragon")]
        Dragon,
        [Description("Snake")]
        Snake,
        [Description("Horse")]
        Horse,
        [Description("Sheep")]
        Sheep
    }

    public enum WesternSigns
    {
        [Description("Capricorn")]
        Capricorn,
        [Description("Aquarius")]
        Aquarius,
        [Description("Pisces")]
        Pisces,
        [Description("Aries")]
        Aries,
        [Description("Taurus")]
        Taurus,
        [Description("Gemini")]
        Gemini,
        [Description("Cancer")]
        Cancer,
        [Description("Leo")]
        Leo,
        [Description("Virgo")]
        Virgo,
        [Description("Libra")]
        Libra,
        [Description("Scorpio")]
        Scorpio,
        [Description("Saggitarius")]
        Saggitarius,
    }

}