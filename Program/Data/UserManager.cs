using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Program.Model;

namespace Program.Data
{
    public sealed class UserManager
    {
        private User _CurrentUser;

        private static UserManager _Instance = null;

        private List<User> _UserLists = new List<User>();
       
        private UserManager() { }

        public static UserManager GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new UserManager();

            }
            return _Instance;
        }

        public User CurrentUser
        {
            get
            {
                return _CurrentUser;
            }
            private set
            {
                _CurrentUser = value;
            }
        }
        public void AddUser(String username, String password)
        {

            _UserLists.Add(new User(username, password));

        }


        public bool LoginUser(string username, string password)
        {

            foreach (var user in _UserLists)
            {
                if (user.UserName ==username  && user.Password == password)
                {
                    CurrentUser = user;
                    return true;
                }

            }
            return false;

        }
       
        public void Logout()
        {
            _CurrentUser = null;
        }
        public string FindUser(long id)
        {
            
            foreach(var user in _UserLists)
            {
                if (user.UserId == id)
                    return user.UserName;
                
            }
            return "No User found";
        }

    }
}
