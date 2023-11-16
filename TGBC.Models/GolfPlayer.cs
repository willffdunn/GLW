using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Models;

public class GolfPlayer
{ 
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GPId { get; set; }
    [Required]
    [DisplayName("Player Type")]
    public string GPType { get; set; }
    [DisplayName("Player Name")]
    public string? GPFName { get; set; }
    public string? GPLName { get; set; }
    public int GGId { get; set; }
    [Required]
    [ForeignKey("GGId")]
    [ValidateNever]
    public GolfGroup GolfGroup { get; set; }
    public int MemberId { get; set; }
    [Required]
    [ForeignKey("MemberId")]
    [ValidateNever]
    public Member Member { get; set; }
    public int Tot1 { get; set; }
    public int Tot2 { get; set; }
    public int Tot3{ get; set; }
    public int PTot1 { get; set; }
    public int PTot2 { get; set; }
    public int PTot3 { get; set; }
    public int GTot1 { get; set; }
    public int GTot2 { get; set; }
    public int GTot3 { get; set; }
    public int FTot1 { get; set; }
    public int FTot2 { get; set; }
    public int FTot3 { get; set; }
}
