using System;

namespace Lab2_Leliuk.Models
{
    public class Person
    {
        enum ChineseSigns
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

        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthDate = DateTime.Now;
        private string _chineseSign, _sunSign;

        public Person(string name, string surname, string email, DateTime birthDate)
        {
            _name = name;
            _surname = surname;
            _email = email;
            _birthDate = birthDate;
            _chineseSign = CalculateChineseSign(_birthDate);
            _sunSign = CalculateSunSign(_birthDate);
        }

        public Person(string name, string surname, string email)
        {
            _name = name;
            _surname = surname;
            _email = email;
        }

        public Person(string name, string surname, DateTime birthDate)
        {
            _name = name;
            _surname = surname;
            _birthDate = birthDate;
            _chineseSign = CalculateChineseSign(_birthDate);
            _sunSign = CalculateSunSign(_birthDate);
        }

        public string Name
        {
            get => _name;
        }

        public string Surname
        {
            get => _surname;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                _sunSign = CalculateSunSign(_birthDate);
                _chineseSign = CalculateChineseSign(_birthDate);
            }
        }


        public bool IsAdult
        {
            get
            {
                var span = DateTime.Now - _birthDate;
                return span.Days / 365 >= 18;
            }
        }

        public string SunSign
        {
            get => _sunSign;
        }

        public string ChineseSign
        {
            get => _chineseSign;
        }

        public bool IsBirthDay
        {
            get => _birthDate.Day == DateTime.Now.Day && _birthDate.Month == DateTime.Now.Month;
        }

        public static string CalculateSunSign(DateTime date)
        {
            var day = date.Day;
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

        public static string CalculateChineseSign(DateTime date)
        {
            return ((ChineseSigns)(date.Year % 12)).ToString();
        }
    }
}