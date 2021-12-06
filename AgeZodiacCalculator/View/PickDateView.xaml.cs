using AgeZodiacCalculator.ViewModel;

namespace AgeZodiacCalculator.View
{
    public partial class PickDateView
    {
        public PickDateView(PickDateViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}