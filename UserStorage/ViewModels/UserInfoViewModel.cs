using System.Windows.Input;
using UserStorage.Managers;
using UserStorage.Models;
using UserStorage.Tools;

namespace UserStorage.ViewModels
{
    class UserInfoViewModel : ObservableItem
    {
        private string _name, _surname, _email, _birthDay, _isAdult, _sunSign, _chineseSign, _isBirthDay;
        private ICommand _goBackCommand;

        public UserInfoViewModel(Storage data)
        {
            data.UserAdded += UiUserSet;
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Birthday
        {
            get => _birthDay;
            set
            {
                _birthDay = value;
                OnPropertyChanged();
            }
        }

        public string IsAdult
        {
            get => _isAdult;
            set
            {
                _isAdult = value;
                OnPropertyChanged();
            }
        }

        public string SunSign
        {
            get => _sunSign;
            set
            {
                _sunSign = value;
                OnPropertyChanged();
            }
        }

        public string ChineseSign
        {
            get => _chineseSign;
            set
            {
                _chineseSign = value;
                OnPropertyChanged();
            }
        }

        public string IsBirthday
        {
            get => _isBirthDay;
            set
            {
                _isBirthDay = value;
                OnPropertyChanged();
            }
        }

        public ICommand GoBackCommand
        {
            get
            {
                if (_goBackCommand == null)
                    _goBackCommand = new RelayCommand(ExecuteGoBack, null);
                return _goBackCommand;
            }
        }

        private void ExecuteGoBack(object obj)
        {
            NavigationManager.Instance.Navigate(Models.Views.UserInputView);
        }

        private void UiUserSet(Person newUser)
        {
            Name = newUser.Name;
            Surname = newUser.Surname;
            Email = newUser.Email;
            var birth = newUser.BirthDate;
            Birthday = $"{birth.Day}.{birth.Month}.{birth.Year}";
            IsAdult = newUser.IsAdult ? "Yes" : "No";
            SunSign = newUser.SunSign;
            ChineseSign = newUser.ChineseSign;
            IsBirthday = newUser.IsBirthday ? "Yes" : "No";
        }
    }
}