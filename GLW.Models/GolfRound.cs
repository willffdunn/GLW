using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models
{
    public class GolfRound
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GRId { get; set; }
        [Required]
        public int GCId { get; set; }
        [ForeignKey("GCId")]
        [ValidateNever]
        public GolfCourse GolfCourse { get; set; }
        [Required]
        [DisplayName("Date")]
        public DateTime GRDate { get; set; } = DateTime.Now;
        [Required]
        [DisplayName("Type")]
        public string GRType { get; set; }
        [Required]
        public int LId { get; set; }
        [Required]
        [ForeignKey("LId")]
        [ValidateNever]
        public League League { get; set; }

    }


}
