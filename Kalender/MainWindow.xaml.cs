using Kalender.Data;
using Kalender.View.Sections.CalenderViews;
using Kalender.View.Sections.Main.ViewModels;
using System.Windows;

namespace Kalender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AppointmentConfiguration_VM AppointmentConfigurationVM { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            AppointmentConfigurationVM = new AppointmentConfiguration_VM();
            frm_CalenderView.Content = new MonthView();
            dop_AppointmentPanel.DataContext = AppointmentConfigurationVM;
        }

        private void btn_MonthView_Click(object sender, RoutedEventArgs e)
        {
            frm_CalenderView.Content = new MonthView();
        }

        private void btn_DayView_Click(object sender, RoutedEventArgs e)
        {
            frm_CalenderView.Content = new DayView();
             
        }
    }
}
