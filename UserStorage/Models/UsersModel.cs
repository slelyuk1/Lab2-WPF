using System;

namespace UserStorage.Models
{
    public class UsersModel
    {
        public UsersModel(Storage data)
        {
            Data = data;
        }

        public Storage Data { get; }

        public bool FilterPredicate(PersonInfo user, string filter, string property)
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

        public PersonInfo ChosenPersonInfo
        {
            get => Data.ChosenUser;
            set => Data.ChosenUser = value;
        }

        public void AddUser(PersonInfo user)
        {
            Data.Users.Add(user);
        }

        public void EditUser(PersonInfo edited)
        {
            // todo make normal
            int foundIndex = -1;
            for (var i = 0; i < Data.Users.Count; ++i)
            {
                if (Data.ChosenUser == edited)
                {
                    foundIndex = i;
                    break;
                }
            }

            if (foundIndex == -1)
            {
                throw new NullReferenceException("No such user in storage to edit !");
            }

            Data.Users[foundIndex] = edited;
        }

        public void DeleteUser(PersonInfo user)
        {
            Data.Users.Remove(user);
        }

        public bool IsUserChosen => ChosenPersonInfo != null;
    }
}