using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeZodiacCalculator.Views;

namespace AgeZodiacCalculator.Models
{
    public enum Views
    {
        PickData
    }

    internal class NavigationModel
    {
        private ContentWindow _window;
        private PickDataView _pickDataView;

        public NavigationModel( ContentWindow window)
        {
            _window = window;
            _pickDataView = new PickDataView();

        }

        public void Navigate(Views view)
        {
            switch (view)
            {
                case Views.PickData:
                    _window.MinWidth = 300;
                    _window.MinHeight = 250;
                    _window.ContentControl.Content = _pickDataView;
                    break;
                default:
                    throw new ArgumentException("Inappropriate argument for method Navigate !");
            }
        }
    }
}