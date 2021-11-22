using Kalender.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalender.Model
{
    public class User : ModelBase
    {
        public User()
        {

        }

        private Guid _userId;
        public Guid UserId { get => _userId; set { _userId = value; NotifyPropertyChanged(nameof(UserId)); } }

        private string _name = "";
        public string Name { get => _name; set { _name = value; NotifyPropertyChanged(nameof(Name)); } }

        private string _email = "";
        public string Email { get => _email; set { _email = value; NotifyPropertyChanged(nameof(Email)); } }

        private DateTime _birthDay;
        public DateTime BirthDay { get => _birthDay; set { _birthDay = value; NotifyPropertyChanged(nameof(BirthDay)); } }
    }
}
