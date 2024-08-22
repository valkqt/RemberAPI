using ApplicationCore.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class Deck
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public bool IsPrivate { get; set; }
        public string Scope { get; set; }
        public Deck(int id, string name, int userId, bool isPrivate, string scope)
        {
            Id = id;
            Name = name;
            UserId = userId;
            IsPrivate = isPrivate;
            Scope = scope;

        }
    }
}
