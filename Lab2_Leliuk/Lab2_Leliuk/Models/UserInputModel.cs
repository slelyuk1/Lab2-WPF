using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lab2_Leliuk.Models
{
    class UserInputModel
    {
        private Storage _data;

        public UserInputModel(Storage data)
        {
            _data = data;
        }

        public Storage Data => _data;

        public bool IsNameValid(string name)
        {
            return name.Length > 0 && char.IsUpper(name[0]);
        }

        public bool IsSurnameValid(string surname)
        {
            return surname.Length > 0 && char.IsUpper(surname[0]);
        }

        public bool IsEmailValid(string email)
        {
            if (Regex.IsMatch(email, "\\w+@\\w+\\.\\w+") && !email.Contains(' '))
                return true;
            return false;
        }

        public bool IsDateValid(DateTime birthday)
        {
            var days = (DateTime.Now - birthday).Days;
            return days > 0 && days / 365 <= 135;
        }

        public bool IsBirthDay()
        {
            if (Data.User == null)
                return false;
            return Data.User.IsBirthDay;
        }

        public void SetUser(string name, string surname, string email, DateTime birthday)
        {
            _data.ChangeUser(new Person(name, surname, email, birthday));
        }
    }
}