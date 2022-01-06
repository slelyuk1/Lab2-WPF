using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using PeopleInfoStorage.Exception;
using Shared.Model.Data;

namespace PeopleInfoStorage.Model.Data
{
    // todo use builder pattern
    [Serializable]
    public class PersonInfo
    {
        public static readonly TypeConverter ChineseSignConverter = TypeDescriptor.GetConverter(typeof(ChineseSign));
        public static readonly TypeConverter WesternSignConverter = TypeDescriptor.GetConverter(typeof(WesternSign));

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

        public ChineseSign ChineseSign => (ChineseSign) ChineseSignConverter.ConvertFrom(BirthDate);

        public WesternSign WesternSign => (WesternSign) WesternSignConverter.ConvertFrom(BirthDate);

        public bool IsBirthday => DateTime.Now.Date == BirthDate.Date;

        public bool IsAdult
        {
            get
            {
                TimeSpan span = DateTime.Now - BirthDate;
                return span.Days / 365 >= 18;
            }
        }

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

        public override string ToString()
        {
            return $"PersonInfo: Name={Name}, Surname={Surname}, Email={Email}, BirthDate={BirthDate}";
        }

        private PersonInfo(string name, string surname, string email, DateTime birthDate)
        {
            _id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Email = email;
            BirthDate = birthDate;
        }

        private static void ValidateInitializationParameters(string name, string surname, string email,
            DateTime birthDate)
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
            int days = (birthDate - DateTime.Now).Days;
            return days > 0 && days / 365 <= 135;
        }
    }
}