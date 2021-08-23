using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AgeZodiacCalculator.Models;
using AgeZodiacCalculator.ViewModels;
using UserControl = System.Windows.Controls.UserControl;

namespace AgeZodiacCalculator.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class PickDataView : UserControl
    {
        private PickDataViewModel _model;

        public PickDataView()
        {
            InitializeComponent();
            _model = new PickDataViewModel();
            DataContext = _model;
            
        }
    }
}