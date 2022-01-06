using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using PeopleInfoStorage.Model.Data;

namespace PeopleInfoStorage.Model.UI
{
    public class PeopleModel
    {
        private readonly ILogger<PeopleModel> _logger;

        public PeopleModel(IEnumerable<PersonInfo> people, ILogger<PeopleModel> logger)
        {
            People = new ObservableCollection<PersonInfo>(people);
            _logger = logger;
        }

        public ObservableCollection<PersonInfo> People { get; }

        public PersonInfo? ChosenPerson { get; set; }

        public void AddPerson(PersonInfo toAdd)
        {
            People.Add(toAdd);
        }

        public void EditPerson(PersonInfo edited)
        {
            if (!IsPersonChosen)
            {
                _logger.LogWarning("Tried to edit without chosen user with: {Edited}", edited);
                return;
            }

            // todo make normal
            int foundIndex = People.IndexOf(ChosenPerson ?? throw new InvalidOperationException("ChosenPerson is expected to be non-null"));
            if (foundIndex == -1)
            {
                _logger.LogWarning("Couldn't find an edited by chosen person: {ChosenPerson}", ChosenPerson);
                return;
            }

            People[foundIndex] = edited;
        }

        public void DeleteSelectedPerson()
        {
            if (!IsPersonChosen)
            {
                _logger.LogWarning("Tried to delete info without selected person");
                return;
            }

            People.Remove(ChosenPerson ?? throw new InvalidOperationException("ChosenPerson is expected to be non-null"));
        }

        public bool IsPersonChosen => ChosenPerson != null;
    }
}