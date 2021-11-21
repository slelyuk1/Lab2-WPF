using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using AgeZodiacCalculator.Info;
using Shared.Tool;
using Shared.View.Navigator;
using UserStorage.Models;
using UserStorage.View;

namespace UserStorage.ViewModel
{
    public class UsersViewModel : ObservableItem
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

        private readonly IViewNavigator<Type> _navigator;
        private readonly UsersModel _model;
        private readonly CollectionView _peopleView;

        public UsersViewModel(IViewNavigator<Type> navigator, UsersModel model)
        {
            _navigator = navigator;
            _model = model;
            _peopleView = new CollectionView(model.People);

            FilterProperties = new List<string>(DefaultFilterProperties);
            SelectedProperty = FilterProperties[0];
            FilterText = "";

            AddCommand = new DelegateBasedCommand(OpenUserInputForAdd);
            DeleteCommand = new DelegateBasedCommand(ExecuteDelete, _ => _model.IsUserChosen);
            EditCommand = new DelegateBasedCommand(OpenUserInputForEdit, _ => _model.IsUserChosen);
            FilterCommand = new DelegateBasedCommand(ExecuteFilter);
        }

        public ObservableCollection<PersonInfo> People => _model.People;
        public IList<string> FilterProperties { get; }
        public string FilterText { get; set; }
        public string SelectedProperty { get; set; }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand FilterCommand { get; }

        public PersonInfo? SelectedUser
        {
            get => _model.ChosenPerson;
            set => _model.ChosenPerson = value;
        }

        // todo implement
        public void UsersSorting(object sender, DataGridSortingEventArgs e)
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

        public void EditPerson(PersonInfo toReplaceWith)
        {
            _model.EditPerson(toReplaceWith);
        }

        private void OpenUserInputForAdd(object obj)
        {
            _navigator.ExecuteAndNavigate<UserInputViewModel>(typeof(UserInputView), viewModel => viewModel.PrepareForInput());
        }

        private void OpenUserInputForEdit(object obj)
        {
            if (SelectedUser == null)
            {
                // todo
                throw new InvalidOperationException();
            }

            _navigator.ExecuteAndNavigate<UserInputViewModel>(typeof(UserInputView), viewModel => viewModel.PrepareForEdit(SelectedUser));
        }

        private void ExecuteFilter(object obj)
        {
            _peopleView.Filter = item =>
            {
                string filter = FilterText.ToLower();
                var user = (PersonInfo) item;
                if (SelectedProperty == AllFilterProperty)
                {
                    string name = user.Name.ToLower();
                    string surname = user.Surname.ToLower();
                    string email = user.Email.ToLower();
                    ChineseSign chineseSign = user.ChineseSign;
                    WesternSign westernSign = user.WesternSign;
                    // todo make better logic for signs
                    return name.Contains(filter) || surname.Contains(filter) || email.Contains(filter) ||
                           email.Contains(filter) || chineseSign.ToString().Contains(filter) || westernSign.ToString().Contains(filter);
                }

                PropertyInfo? userProperty = user.GetType().GetProperty(SelectedProperty);
                if (userProperty == null)
                {
                    throw new ArgumentException("Inappropriate property for user !");
                }

                object propertyVal = userProperty.GetValue(user, null);
                if (propertyVal is not string stringRepresentation)
                {
                    // todo normal conversion
                    stringRepresentation = propertyVal.ToString();
                }

                return stringRepresentation.ToLower().Contains(filter);
            };
        }

        private void ExecuteDelete(object obj)
        {
            _model.DeleteSelectedUser();
        }
    }
}