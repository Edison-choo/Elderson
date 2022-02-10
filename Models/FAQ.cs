using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class FAQ
    {
        public string Id { get; set; }

        public string Question { get; set; }

        public string Topic { get; set; }

        public string UserId { get; set; }

        public string FullName { get; set; }
    }
}
