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
    /// Interaktionslogik für MonthView.xaml
    /// </summary>
    public partial class MonthView : Page
    {
        public MonthView_VM ViewModel { get; set; }
        public MonthView()
        {
            InitializeComponent();
            ViewModel = new MonthView_VM();
            this.DataContext = ViewModel;
        }
    }
}
