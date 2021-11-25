using System;
using AgeZodiacCalculator.Model.Data;
using Shared.Model.Data;

namespace AgeZodiacCalculator.Model.UI
{
    internal interface IPickDateModel
    {
        public DateTime SelectedDate { get; set; }

        public AgeInfo? Age { get; }

        public ChineseSign? ChineseSign { get; }

        public WesternSign? WesternSign { get; }
    }
}