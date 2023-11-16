using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class GolfCourseHole
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GHId { get; set; }
    [Required]
    public int GCId { get; set; }
    [ForeignKey("GCId")]
    [ValidateNever]
    public GolfCourse GolfCourse { get; set; }
    public string GCName { get; set; }
    [Required]
    [DisplayName("Course Section")]
    public string GHSectName { get; set; }
    [Required]
    [DisplayName("Hole Number")]
    [Range(1,27,ErrorMessage = "Invalid Hole Number")]
    public int GHHole { get; set; }
    [Required]
    [DisplayName("Handicap")]
    [Range(1, 18, ErrorMessage = "Invalid Handicap")]
    public int GHHandicap { get; set; }
    [Required]
    [DisplayName("Par")]
    [Range(3, 5, ErrorMessage = "Invalid Par")]
    public int GHPar { get; set; }
    public int GHT1 { get; set; }
    public int GHT2 { get; set; }
    public int GHT3 { get; set; }
    public int GHT4 { get; set; }
    public int GHT5 { get; set; }

}