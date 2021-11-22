using Kalender.Base;
using Kalender.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalender.Data
{
    public sealed class NotificationData : DataBase<Notification>
    {
        public static void InitTables()
        {
            MySqlCommand mySqlCommand = new MySqlCommand();

            mySqlCommand.Connection = DatabaseConnection.Connection;
            mySqlCommand.Parameters.Clear();

            string sql = "CREATE TABLE IF NOT EXISTS Message (messageId VARCHAR(56) PRIMARY KEY, MessageTime DATETIME, appointmentId VARCHAR(56));";
            mySqlCommand.CommandText = sql;

            mySqlCommand.ExecuteNonQuery();
        }

        public override void DeleteItem(Notification item)
        {
            throw new NotImplementedException();
        }

        public override void DeleteItems(List<Notification> items)
        {
            throw new NotImplementedException();
        }

        public override Notification GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Notification> GetItems()
        {
            throw new NotImplementedException();
        }

        public override void InsertItem(Notification item)
        {
            throw new NotImplementedException();
        }

        public override void InsertItems(List<Notification> items)
        {
            throw new NotImplementedException();
        }

        public override void UpdateItem(Notification item)
        {
            throw new NotImplementedException();
        }
    }
}
