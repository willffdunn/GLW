using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace TBGC.Models
{
    public class League
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LId { get; set; }
        [Required]
        [DisplayName("League Name")]
        public string LeagueName { get; set; }
        [Required]
        [DisplayName("Street")]
        public string LStreet { get; set; }
        [Required]
        [DisplayName("City")]
        public string LCity { get; set; }
        [Required]
        [DisplayName("State")]
        public string LState { get; set; }
        [Required]
        [DisplayName("Zip")]
        [RegularExpression(@"^\d{5}$",
        ErrorMessage = "Invalid zip code")]
        public string LZip { get; set; }
        public string LContactFName { get; set; }
        public string LContactLName { get; set; }
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            ErrorMessage = "Invalid email address.")]
        public string LContactEmail { get; set; }
        [RegularExpression(@"^\(?\d{3}\)?[-.]?\d{3}[-.]?\d{4}$",
            ErrorMessage = "Invalid phone number.")]
        public string LContactPhoneNumber { get; set; }
        [Required]
        public DateTime LSeasonStart { get; set; } = DateTime.Now;
        public DateTime LSeasonEnd { get; set; } = DateTime.Now;
        [DisplayName("League Description")]
        public string LDescription { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The password must be at least 8 characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
