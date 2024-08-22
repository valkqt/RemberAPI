using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models.User;

namespace ApplicationCore.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public virtual Deck? Deck { get; set; }
        public Card() { }

        public Card(int id, int userId, string front, string back, Deck? deck)
        {
            Id = id;
            UserId = userId;
            Front = front;
            Back = back;
            Deck = deck;
        }
    }
}
