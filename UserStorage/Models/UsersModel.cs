using System;

namespace UserStorage.Models
{
    public class UsersModel
    {
        private readonly Storage _storage;

        public UsersModel(Storage storage)
        {
            _storage = storage;
        }

        public PersonInfo? ChosenPersonInfo
        {
            get => _storage.ChosenUser;
            set => _storage.ChosenUser = value;
        }

        public void AddUser(PersonInfo user)
        {
            _storage.Users.Add(user);
        }

        public void EditUser(PersonInfo edited)
        {
            // todo make normal
            int foundIndex = -1;
            for (var i = 0; i < _storage.Users.Count; ++i)
            {
                if (_storage.ChosenUser == edited)
                {
                    foundIndex = i;
                    break;
                }
            }

            if (foundIndex == -1)
            {
                throw new NullReferenceException("No such user in storage to edit !");
            }

            _storage.Users[foundIndex] = edited;
        }

        public void DeleteUser(PersonInfo user)
        {
            _storage.Users.Remove(user);
        }

        public bool IsUserChosen => ChosenPersonInfo != null;
    }
}