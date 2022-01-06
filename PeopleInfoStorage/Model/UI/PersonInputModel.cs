using System;
using PeopleInfoStorage.Model.Data;

namespace PeopleInfoStorage.Model.UI
{
    public class PeopleInputModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public PeopleInputModel()
        {
            Name = "";
            Surname = "";
            Email = "";
            BirthDate = DateTime.Now;
        }

        public PeopleInputModel(PersonInfo person)
        {
            Name = person.Name;
            Surname = person.Surname;
            Email = person.Email;
            BirthDate = person.BirthDate;
        }

        public PersonInfo MakeInfo()
        {
            return new PersonInfo.Builder()
                .WithName(Name)
                .WithSurname(Surname)
                .WithEmail(Email)
                .WithBirthDate(BirthDate)
                .Build();
        }
    }
}