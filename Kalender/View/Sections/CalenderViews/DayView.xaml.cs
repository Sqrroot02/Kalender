using Kalender.View.Sections.CalenderViews.ViewModels;
using System.Windows.Controls;

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
