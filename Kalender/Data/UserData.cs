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
    internal class UserData : DataBase<User>
    {
        public static void InitTables()
        {
            MySqlCommand mySqlCommand = new MySqlCommand();

            mySqlCommand.Connection = DatabaseConnection.Connection;
            mySqlCommand.Parameters.Clear();

            string sql = "CREATE TABLE IF NOT EXISTS User (userId VARCHAR(56) PRIMARY KEY, Name VARCHAR(512), Email VARCHAR(512), BirthDay DATETIME);";
            mySqlCommand.CommandText = sql;

            mySqlCommand.ExecuteNonQuery();
        }
        public override void DeleteItem(User item)
        {
            throw new NotImplementedException();
        }

        public override void DeleteItems(List<User> items)
        {
            throw new NotImplementedException();
        }

        public override User GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public override List<User> GetItems()
        {
            throw new NotImplementedException();
        }

        public override void InsertItem(User item)
        {
            throw new NotImplementedException();
        }

        public override void InsertItems(List<User> items)
        {
            throw new NotImplementedException();
        }

        public override void UpdateItem(User item)
        {
            throw new NotImplementedException();
        }
    }
}
