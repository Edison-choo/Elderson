using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public string Text { get; set; }
        public DateTime When { get; set; }

        public string UserId { get; set; }
        public string ToUserId { get; set; }
        public string Read { get; set; }

        //public string UserId { get; set; }
        //public virtual AppUser Sender { get; set; }
    }
}
