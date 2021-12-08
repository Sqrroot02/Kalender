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

namespace Kalender.Data
{
    public sealed class CalendarData : DataBase<Calendar>
    {
        /// <summary>
        /// Event für Änderungen beim ausgewähltem Datum
        /// </summary>
        public static event EventHandler<DateTime>? SelectedDateChanged;

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
             

        MySqlCommand _command = new MySqlCommand();

        public CalendarData()
        {
            _command.Connection = DatabaseConnection.Connection;
        }

        public override void DeleteItem(Calendar item)
        {

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
            string sql = "SELECT * FROM calender";
            _command.Parameters.Clear();
            _command.CommandText = sql;

            using (MySqlDataAdapter adapter = new())
            {
                adapter.SelectCommand = _command;
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return MapCalender(dataTable);
            }
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
                };
            }
            return calendars;
        }
             

        public static void InitTables()
        {
            MySqlCommand mySqlCommand = new MySqlCommand();

            mySqlCommand.Connection = DatabaseConnection.Connection;
            mySqlCommand.Parameters.Clear();

            string sql = "CREATE TABLE IF NOT EXISTS Calender (calenderId VARCHAR(56) PRIMARY KEY, Color VARCHAR(56), userId VARCHAR(56), DateCreated DATETIME, Name VARCHAR(56));";
            mySqlCommand.CommandText = sql;
            mySqlCommand.ExecuteNonQuery();
        }

        public override void InsertItem(Calendar item)
        {
            string sql = "INSERT INTO calender (calenderId,DateCreated,Name) VALUES (@cid,@dtcreate,@cname)";

            _command.Parameters.Clear();
            _command.CommandText = sql;
            _command.Parameters.Add(new MySqlParameter("cid", item.CalendarId.ToString()));
            _command.Parameters.Add(new MySqlParameter("dtcreate", item.TimeCreated.ToString()));
            _command.Parameters.Add(new MySqlParameter("cname", item.Name.ToString()));

            _command.ExecuteNonQuery();
        }

        public override void InsertItems(List<Calendar> items)
        {
            throw new NotImplementedException();
        }

        public override void UpdateItem(Calendar item)
        {
            throw new NotImplementedException();
        }
    }
}
