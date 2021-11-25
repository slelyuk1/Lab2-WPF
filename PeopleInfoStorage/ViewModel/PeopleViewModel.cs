using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using PeopleInfoStorage.Model.Data;
using PeopleInfoStorage.Model.UI;
using PeopleInfoStorage.View;
using Shared.ComponentModel;
using Shared.Model.Data;
using Shared.Tool.ViewModel;
using Shared.View.Navigator;

namespace PeopleInfoStorage.ViewModel
{
    public class PeopleViewModel : ObservableItem
    {
        private const string AllFilterProperty = "All";

        private static readonly string[] DefaultFilterProperties =
        {
            AllFilterProperty,
            nameof(PersonInfo.Name),
            nameof(PersonInfo.Surname),
            nameof(PersonInfo.Email),
            nameof(PersonInfo.WesternSign),
            nameof(PersonInfo.ChineseSign)
        };

        private readonly PeopleModel _model;
        private readonly IViewNavigator<Type> _navigator;
        private readonly TypeConverter _chineseSignConverter;
        private readonly TypeConverter _westernSignConverter;

        private string _filterText;

        public PeopleViewModel(
            IViewNavigator<Type> navigator,
            PeopleModel model,
            TypeConverter chineseSignConverter,
            TypeConverter westernSignConverter
        )
        {
            _navigator = navigator;
            _model = model;
            _chineseSignConverter = chineseSignConverter;
            _westernSignConverter = westernSignConverter;

            _filterText = "";

            People = _model.People;
            PeopleView = new GenericCollectionViewAdapter<PersonInfo>(CollectionViewSource.GetDefaultView(People));
            FilterProperties = new List<string>(DefaultFilterProperties);
            SelectedProperty = FilterProperties[0];

            AddCommand = new DelegateBasedCommand(OpenInputForAdd);
            DeleteCommand = new DelegateBasedCommand(DeleteSelectedPerson, _ => _model.IsPersonChosen);
            EditCommand = new DelegateBasedCommand(OpenInputForEdit, _ => _model.IsPersonChosen);
        }

        public ObservableCollection<PersonInfo> People { get; }
        public GenericCollectionViewAdapter<PersonInfo> PeopleView { get; }
        public IList<string> FilterProperties { get; }

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                RunFilter(_filterText);
            }
        }

        public string SelectedProperty { get; set; }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public PersonInfo? SelectedPerson
        {
            get => _model.ChosenPerson;
            set => _model.ChosenPerson = value;
        }

        // todo implement
        public void Sort(object sender, DataGridSortingEventArgs e)
        {
            /* bool ascending = e.Column.SortDirection == ListSortDirection.Ascending;
             var sorted = ascending ? Users.OrderBy(u => u.Name) : Users.OrderByDescending(u => u.Name);
             Users = new ObservableCollection<Person>(sorted);
            */
        }

        public void AddPerson(PersonInfo info)
        {
            _model.AddPerson(info);
        }

        public void ReplaceSelectedPerson(PersonInfo with)
        {
            _model.EditPerson(with);
        }

        private void DeleteSelectedPerson(object obj)
        {
            _model.DeleteSelectedPerson();
        }

        private void OpenInputForAdd(object obj)
        {
            _navigator.ExecuteAndNavigate<PersonInputViewModel>(typeof(PersonInputView), viewModel => viewModel.PrepareForInput());
        }

        private void OpenInputForEdit(object obj)
        {
            if (SelectedPerson == null)
            {
                // todo
                throw new InvalidOperationException();
            }

            _navigator.ExecuteAndNavigate<PersonInputViewModel>(typeof(PersonInputView), viewModel => viewModel.PrepareForEdit(SelectedPerson));
        }

        private void RunFilter(string filterText)
        {
            filterText = filterText.ToLower();
            PeopleView.Filter = item =>
            {
                Type filteredType = typeof(PersonInfo);

                PropertyInfo[] filteredProperties = SelectedProperty == AllFilterProperty
                    ? filteredType.GetProperties()
                    : new[] {filteredType.GetProperty(SelectedProperty)};

                foreach (var property in filteredProperties)
                {
                    object value = property.GetValue(item, null);
                    string? stringValue = value switch
                    {
                        ChineseSign chineseSign => _chineseSignConverter.ConvertToString(chineseSign),
                        WesternSign westernSign => _westernSignConverter.ConvertToString(westernSign),
                        _ => value?.ToString()
                    };
                    if (stringValue != null && stringValue.ToLower().Contains(filterText))
                    {
                        return true;
                    }
                }

                return false;
            };
        }
    }
}