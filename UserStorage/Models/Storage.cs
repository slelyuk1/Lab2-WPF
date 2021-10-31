using System;
using System.Collections.Generic;

namespace UserStorage.Models
{
    [Serializable]
    public class Storage
    {
        // todo review NonSerialized usage
        [field: NonSerialized] public event Action<PersonInfo>? UserAdded;
        [field: NonSerialized] public event Action<PersonInfo>? UserDeleted;
        [field: NonSerialized] public event Action<PersonInfo>? UserEdited;
        [field: NonSerialized] public event Action<PersonInfo>? UserChosen;

        [NonSerialized] private PersonInfo? _currentChosenUser;
        public IList<PersonInfo> Users { get; }

        public Storage()
        {
            Users = new List<PersonInfo>();
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