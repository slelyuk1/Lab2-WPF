using AgeZodiacCalculator.ViewModels;

namespace AgeZodiacCalculator.Views
{
    public partial class PickDataView
    {
        public PickDataView()
        {
            InitializeComponent();
            DataContext = new PickDataViewModel();
        }
    }
}