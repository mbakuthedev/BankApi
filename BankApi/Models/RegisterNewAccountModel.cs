using System;
using System.ComponentModel.DataAnnotations;

namespace BankApi.Models
{
    public class RegisterNewAccountModel
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public AccountType AccountType { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{4}$",ErrorMessage ="Pin must be 4 digits")] //4 digit pin
        public string Pin { get; set; }
        [Required]
        [Compare("Pin", ErrorMessage ="Pins dont match")]
        public string ConfirmedPin { get; set; }
    }
}