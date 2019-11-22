using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCalendar.Users
{
    public class ApplicationUser : IdentityUser<int>
    {
        [MaxLength(30)]
        public string FullName { get; set; }

        [MaxLength(14)]
        public string BankAccount { get; set; }

        public string Role { get; set; }
    }
}
