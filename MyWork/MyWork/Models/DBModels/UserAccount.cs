using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Models.DBModels
{
    public class UserAccount : Entity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsAdmin { get; set; }
    }
}
