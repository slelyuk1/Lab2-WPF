using System;

namespace UserStorage.Models
{
    public class UserInputModel
    {
        public UserInputModel(Storage data)
        {
            Data = data;
        }

        public Storage Data { get; }

        public Person ChosenUser
        {
            get => Data.ChosenUser;
        }

        public bool IsBirthDay()
        {
            if (Data.ChosenUser == null)
                return false;
            return Data.ChosenUser.IsBirthday;
        }

        public void AddUser(string name, string surname, string email, DateTime birthday)
        {
            Data.AddUser((new Person(name, surname, email, birthday)));
        }

        public void EditUser(Person toEdit, string name, string surname, string email, DateTime birthday)
        {
            Data.EditUser(new Person(name, surname, email, birthday));
        }
    }
}