using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class CartItem
    {
        public string Id { get; set; }
        public string ItemName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Clinic { get; set; }
        public string BookDateTime { get; set; }
        public string Symptoms { get; set; }
        public string DoctorID { get; set; }
    }
}
