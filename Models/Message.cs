using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime When { get; set; }

        public static implicit operator Message(List<Message> v)
        {
            throw new NotImplementedException();
        }

        //public string UserId { get; set; }
        //public virtual AppUser Sender { get; set; }
    }
}
