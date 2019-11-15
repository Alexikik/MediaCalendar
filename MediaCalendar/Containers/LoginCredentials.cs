using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCalendar.Containers
{
    public class LoginCredentials
    {
        public string username { get; set; }
        public string password { get; set; }

        public LoginCredentials(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
