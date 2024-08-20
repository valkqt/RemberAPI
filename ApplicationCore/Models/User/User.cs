using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.User
{
    public class User : BaseUser
    {

        public string? PasswordHash { get; set; }
        public bool? EmailVerified { get; set; }


        public User(int id, string username, string email, string passwordHash, bool emailVerified, DateTime createdAt) : base(id, username, email, createdAt)
        {
            PasswordHash = passwordHash;
            EmailVerified = emailVerified;
        }
    }


}
