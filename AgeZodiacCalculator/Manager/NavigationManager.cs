using AgeZodiacCalculator.Model;

namespace AgeZodiacCalculator.Manager
{
    internal class NavigationManager
    {
        private static NavigationManager _instance;
        private static readonly object Lock = new object();

        private NavigationModel _navigation;

        public static NavigationManager Instance
        {
            // todo recall what happens here
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                lock (Lock)
                {
                    _instance = new NavigationManager();
                }

                return _instance;
            }
        }

        public void Initialize(NavigationModel model)
        {
            _navigation = model;
        }

        public void Navigate(Model.View view)
        {
            _navigation?.Navigate(view);
        }
    }
}