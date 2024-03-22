using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EvId { get; set; }
        public DateTime EvDate { get; set; } = DateTime.Now;
        public TimeSpan EvTime { get; set; }
        public DateTime EvEndDate { get; set; } = DateTime.Now;
        public TimeSpan EvEndTime { get; set; }
        public string EvRegMethod { get; set; }
        public DateTime EvRegStartDate { get; set; } = DateTime.Now;
        public TimeSpan EvRegStartTime { get; set; }
        public DateTime EvRegEndDate { get; set; } = DateTime.Now;
        public TimeSpan EvRegEndTime { get; set; }
        [Required]
        [DisplayName("Event Location")]
        public string EvLocation { get; set; }
        public string EvContactFName { get; set; }
        public string EvContactLName { get; set; }
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            ErrorMessage = "Invalid email address.")]
        public string EvContactEmail { get; set; }
        [RegularExpression(@"^\(?\d{3}\)?[-.]?\d{3}[-.]?\d{4}$",
            ErrorMessage = "Invalid phone number.")]
        public string EvContactPhoneNumber { get; set; }
        public string EvFeeAmount { get; set; }
        public string EvRegComment { get; set; }
        [Required]
        [DisplayName("Event Type")]
        public string EvType { get; set; }
        [DisplayName("Event Short Description")]
        public string EvShortDescription { get; set; }
        [DisplayName("Event Description")]
        public string EvDescription { get; set; }
        [Required]
        public int LId { get; set; }
        [Required]
        [ForeignKey("LId")]
        [ValidateNever]
        public League League { get; set; }
    }
}
