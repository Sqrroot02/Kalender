using Kalender.View.Sections.CalenderViews;
using Kalender.View.Sections.Main.ViewModels;
using System;
using System.Windows;

namespace Kalender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AppointmentConfiguration_VM AppointmentConfigurationVM { get; set; }
        public CalendarConfiguration_VM CalendarConfigurationVM { get; set; }

        public MonthView MonthView { get; set; }
        public DayView DayView { get; set; }    

        public MainWindow()
        {
            InitializeComponent();   

            AppointmentConfigurationVM = new AppointmentConfiguration_VM();
            CalendarConfigurationVM = new CalendarConfiguration_VM();  

            DayView = new DayView();
            MonthView = new MonthView();
            
            frm_CalenderView.Content = new MonthView();

            dop_AppointmentPanel.DataContext = AppointmentConfigurationVM;
            dop_CalenderSettings.DataContext = CalendarConfigurationVM;
        }

        private void btn_MonthView_Click(object sender, RoutedEventArgs e) =>
            frm_CalenderView.Content = new MonthView();

        private void btn_DayView_Click(object sender, RoutedEventArgs e) =>
            frm_CalenderView.Content = new DayView();            

        

        // Prüft ob eine Zahl für die Uhrzeit angegeben wurde und erlaubt die Eingabe nur in diesem Falle
        private void txb_NewAppMinuteStart_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (char.IsDigit(Convert.ToChar(e.Text))) 
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
