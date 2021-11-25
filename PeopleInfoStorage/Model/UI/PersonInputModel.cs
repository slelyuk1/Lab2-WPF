using System;
using PeopleInfoStorage.Model.Data;

namespace PeopleInfoStorage.Model.UI
{
    public class PeopleInputModel
    {
        public PeopleInputModel()
        {
            Name = "Oleksandr";
            Surname = "Leliuk";
            Email = "slelyuk1@gmail.com";
            BirthDate = new DateTime(2000, 2, 13);
        }

        public PeopleInputModel(PersonInfo person)
        {
            Name = person.Name;
            Surname = person.Surname;
            Email = person.Email;
            BirthDate = person.BirthDate;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public PersonInfo MakeInfo()
        {
            return PersonInfo.From(Name, Surname, Email, BirthDate);
        }
    }
}