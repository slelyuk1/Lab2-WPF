using System;
using UserStorage.Views;

namespace UserStorage.Models
{
    public enum Views
    {
        UserInputView,
        UserInfoView,
        UsersView
    }

    class NavigationModel
    {
        private MainWindow _window;
        private UserInputView _userInputView;
        private UserInfoView _userInfoView;
        private UsersView _usersView;

        public NavigationModel(MainWindow window, Storage data)
        {
            _window = window;
            _userInputView = new UserInputView(data);
            _userInfoView = new UserInfoView(data);
            _usersView = new UsersView(data);
        }

        public void Navigate(Views view)
        {
            switch (view)
            {
                case Views.UserInputView:
                    _window.Title = "Info Input";
                    _window.MinWidth = 300;
                    _window.MinHeight = 271;
                    _window.WindowContents.Content = _userInputView;
                    break;
                case Views.UserInfoView:
                    _window.Title = "Info";
                    _window.MinHeight = 310;
                    _window.WindowContents.Content = _userInfoView;
                    break;
                case Views.UsersView:
                    _window.Title = "Users";
                    _window.MinHeight = 200;
                    _window.MaxHeight = 500;
                    _window.MinWidth = 750;
                    _window.MaxWidth = 900;
                    _window.WindowContents.Content = _usersView;
                    break;
                default:
                    throw new ArgumentException("Inappropriate parameter for navigation !");
            }
        }
    }
}