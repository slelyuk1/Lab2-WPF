using System;

namespace Lab2_Leliuk.Models
{
    public class Storage
    {
        public event Action<Person> UserChanged;
        private Person _user;

        public Person User
        {
            get => _user;
            set => _user = value;
        }

        public void ChangeUser(Person user)
        {
            _user = user;
            UserChanged?.Invoke(user);
        }
    }
}