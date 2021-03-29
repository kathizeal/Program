using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Model
{
    public class User
    {
        public User(string userName, string password)
        {
            this.UserId = DateTime.Now.Ticks;
            this.UserName = userName;
            this.Password = password;
        }
        public string UserName { get; set; }
        //public string LastName { get; set; }
        public long UserId { get; set; }
        public string Password { get; set; }
        //  public string Email { get; set; }

    }
}
