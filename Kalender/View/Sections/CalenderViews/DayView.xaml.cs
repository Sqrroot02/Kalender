using Kalender.View.Sections.CalenderViews.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kalender.View.Sections.CalenderViews
{
    /// <summary>
    /// Interaktionslogik für DayView.xaml
    /// </summary>
    public partial class DayView : Page
    {
        public DayView_VM ViewModel { get; set; }
        public DayView()
        {
            InitializeComponent();
            ViewModel = new DayView_VM();
            DataContext = ViewModel;    
        }
    }
}
