using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using PeopleInfoStorage.Exception;
using Shared.Model.Data;

namespace PeopleInfoStorage.Model.Data
{
    [Serializable]
    public class PersonInfo
    {
        public static readonly TypeConverter ChineseSignConverter = TypeDescriptor.GetConverter(typeof(ChineseSign));
        public static readonly TypeConverter WesternSignConverter = TypeDescriptor.GetConverter(typeof(WesternSign));

        private readonly Guid _id;

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

        public class Builder
        {
            private string? _name, _surname, _email;
            private DateTime? _birthDate;

            public Builder WithName(string name)
            {
                if (!IsNameValid(name))
                {
                    throw new NameException(name);
                }

                _name = name;
                return this;
            }

            public Builder WithSurname(string surname)
            {
                if (!IsNameValid(surname))
                {
                    throw new NameException(surname);
                }

                _surname = surname;
                return this;
            }

            public Builder WithEmail(string email)
            {
                if (!IsEmailValid(email))
                {
                    throw new EmailException(email);
                }

                _email = email;
                return this;
            }

            public Builder WithBirthDate(DateTime birthDate)
            {
                if (!IsBirthDateValid(birthDate))
                {
                    throw new BirthDateException(birthDate);
                }

                _birthDate = birthDate;
                return this;
            }

            public PersonInfo Build()
            {
                return new PersonInfo(
                    _name ?? throw new NameException(_name),
                    _surname ?? throw new NameException(_surname),
                    _email ?? throw new EmailException(_email),
                    _birthDate ?? throw new BirthDateException(_birthDate)
                );
            }
        }

        private PersonInfo(string name, string surname, string email, DateTime birthDate)
        {
            _id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Email = email;
            BirthDate = birthDate;
        }

        public static bool IsNameValid(string name)
        {
            return name.Length != 0 && char.IsUpper(name[0]);
        }

        public static bool IsEmailValid(string email)
        {
            return Regex.IsMatch(email, "\\w+@\\w+\\.\\w+") && !email.Contains(' ');
        }

        public static bool IsBirthDateValid(DateTime birthDate)
        {
            int days = (birthDate - DateTime.Now).Days;
            return days < 0 || days / 365 > 135;
        }
    }
}