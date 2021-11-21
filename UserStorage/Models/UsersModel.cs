using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UserStorage.Models
{
    public class UsersModel
    {
        public UsersModel(IEnumerable<PersonInfo> people)
        {
            People = new ObservableCollection<PersonInfo>(people);
        }

        public ObservableCollection<PersonInfo> People { get; }

        public PersonInfo? ChosenPerson { get; set; }

        public void AddPerson(PersonInfo toAdd)
        {
            People.Add(toAdd);
        }

        public void EditPerson(PersonInfo edited)
        {
            if (ChosenPerson == null)
            {
                return;
            }

            // todo make normal
            int foundIndex = People.IndexOf(ChosenPerson);
            if (foundIndex == -1)
            {
                throw new NullReferenceException("No such user in storage to edit !");
            }

            People[foundIndex] = edited;
        }

        public void DeleteSelectedUser()
        {
            if (ChosenPerson == null)
            {
                return;
            }

            People.Remove(ChosenPerson);
        }

        public bool IsUserChosen => ChosenPerson != null;
    }
}