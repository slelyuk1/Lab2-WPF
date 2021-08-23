using System;
using System.Linq;
using System.Text.RegularExpressions;
using UserStorage.Tools;

namespace UserStorage.Models
{
    [Serializable]
    public class Person
    {
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthDate = DateTime.Now;

        public Person(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
        }

        public Person(string name, string surname, string email, DateTime birthDate) : this(name, surname, email)
        {
            BirthDate = birthDate;
        }

        public Person(string name, string surname, DateTime birthDate)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
        }

        public string Name
        {
            get => _name;
            private set
            {
                if (value.Length == 0 || char.IsLower(value[0]))
                {
                    throw new NameException();
                }

                _name = value;
            }
        }

        public string Surname
        {
            get => _surname;
            private set
            {
                if (value.Length == 0 || char.IsLower(value[0]))
                {
                    throw new SurnameException();
                }

                _surname = value;
            }
        }

        public string Email
        {
            get => _email;
            private set
            {
                if (!Regex.IsMatch(value, "\\w+@\\w+\\.\\w+") || value.Contains(' '))
                {
                    throw new EmailException();
                }

                _email = value;
            }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            private set
            {
                var days = (DateTime.Now - value).Days;
                if (days <= 0 || days / 365 > 135)
                {
                    throw new AgeException();
                }

                _birthDate = value;
                SunSign = CalculateSunSign(_birthDate);
                ChineseSign = CalculateChineseSign(_birthDate);
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

        public string SunSign { get; private set; }

        public string ChineseSign { get; private set; }

        public bool IsBirthday
        {
            get => _birthDate.Day == DateTime.Now.Day && _birthDate.Month == DateTime.Now.Month;
        }

        public static string CalculateSunSign(DateTime date)
        {
            var day = date.Day;
            var month = date.Month - 1;
            var sign = (WesternSigns) month;
            bool shift;

            if (month == 1)
            {
                shift = day >= 20;
            }
            else if (month == 0 || (month >= 2 && month <= 5))
            {
                shift = day >= 21;
            }
            else if (month >= 6 && month <= 11)
            {
                shift = day >= 22;
            }
            else
            {
                throw new ArgumentException("Inappropriate format of month !");
            }

            return shift ? EnumHelper.GetDescription(sign + 1) : EnumHelper.GetDescription(sign);
        }

        public static string CalculateChineseSign(DateTime date)
        {
            return EnumHelper.GetDescription((ChineseSigns) (date.Year % 12));
        }
    }
}