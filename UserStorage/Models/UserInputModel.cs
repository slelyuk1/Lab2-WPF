using System;

namespace UserStorage.Models
{
    public class UserInputModel
    {
        public UserInputModel()
        {
            Name = "Oleksandr";
            Surname = "Leliuk";
            Email = "slelyuk1@gmail.com";
            BirthDate = new DateTime(2000, 2, 13);
        }

        public UserInputModel(PersonInfo person)
        {
            ChosenUser = person;
            Name = ChosenUser.Name;
            Surname = ChosenUser.Surname;
            Email = ChosenUser.Email;
            BirthDate = ChosenUser.BirthDate;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public PersonInfo? ChosenUser { get; }

        public PersonInfo MakeInfo()
        {
            return PersonInfo.From(Name, Surname, Email, BirthDate);
        }
    }
}