using AgeZodiacCalculator.Models;

namespace AgeZodiacCalculator.Managers
{
    class NavigationManager
    {
        private static NavigationManager _instance;
        private static object _lock = new object();
        private NavigationModel _navigation;

        public static NavigationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance = new NavigationManager();
                    }
                }

                return _instance;
            }
        }

        public void Initialise(NavigationModel model)
        {
            _navigation = model;
        }

        public void Navigate(Models.Views view)
        {
            _navigation?.Navigate(view);
        }
    }
}