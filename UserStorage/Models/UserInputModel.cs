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

        public PersonInfo ChosenUser
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
            Data.AddUser((new PersonInfo(name, surname, email, birthday)));
        }

        public void EditUser(PersonInfo toEdit, string name, string surname, string email, DateTime birthday)
        {
            Data.EditUser(new PersonInfo(name, surname, email, birthday));
        }
    }
}