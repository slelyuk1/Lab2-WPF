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
        private readonly PickDateView _pickDateView;

        public NavigationModel(ContentWindow window, PickDateView pickDateView)
        {
            _window = window;
            _pickDateView = pickDateView;
        }

        public void Navigate(View view)
        {
            switch (view)
            {
                case View.PickData:
                    // todo move to instance
                    _window.MinWidth = 300;
                    _window.MinHeight = 250;
                    _window.ContentControl.Content = _pickDateView;
                    break;
                default:
                    throw new ArgumentException("Inappropriate argument for method Navigate");
            }
        }
    }
}