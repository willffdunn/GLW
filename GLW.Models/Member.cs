using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Models;

public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberId { get; set; }
        [Required]
        public string LastName { get; set; }  
        public string FirstName { get; set; }
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [DefaultValue(false)]
        public bool EmailConfirmed { get; set; }
        [DefaultValue(false)]
        public bool Registered { get; set; }
        public string PreferredNotification { get; set; }

        [DisplayName("Status")]
        public string MemberStatus { get; set; }

        [DisplayName("Member Type")]
        public string MemberType { get; set; }
        [RegularExpression(@"^\(?\d{3}\)?[-.]?\d{3}[-.]?\d{4}$",
            ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        [DisplayName("Member Tee")]
        public string MemberTee { get; set; }

        [DisplayName("Handicap")]
        public int? Handicap { get; set; }
        
        [AllowNull]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
            set { }
        }
        [Required]
        public int LId { get; set; }
        [Required]
        [ForeignKey("LId")]
        [ValidateNever]
        public League League { get; set; }

}















