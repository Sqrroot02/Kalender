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

        // Prüft ob die Startuhrzeit kleiner als die Enduhrzeit ist und setzt diese gleich wenn dies nicht der Fall seien sollte
        private void txb_NewAppHourStart_LostFocus(object sender, RoutedEventArgs e)
        {
            int hEnd = 0;
            int hStart = 0;

            int.TryParse(txb_NewAppHourStart.Text, out hStart);
            int.TryParse(txb_NewAppHourEnd.Text, out hEnd);

            if (hEnd < hStart)
            {
                txb_NewAppHourEnd.Text = hStart.ToString();
                txb_NewAppMinuteStart.Text = "0";
                txb_NewAppMinuteEnd.Text = "0";
            }
        }

        // Prüft ob die Startuhrzeit kleiner als die Enduhrzeit ist und setzt diese gleich wenn dies nicht der Fall seien sollte
        private void txb_NewAppHourEnd_LostFocus(object sender, RoutedEventArgs e)
        {
            int hEnd = 0;
            int hStart = 0;

            int.TryParse(txb_NewAppHourStart.Text, out hStart);
            int.TryParse(txb_NewAppHourEnd.Text, out hEnd);

            if (hEnd < hStart)
            {
                txb_NewAppHourStart.Text = hEnd.ToString();
                txb_NewAppMinuteStart.Text = "0";
                txb_NewAppMinuteEnd.Text = "0";
            }             
        }

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
