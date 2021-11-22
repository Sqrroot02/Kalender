using Kalender.View.Controls;
using Kalender.View.Sections.CalenderViews;
using Kalender.View.Sections.Main.ViewModels;
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
            MonthView monthView = new MonthView();
            frm_CalenderView.Content = monthView;
            dop_AppointmentPanel.DataContext = AppointmentConfigurationVM;
        }
    }
}
