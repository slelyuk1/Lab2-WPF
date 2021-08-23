using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AgeZodiacCalculator.Models
{
    public class PickDataModel
    {
        private static readonly string[] _chinezeSigns =
            {"Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Sheep"};

        public PickDataModel()
        {
        }


        public string CalculateAge(DateTime date)
        {
            TimeSpan timeSpan = DateTime.Now - date;
            int substr = timeSpan.Days;
            if (substr <= 0)
                throw new ArgumentException();
            int years = substr / 365;
            int months = (12 - date.Month + DateTime.Now.Month) % 12;
            int days = Math.Abs(DateTime.Now.Day - date.Day);
            if (years > 135)
                throw new ArgumentException();

            return days + " days, " + months + " months, " + years + " years";
        }

        public string CalculateChineseSign(DateTime date)
        {
            return _chinezeSigns[date.Year % 12];
        }

        public string CalculateWesternSign(DateTime date)
        {
            int day = date.Day;
            switch (date.Month)
            {
                case 1:
                    return day < 21 ? "Capricorn" : "Aquarius";
                case 2:
                    return day < 20 ? "Aquarius" : "Pisces";
                case 3:
                    return day < 21 ? "Pisces" : "Aries";
                case 4:
                    return day < 21 ? "Aries" : "Taurus";
                case 5:
                    return day < 21 ? "Taurus" : "Gemini";
                case 6:
                    return day < 21 ? "Gemini" : "Cancer";
                case 7:
                    return day < 22 ? "Cancer" : "Leo";
                case 8:
                    return day < 22 ? "Leo" : "Virgo";
                case 9:
                    return day < 22 ? "Virgo" : "Libra";
                case 10:
                    return day < 22 ? "Libra" : "Scorpio";
                case 11:
                    return day < 22 ? "Scorpio" : "Sagittarius";
                case 12:
                    return day < 22 ? "Sagittarius" : "Capricorn";
                default:
                    throw new ArgumentException("Inappropriate format of month !");
            }
        }
    }
}