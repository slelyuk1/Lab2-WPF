using System;
using Lab1.Views;

namespace Lab1.Models
{
    enum Views
    {
        UserInputView,
        UserInfoView
    }

    class NavigationModel
    {
        private Storage _data;
        private MainWindow _window;
        private UserInputView _userInputView;
        private UserInfoView _userInfoView;

        public NavigationModel(MainWindow window, Storage data)
        {
            _window = window;
            _data = data;
            _userInputView = new UserInputView(data);
            _userInfoView = new UserInfoView(_data);
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
                default:
                    throw new ArgumentException("Inappropriate parameter for navigation !");
            }
        }
    }
}