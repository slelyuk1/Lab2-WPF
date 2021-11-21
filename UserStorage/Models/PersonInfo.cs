using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using AgeZodiacCalculator.Info;
using UserStorage.Exception;

namespace UserStorage.Models
{
    // todo use builder pattern
    [Serializable]
    public class PersonInfo
    {
        private readonly Guid _id;

        public static PersonInfo From(string name, string surname, string email, DateTime birthDate)
        {
            ValidateInitializationParameters(name, surname, email, birthDate);
            return new PersonInfo(name, surname, email, birthDate);
        }

        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public DateTime BirthDate { get; }

        public bool IsAdult
        {
            get
            {
                TimeSpan span = DateTime.Now - BirthDate;
                return span.Days / 365 >= 18;
            }
        }

        public ChineseSign ChineseSign => (ChineseSign) TypeDescriptor.GetConverter(typeof(ChineseSign)).ConvertFrom(BirthDate);

        public WesternSign WesternSign => (WesternSign) TypeDescriptor.GetConverter(typeof(WesternSign)).ConvertFrom(BirthDate);

        public bool IsBirthday => DateTime.Now.Date == BirthDate.Date;

        protected bool Equals(PersonInfo other)
        {
            return _id.Equals(other._id);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((PersonInfo) obj);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        private PersonInfo(string name, string surname, string email, DateTime birthDate)
        {
            _id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Email = email;
            BirthDate = birthDate;
        }

        private static void ValidateInitializationParameters(string name, string surname, string email, DateTime birthDate)
        {
            if (IsNameValid(name))
            {
                throw new NameException(name);
            }

            if (IsNameValid(surname))
            {
                throw new NameException(surname);
            }

            if (!IsEmailValid(email))
            {
                throw new EmailException(email);
            }

            if (IsBirthDateValid(birthDate))
            {
                throw new BirthDateException(birthDate);
            }
        }

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