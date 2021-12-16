using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Kalender.Base
{
    /// <summary>
    /// Basisklasse für jede Datensteuerungsklasse
    /// </summary>
    /// <typeparam name="T">Der Typ der zur Steuernden Daten</typeparam>
    public abstract class DataBase<T>
    {
        public DataBase()
        {
            Command = new MySqlCommand();
        }
        public MySqlCommand Command { get; set; }

        /// <summary>
        /// Fügt ein Item der Datenbank hinzu
        /// </summary>
        /// <param name="item"></param>
        public abstract void InsertItem(T item);

        /// <summary>
        /// Fügt mehrere Items der Datenbank hinzu
        /// </summary>
        /// <param name="items"></param>
        public abstract void InsertItems(List<T> items);

        /// <summary>
        /// Aktualisiert ein Item
        /// </summary>
        /// <param name="item"></param>
        public abstract void UpdateItem(T item);

        /// <summary>
        /// Löscht ein Item
        /// </summary>
        /// <param name="item"></param>
        public abstract void DeleteItem(T item);

        /// <summary>
        /// Löscht mehrere Items
        /// </summary>
        /// <param name="items"></param>
        public abstract void DeleteItems(List<T> items);

        /// <summary>
        /// Giebt ein bestimmtes Item nach seiner ID wieder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract T GetItem(int id);

        /// <summary>
        /// Giebt alle Items zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract List<T> GetItems();

        // MySQL besitzt kein bool, daher werden bools als Byte und in SQL als TinyInt 1 oder 0 gespeichert
        public virtual byte ConvertBoolToTinyInt(bool b)
        {
            if (b)
                return 1;
            else
                return 0;
        }

        public virtual bool ConvertTinyIntToBool(byte val)
        {
            if (val == 1)
                return true;
            else
                return false;
        }
    }
}
