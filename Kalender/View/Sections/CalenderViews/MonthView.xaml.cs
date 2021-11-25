using Kalender.Base;
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
        private readonly ValueCheck _valueCheck = new ValueCheck();
        public MonthView_VM ViewModel { get; set; }
        public MonthView()
        {
            InitializeComponent();
            ViewModel = new MonthView_VM();
            this.DataContext = ViewModel;
        }

        private void txb_MonthInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //if (((TextBox)sender).Text == "")
            //{
            //    e.Handled = false; return;   
            //}
            //e.Handled = !_valueCheck.IsMonth(((TextBox)sender).Text);           
        }

        private void txb_YearInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //if (((TextBox)sender).Text == "")
            //{
            //    e.Handled = false; return;
            //}
            //e.Handled = !_valueCheck.IsValidYear(((TextBox)sender).Text);
        }

    }
}
