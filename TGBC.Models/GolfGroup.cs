using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class GolfGroup
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GGId { get; set; }
    [Required]
    [DisplayName("Tee Time")]
    public TimeSpan GGTtime { get; set; }
    [Required]
    [DisplayName("Group Name")]
    public string GGName { get; set; }
    [Required]
    public int GRId { get; set; }
    [Required]
    [ForeignKey("GRId")]
    [ValidateNever]
    public GolfRound GolfRound { get; set; }
 

}
