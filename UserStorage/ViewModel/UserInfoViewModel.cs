using System;
using System.Windows.Input;
using Shared.Tool;
using Shared.View.Navigator;
using UserStorage.Content;
using UserStorage.Models;

namespace UserStorage.ViewModel
{
    public class UserInfoViewModel : ObservableItem
    {
        private readonly IViewNavigator<Type> _navigator;
        private string _nameProperty = "";
        private string _surnameProperty = "";
        private string _emailProperty = "";
        private string _birthdayProperty = "";
        private string _isAdultProperty = "";
        private string _isBirthdayProperty = "";
        private string _sunSignProperty = "";
        private string _chineseSignProperty = "";
        public ICommand GoBackCommand { get; }

        public UserInfoViewModel(IViewNavigator<Type> navigator, Storage data)
        {
            _navigator = navigator;

            GoBackCommand = new DelegateBasedCommand(ExecuteGoBack, value => true);
            data.UserAdded += UiUserSet;
        }

        public string NameProperty
        {
            get => _nameProperty;
            set
            {
                _nameProperty = value;
                OnPropertyChanged();
            }
        }

        public string SurnameProperty
        {
            get => _surnameProperty;
            set
            {
                _surnameProperty = value;
                OnPropertyChanged();
            }
        }

        public string EmailProperty
        {
            get => _emailProperty;
            set
            {
                _emailProperty = value;
                OnPropertyChanged();
            }
        }

        public string BirthdayProperty
        {
            get => _birthdayProperty;
            set
            {
                _birthdayProperty = value;
                OnPropertyChanged();
            }
        }

        public string IsAdultProperty
        {
            get => _isAdultProperty;
            set
            {
                _isAdultProperty = value;
                OnPropertyChanged();
            }
        }

        public string IsBirthdayProperty
        {
            get => _isBirthdayProperty;
            set
            {
                _isBirthdayProperty = value;
                OnPropertyChanged();
            }
        }

        public string SunSignProperty
        {
            get => _sunSignProperty;
            set
            {
                _sunSignProperty = value;
                OnPropertyChanged();
            }
        }

        public string ChineseSignProperty
        {
            get => _chineseSignProperty;
            set
            {
                _chineseSignProperty = value;
                OnPropertyChanged();
            }
        }

        private void ExecuteGoBack(object obj)
        {
            _navigator.Navigate(typeof(UserInputContent));
        }

        private void UiUserSet(PersonInfo newUser)
        {
            NameProperty = newUser.Name;
            SurnameProperty = newUser.Surname;
            EmailProperty = newUser.Email;
            DateTime birth = newUser.BirthDate;
            BirthdayProperty = $"{birth.Day}.{birth.Month}.{birth.Year}";
            IsAdultProperty = newUser.IsAdult ? "Yes" : "No";
            SunSignProperty = newUser.SunSign;
            ChineseSignProperty = newUser.ChineseSign;
            IsBirthdayProperty = newUser.IsBirthday ? "Yes" : "No";
        }
    }
}