using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace Models
{
    public class ApplicationUser:IdentityUser   {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name
        {
            get { return FirstName + " " + LastName; }
            set { }
        }

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set;}
        public string Discriminator
        {
            get { return "ApplicationUser"; }
            set { }
        }
    
    }
}
