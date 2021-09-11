using System;
using AgeZodiacCalculator.Info;

namespace AgeZodiacCalculator.Model
{
    internal interface IPickDateModel
    {
        public DateTime SelectedDate { get; set; }

        AgeInfo? CalculateAge();

        ChineseSign? CalculateChineseSign();

        WesternSign? CalculateWesternSign();
    }
}