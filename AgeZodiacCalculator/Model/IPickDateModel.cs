using System;
using AgeZodiacCalculator.Info;

namespace AgeZodiacCalculator.Model
{
    internal interface IPickDateModel
    {
        public DateTime SelectedDate { get; set; }

        public AgeInfo? Age { get; }

        public ChineseSign? ChineseSign { get; }

        public WesternSign? WesternSign { get; }
    }
}