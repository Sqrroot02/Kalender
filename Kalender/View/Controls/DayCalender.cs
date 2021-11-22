using Kalender.Data;
using Kalender.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kalender.View.Controls
{
    /// <summary>
    /// Tageskalender Ansicht
    /// </summary>
    public class DayCalender : Canvas
    {
        private AppointmentData _appointmentData = new AppointmentData();

        public DayCalender()
        {
            Loaded += DayCalender_Loaded;
            SizeChanged += DayCalender_SizeChanged;
            AppointmentData.Appointments.CollectionChanged += Appointments_CollectionChanged;
        }

        #region Dependency Properties

        public int Day
        {
            get { return (int)GetValue(DayProperty); }
            set { SetValue(DayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Day.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DayProperty =
            DependencyProperty.Register("Day", typeof(int), typeof(DayCalender), new PropertyMetadata(CallbackDay));

        public static void CallbackDay(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DayCalender dayCalender = (DayCalender)sender;
            if ((int)e.NewValue > 31 && (int)e.NewValue == 0)
                dayCalender.Day = 1;
            else
                dayCalender.Day = (int)e.NewValue;
            dayCalender.DateChanged();
        }

        public int Year
        {
            get { return (int)GetValue(YearProperty); }
            set { SetValue(YearProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Year.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YearProperty =
            DependencyProperty.Register("Year", typeof(int), typeof(DayCalender), new PropertyMetadata(CallbackYear));

        public static void CallbackYear(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DayCalender dayCalender= (DayCalender)sender;
            if ((int)e.NewValue == 0)
                dayCalender.Year = 2021;
            else
                dayCalender.Year = (int) e.NewValue;
            dayCalender.DateChanged();
        }

        public int Month
        {
            get { return (int)GetValue(MonthProperty); }
            set { SetValue(MonthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Month.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MonthProperty =
            DependencyProperty.Register("Month", typeof(int), typeof(DayCalender), new PropertyMetadata(CallbackMonth));

        public static void CallbackMonth(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DayCalender dayCalender = (DayCalender)sender;
            if ((int)e.NewValue <= 0 || (int)e.NewValue > 12)
                dayCalender.Month = 1;
            else
                dayCalender.Month = (int)e.NewValue;
            dayCalender.DateChanged();
        }

        protected virtual void DateChanged()
        {
            if (Year != 0 && Month != 0 && Day != 0)
            {
                AppointmentData.Appointments = new ObservableCollection<Appointment>(_appointmentData.GetAppointmentsByDate(Year, Month, Day));
                CalendarData.SelectedDate = new DateTime(Year, Month, Day);
                Children.Clear();
                AppointmentBars.Clear();

                BuildAppointments();    
                BuildCalender();
            }
        }

        #endregion

        private void DayCalender_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            Children.Clear();
            BuildCalender();
        }

        private void DayCalender_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Year = CalendarData.SelectedDate.Year; Month = CalendarData.SelectedDate.Month; Day = CalendarData.SelectedDate.Day;
            Children.Clear();
            AppointmentBars.Clear();
            BuildAppointments();
            BuildCalender();
        }

        private void Appointments_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Children.Clear();
            AppointmentBars.Clear();
            BuildAppointments();
            BuildCalender();
        }

        private ObservableCollection<Appointment> _appointments = new ObservableCollection<Appointment>();
        public ObservableCollection<Appointment> Appointments { get => AppointmentData.Appointments; }

        private ObservableCollection<(AppointmentBar, Appointment)> _appointmentBars = new ObservableCollection<(AppointmentBar, Appointment)>();
        public ObservableCollection<(AppointmentBar, Appointment)> AppointmentBars { get { return _appointmentBars; } set { _appointmentBars = value; } }

        public void BuildAppointments()
        {
            foreach (Appointment item in Appointments)
            {
                AppointmentBar appointmentBar = new AppointmentBar(item);
                AppointmentBars.Add(new(appointmentBar, item));
            }
        }

        /// <summary>
        /// Baut den Kalender
        /// </summary>
        public void BuildCalender()
        {
            Children.Clear();
            double hourWidth = 120;
            double appointmentHeight = 60;
            SetValue(WidthProperty, hourWidth * 25);
            TimeOnly currentTime = new TimeOnly(0,0);

            for (int i = 0; i < 24; i++)
            {               
                #region Abstandslinie
                Line line = new Line();
                line.X1 = i*hourWidth+hourWidth;
                line.Y1 = 0;
                line.Y2 = (double)GetValue(ActualHeightProperty);
                line.X2 = i*hourWidth+hourWidth;
                line.Stroke = Brushes.Gray;

                Children.Add(line);
                #endregion

                #region Zeitangabe
                TextBlock textBlock = new TextBlock();
                textBlock.Text = currentTime.ToShortTimeString();
                textBlock.FontSize = 13;
                textBlock.FontWeight = FontWeights.Normal;

                Canvas.SetLeft(textBlock,i*hourWidth + hourWidth/2);
                Canvas.SetTop(textBlock,5);

                Children.Add(textBlock);
                #endregion

                currentTime = currentTime.AddHours(1);
            }

            int j = 0;
            foreach ((AppointmentBar, Appointment) item in AppointmentBars)
            {
                int hEnd = 0;
                int hStart = 0;

                if (item.Item2.FullTime)
                {
                    hEnd = 24;
                    hStart = 0;
                }
                else
                {
                    hEnd=item.Item2.HourEnd;
                    hStart=item.Item2.HourStart;
                }

                item.Item1.Height = appointmentHeight;
                item.Item1.Width = (hEnd * hourWidth + item.Item2.MinuteEnd * (hourWidth / 60)) - (hStart * hourWidth + item.Item2.MinuteStart * (hourWidth / 60));
                Canvas.SetLeft(item.Item1, hStart * hourWidth + item.Item2.MinuteStart*(hourWidth/60));
                Canvas.SetTop(item.Item1, j * appointmentHeight + appointmentHeight);

                Children.Add(item.Item1);
                j++;
            }
        }
    }
}
