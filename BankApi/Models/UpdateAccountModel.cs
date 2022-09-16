using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models
{
    public class UpdateAccountModel
    {
        [Key]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]/d[4]$", ErrorMessage = "Pin must be 4 digits")] //4 digit pin
        public string Pin { get; set; }
        [Required]
        [Compare("Pin", ErrorMessage = "Pins dont match")]
        public string ConfirmedPin { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
    }
}
