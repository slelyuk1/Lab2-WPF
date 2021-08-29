using System;
using AgeZodiacCalculator.View;
using AgeZodiacCalculator.Window;

namespace AgeZodiacCalculator.Model
{
    internal enum View
    {
        PickData
    }

    internal class NavigationModel
    {
        private readonly ContentWindow _window;
        private readonly PickDataView _pickDataView;

        public NavigationModel(ContentWindow window)
        {
            _window = window;
            // todo maybe DI
            _pickDataView = new PickDataView();
        }

        public void Navigate(View view)
        {
            switch (view)
            {
                case View.PickData:
                    // todo move to instance
                    _window.MinWidth = 300;
                    _window.MinHeight = 250;
                    _window.ContentControl.Content = _pickDataView;
                    break;
                default:
                    throw new ArgumentException("Inappropriate argument for method Navigate");
            }
        }
    }
}