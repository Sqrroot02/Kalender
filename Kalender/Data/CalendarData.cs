using Kalender.Base;
using Kalender.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Kalender.Data
{
    /// <summary>
    /// Datenlogik für Kalenderdaten
    /// </summary>
    public sealed class CalendarData : DataBase<Calendar>
    {
        /// <summary>
        /// Event für Änderungen beim ausgewähltem Datum
        /// </summary>
        public static event EventHandler<DateTime>? SelectedDateChanged;
        /// <summary>
        /// Event für Änderungen beim ausgewähltem Kalender
        /// </summary>
        public static event EventHandler<Calendar>? SelectedCalenderChanged;

        /// <summary>
        /// Das derzeitige Ausgewählte Datum
        /// </summary>
        private static DateTime _selectedDate = DateTime.Now;
        public static DateTime SelectedDate 
        { 
            get => _selectedDate;
            set { _selectedDate = value; SelectedDateChanged?.Invoke(value,value);GC.Collect(); GC.WaitForPendingFinalizers();GC.Collect(); }
        }

        private static ObservableCollection<Calendar> _calendars = new ObservableCollection<Calendar>();
        /// <summary>
        /// Alle aktuell relevanten Kalender
        /// </summary>
        public static ObservableCollection<Calendar> Calendars
        {
            get => _calendars;
            set { _calendars = value; }
        }

        private static Calendar _selectedCalender = Calendars.FirstOrDefault();
        /// <summary>
        /// Der derzeitige ausgewählte Kalender
        /// </summary>
        public static Calendar SelectedCalender
        {
            get => _selectedCalender;
            set { _selectedCalender = value; SelectedCalenderChanged?.Invoke(value, value); }
        }
             

        MySqlCommand _command = new MySqlCommand();

        public CalendarData() =>
            _command.Connection = DatabaseConnection.Connection;

        public override void DeleteItem(Calendar item)
        {
            string sql = "DELETE FROM calender WHERE calenderId=@ccalenderId";

            _command.Parameters.Clear();
            _command.CommandText = sql;
            _command.Parameters.Add(new MySqlParameter("ccalenderId", item.CalendarId));

            _command.ExecuteNonQuery();
        }

        public override void DeleteItems(List<Calendar> items)
        {
            throw new NotImplementedException();
        }

        public override Calendar GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Calendar> GetItems()
        { 
           List<Calendar> items = new List<Calendar>(); 

            string sql = @"SELECT * FROM calender";

            _command.Parameters.Clear();
            _command.CommandText = sql;

            using (MySqlDataAdapter adapter = new MySqlDataAdapter())
            {
                adapter.SelectCommand = _command;
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                items = MapCalender(dataTable);
            }
            return items;
        }

        public List<Calendar> MapCalender(DataTable dataTable)
        {
            List<Calendar> calendars = new List<Calendar>();

            foreach (DataRow item in dataTable.Rows)
            {
                Calendar calendar = new Calendar()
                {
                    Name = item["Name"].ToString(),
                    TimeCreated = DateTime.Parse(item["DateCreated"].ToString()),
                    CalendarId = Guid.Parse(item["calenderId"].ToString()),
                    Color = (Color)ColorConverter.ConvertFromString(item["Color"].ToString())
                };
                calendars.Add(calendar);
            }
            return calendars;
        }
             

        public static void InitTables()
        {
            MySqlCommand mySqlCommand = new MySqlCommand();

            mySqlCommand.Connection = DatabaseConnection.Connection;
            mySqlCommand.Parameters.Clear();

            string sql = "CREATE TABLE IF NOT EXISTS Calender (calenderId VARCHAR(56) PRIMARY KEY, Color VARCHAR(56), userId VARCHAR(56), DateCreated DATETIME, Name VARCHAR(56),IsVisible TINYINT(1));";
            mySqlCommand.CommandText = sql;
            mySqlCommand.ExecuteNonQuery();
        }

        public override void InsertItem(Calendar item)
        {
            Random rnd = new Random();
            Color col = new Color();
            col.R = Convert.ToByte(rnd.Next(255));
            col.G = Convert.ToByte(rnd.Next(255));
            col.B = Convert.ToByte(rnd.Next(255));
            col.A = 140;
            item.Color = col;

            string sql = @"INSERT INTO calender (calenderId,DateCreated,Name,Color) VALUES (@cid,@dtcreate,@cname,@ccolor)";

            _command.Parameters.Clear();
            _command.CommandText = sql;
            _command.Parameters.Add(new MySqlParameter("cid", item.CalendarId));
            _command.Parameters.Add(new MySqlParameter("dtcreate", item.TimeCreated));
            _command.Parameters.Add(new MySqlParameter("cname", item.Name));
            _command.Parameters.Add(new MySqlParameter("ccolor", item.Color.ToString()));

            _command.ExecuteNonQuery();
        }

        public override void InsertItems(List<Calendar> items)
        {
            throw new NotImplementedException();
        }

        public override void UpdateItem(Calendar item)
        {
            string sql = @"UPDATE calender SET DateCreated=@cdateCreated,Name=@cname WHERE calenderId=@ccalenderId;";

            _command.Parameters.Clear();
            _command.CommandText= sql;
            _command.Parameters.Add(new MySqlParameter("cdateCreated", item.TimeCreated));
            _command.Parameters.Add(new MySqlParameter("cname", item.Name));
            _command.Parameters.Add(new MySqlParameter("ccalenderId", item.CalendarId));

            _command.ExecuteNonQuery();
        }
    }
}
