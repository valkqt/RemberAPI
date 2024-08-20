using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.User
{
    public class BaseUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public BaseUser(int id, string username, string email, DateTime createdAt)
        {
            Id = id;
            Username = username;
            Email = email;
            CreatedAt = createdAt;
        }


    }
}
