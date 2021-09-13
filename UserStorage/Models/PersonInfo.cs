using System;
using System.Linq;
using System.Text.RegularExpressions;
using UserStorage.Exception;
using UserStorage.Tool;

namespace UserStorage.Models
{
    [Serializable]
    public class PersonInfo
    {
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthDate;

        public PersonInfo(string name, string surname, string email, DateTime birthDate)
        {
            Name = name;
            Surname = surname;
            Email = email;
            BirthDate = birthDate;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (IsNameValid(value))
                {
                    throw new NameException(value);
                }

                _name = value;
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                if (IsNameValid(value))
                {
                    throw new NameException(value);
                }

                _surname = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (!IsEmailValid(value))
                {
                    throw new EmailException(value);
                }

                _email = value;
            }
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                if (IsBirthDateValid(value))
                {
                    throw new BirthDateException(value);
                }

                _birthDate = value;
            }
        }

        public bool IsAdult
        {
            get
            {
                TimeSpan span = DateTime.Now - _birthDate;
                return span.Days / 365 >= 18;
            }
        }

        // todo make normal
        public string SunSign => EnumHelper.GetDescription((ChineseSigns) (_birthDate.Year % 12));

        public string ChineseSign
        {
            get
            {
                int day = _birthDate.Day;
                int month = _birthDate.Month - 1;
                // todo redo
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
        }

        public bool IsBirthday => _birthDate.Day == DateTime.Now.Day && _birthDate.Month == DateTime.Now.Month;

        private static bool IsNameValid(string name)
        {
            return name.Length != 0 && char.IsLower(name[0]);
        }

        private static bool IsEmailValid(string email)
        {
            return Regex.IsMatch(email, "\\w+@\\w+\\.\\w+") && !email.Contains(' ');
        }

        private static bool IsBirthDateValid(DateTime birthDate)
        {
            // todo maybe improve logic
            int days = (DateTime.Now - birthDate).Days;
            return days > 0 && days / 365 <= 135;
        }
    }
}