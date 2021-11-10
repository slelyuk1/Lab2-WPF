using System.ComponentModel;
using AgeZodiacCalculator.Converter.CustomTypeConverter;

namespace AgeZodiacCalculator.Info
{
    [TypeConverter(typeof(AgeInfoConverter))]
    public class AgeInfo
    {
        public int Days { get; }
        public int Months { get; }
        public int Years { get; }

        public AgeInfo(int days, int months, int years)
        {
            Days = days;
            Months = months;
            Years = years;
        }

        public override string ToString()
        {
            return $"AgeInfo: days={Days}; months={Months}; years={Years};";
        }
    }
}