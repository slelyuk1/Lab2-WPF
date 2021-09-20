using System;
using System.Collections.Generic;
using UserStorage.Managers;

namespace UserStorage.Models
{
    public class Storage
    {
        public event Action<PersonInfo>? UserAdded;
        public event Action<PersonInfo>? UserDeleted;
        public event Action<PersonInfo>? UserEdited;
        public event Action<PersonInfo>? UserChosen;

        private PersonInfo? _currentChosenUser;
        public IList<PersonInfo> Users { get; }

        public Storage()
        {
            Users = SerializationManager.DeserializeUsers() ?? new List<PersonInfo>();
        }

        public PersonInfo? ChosenUser
        {
            get => _currentChosenUser;
            set
            {
                _currentChosenUser = value;
                UserChosen?.Invoke(value);
            }
        }

        public void AddUser(PersonInfo user)
        {
            UserAdded?.Invoke(user);
        }

        public void EditUser(PersonInfo edited)
        {
            UserEdited?.Invoke(edited);
        }

        public void DeleteUser(PersonInfo user)
        {
            UserDeleted?.Invoke(user);
        }
    }
}