using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


public class GolfRound {
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



}
