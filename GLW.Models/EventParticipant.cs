using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models
{
    public class EventParticipant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EPId { get; set; }
        [Required]
        public int EvId { get; set; }
        [Required]
        [ForeignKey("EvId")]
        [ValidateNever]
        public Event Event { get; set; }
        public int MemberId { get; set; }
        public string EPType { get; set; }
        public string EPLName { get; set; }
        public string EPFName { get; set; }
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            ErrorMessage = "Invalid email address.")]
        public string EPEmail { get; set; }
        [RegularExpression(@"^\(?\d{3}\)?[-.]?\d{3}[-.]?\d{4}$",
            ErrorMessage = "Invalid phone number.")]
        public string EPPhoneNumber { get; set; }
        [Required]
        public int LId { get; set; }

    }
}
