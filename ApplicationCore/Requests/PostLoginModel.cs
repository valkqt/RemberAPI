using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Utility
{
    public class PostLoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Persist { get; set; }
        public PostLoginModel(string username, string password, bool persist)
        {
            Username = username;
            Password = password;
            Persist = persist;
        }
    }
}
