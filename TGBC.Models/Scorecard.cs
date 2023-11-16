using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

public class Scorecard
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SCId { get; set; }
    [Required]
    [ForeignKey("GCId")]
    public int GCIdFK { get; set; }
    [Required]
    [DisplayName("Course Section")]
    public string GCSectName { get; set; }
    [Required]
    public string SCRowType { get; set; }
    public string GCTee { get; set; }
    [Required]
    [DisplayName("Hole Number")]
    [Range(1, 27, ErrorMessage = "Invalid Hole Number")]
    public int GHHole { get; set; }
    [Required]
    [DisplayName("Handicap")]
    [Range(1, 18, ErrorMessage = "Invalid Handicap")]
    public int GHHandicap { get; set; }
    [Required]
    [DisplayName("Par")]
    [Range(3, 5, ErrorMessage = "Invalid Par")]
    public int GHPar { get; set; }

    public string GCName { get; set; }



}