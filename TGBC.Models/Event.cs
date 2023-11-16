using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;


namespace TBGC.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EvId { get; set; }
        [Required]
        public DateTime EvDate { get; set; } = DateTime.Now;
        [Required]
        [DisplayName("Event Type")]
        public string EvType { get; set; }
        [DisplayName("Event Short Description")]
        public string EvShortDescription { get; set; }

        [DisplayName("Event Description")]
        public string EvDescription { get; set; }
    }
}
