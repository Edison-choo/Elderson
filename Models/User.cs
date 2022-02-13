using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elderson.Models
{
    public class User
    {
        public string Id { get; set; }
        [Required, RegularExpression(@"^([a-zA-Z0-9]+)([\.{1}])?([a-zA-Z0-9]+)\@(?:gmail|GMAIL)([\.])(?:com|COM)$", ErrorMessage ="The email format is invalid. Only gmail is allowed.")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        [Required]
        public string Fullname { get; set; }
        public string Gender { get; set; }
        [Required]
        public string UserType { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PasswordSalt { get; set; }
        public string CountryCode { get; set; }
        public string IsVerified { get; set; }
    }
}
