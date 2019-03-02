using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab2_Leliuk.Models;

namespace Lab2_Leliuk.Managers
{
    class NavigationManager
    {
        private static NavigationManager _manager;
        private static object _lock = new object();
        private NavigationModel _model;

        public static NavigationManager Instance
        {
            get
            {
                if (_manager == null)
                {
                    lock (_lock)
                    {
                        _manager = new NavigationManager();
                    }
                }

                return _manager;
            }
        }

        public void Initialise(NavigationModel model)
        {
            _model = model;
        }

        public void Navigate(Models.Views view)
        {
            _model?.Navigate(view);
        }
    }
}