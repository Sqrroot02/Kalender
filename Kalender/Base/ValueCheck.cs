using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalender.Base
{
    public class ValueCheck
    {
        public ValueCheck()
        {
            Months.Add(1, "January");
            Months.Add(2, "February");
            Months.Add(3, "March");
            Months.Add(4, "April");
            Months.Add(5, "May");
            Months.Add(6, "June");
            Months.Add(7, "July");
            Months.Add(8, "August");
            Months.Add(9, "September");
            Months.Add(10, "October");
            Months.Add(11, "November");
            Months.Add(12, "December");
        }

        public ObservableCollection<int> DaysInMonth(int year, int month)
        {
            if (month > 0 && month <= 12)
            {
                ObservableCollection<int> result = new ObservableCollection<int>();
                int count = DateTime.DaysInMonth(year, month);
                for (int i = 1; i <= count; i++)
                    result.Add(i);
                return result;
            }
            else
                return null;
        }

        public bool IsInt(string obj)
        {
            int i = 0;
            if (int.TryParse(obj, out i))
                return true;
            else
                return false;
        }

        public bool IsMonth(string obj)
        {
            int month = 0;
            if (int.TryParse(obj, out month))
                if (month >= 1 && month <= 12)
                    return true;
                else
                    return false;  
            else
                return false ;
        }

        public bool IsValidYear(string obj)
        {
            int year = 0;
            if (int.TryParse(obj, out year))
                if (year >= 0 && year <= 9999)
                    return true;
                else
                    return false;
            else
                return false;
        }

        public Dictionary<int, string> Months = new Dictionary<int, string>();
    }
}
