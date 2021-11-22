using Kalender.Model;
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

namespace Kalender.View.Controls
{
    /// <summary>
    /// Interaktionslogik für AppointmentBar.xaml
    /// </summary>
    public partial class AppointmentBar : UserControl
    {
        public AppointmentBar(Appointment appointment)
        {
            InitializeComponent();
            Appointment = appointment;
            DataContext = Appointment;
        }

        public Appointment Appointment { get; set; }
    }
}
