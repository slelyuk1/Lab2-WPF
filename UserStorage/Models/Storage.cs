using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UserStorage.Managers;

namespace UserStorage.Models
{
    public class Storage
    {
        private static readonly string usersRecoveryPath = "..\\..\\SerializedData\\default_users.bin";

        public event Action<Person> UserAdded;
        public event Action<Person> UserDeleted;
        public event Action<Person> UserEdited;
        public event Action<Person> UserChosen;

        private Person _currentChosenUser;
        public LinkedList<Person> Users;

        public Storage(string serializationPath)
        {
            Users = new LinkedList<Person>();
            if (File.Exists(serializationPath))
            {
                Users = SerializationManager.DeserializeUsers(serializationPath);
            }
            else if (File.Exists(usersRecoveryPath))
            {
                Users = SerializationManager.DeserializeUsers(usersRecoveryPath);
            }
            else
            {
                throw new FileNotFoundException("Files that contain users were not found !");
            }
        }

        public Person ChosenUser
        {
            get => _currentChosenUser;
            set
            {
                _currentChosenUser = value;
                UserChosen?.Invoke(value);
            }
        }

        public void AddUser(Person user)
        {
            UserAdded?.Invoke(user);
        }

        public void EditUser(Person edited)
        {
            UserEdited?.Invoke(edited);
        }

        public void DeleteUser(Person user)
        {
            UserDeleted?.Invoke(user);
        }
    }
}