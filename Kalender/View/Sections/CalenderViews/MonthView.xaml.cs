using Kalender.Base;
using Kalender.View.Sections.CalenderViews.ViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Input;

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
    }
}
