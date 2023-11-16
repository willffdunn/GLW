using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class GolfPlayerScore
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GPSId { get; set; }
    [Required]
    public int GPId { get; set; }
    public int GHId { get; set; }
    [Required]
    [ForeignKey("GHId")]
    [ValidateNever]
    public GolfCourseHole GolfCourseHole { get; set; }
    [Required]
    [DisplayName("Strokes")]
    public int GPSStrokes { get; set; }
    [DisplayName("Putts")]
    [Required]
    public int GPSPutts { get; set; }
    [DisplayName("GIY")]
    public string? GPSGIY { get; set; }
    [DisplayName("Fairway")]
    public string? GPSFW { get; set; }

}