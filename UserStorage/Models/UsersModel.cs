using System;
using System.Globalization;
using System.Windows;

namespace UserStorage.Models
{
    class UsersModel
    {
        public UsersModel(Storage data)
        {
            Data = data;
        }

        public Storage Data { get; }

        public bool FilterPredicate(Person user, string filter, string property)
        {
            filter = filter.ToLower();
            if (property == "All")
            {
                var name = user.Name.ToLower();
                var surname = user.Surname.ToLower();
                var email = user.Email.ToLower();
                var sunSign = user.SunSign.ToLower();
                var chineseSign = user.ChineseSign.ToLower();
                return name.Contains(filter) || surname.Contains(filter) || email.Contains(filter) ||
                       email.Contains(filter) || sunSign.Contains(filter) || chineseSign.Contains(filter);
            }

            var userProperty = user.GetType().GetProperty(property);
            if (userProperty == null)
                throw new ArgumentException("Inappropriate property for user !");
            var propertyVal = userProperty.GetValue(user, null);

            if (propertyVal is string s)
            {
                return s.ToLower().Contains(filter);
            }

            throw new ArgumentException("Inappropriate property type for filtering");
        }

        public Person ChosenPerson
        {
            get => Data.ChosenUser;
            set => Data.ChosenUser = value;
        }

        public void AddUser(Person user)
        {
            Data.Users.AddLast(user);
        }

        public void EditUser(Person edited)
        {
            var node = Data.Users.Find(Data.ChosenUser);
            if (node == null)
                throw new NullReferenceException("No such user in storage to edit !");
            node.Value = edited;
        }

        public void DeleteUser(Person user)
        {
            Data.Users.Remove(user);
        }

        public bool IsUserChosen => ChosenPerson != null;
    }
}