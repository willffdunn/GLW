using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

public class GolfCourse
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GCId { get; set; }
    [Required]
    [DisplayName("Course Name")]
    public string GCName { get; set; }
    [Required]
    [DisplayName("Street")]
    public string GCStreet { get; set; }
    [Required]
    [DisplayName("City")]
    public string GCCity { get; set; }
    [Required]
    [DisplayName("State")]
    public string GCState { get; set; }
    [Required]
    [DisplayName("Zip")]
    [RegularExpression(@"^\d{5}$",
        ErrorMessage = "Invalid zip code")]
    public string GCZip { get; set; }
    [Required]
    [DisplayName("Course Phone Number")]
    [RegularExpression(@"^\(?\d{3}\)?[-.]?\d{3}[-.]?\d{4}$",
            ErrorMessage = "Invalid phone number.")]
    public string GCPhoneNumber { get; set; }
    [Required]
    [DisplayName("Number of Holes")]
    public int GCNbrHoles { get; set; }
    [Required]
    [DisplayName("Course Type")]
    public string GCType { get; set; }

    [DisplayName("Comments / Special Instructions")]
    public string? GCComments { get; set; } = "";

    public string? GCName1 { get; set; } = "";

    public string? GCName2 { get; set; } = "";

    public string? GCName3 { get; set; } = "";
    [Required]
    public string GCT1 { get; set; }
    [Required]
    public string GCT2 { get; set; }
    [Required]
    public string GCT3 { get; set; }
    [Required]
    public string GCT4 { get; set; }
    [Required]
    public string GCT5 { get; set; }
    public string GCC1 { get; set; }
    [Required]
    public string GCC2 { get; set; }
    [Required]
    public string GCC3 { get; set; }
    [Required]
    public string GCC4 { get; set; }
    [Required]
    public string GCC5 { get; set; }
    public int GCT1TotIn { get; set; }
    public int GCT2TotIn { get; set; }
    public int GCT3TotIn { get; set; }
    public int GCT4TotIn { get; set; }
    public int GCT5TotIn { get; set; }
    public int GCT1TotOut { get; set; }
    public int GCT2TotOut { get; set; }
    public int GCT3TotOut { get; set; }
    public int GCT4TotOut { get; set; }
    public int GCT5TotOut { get; set; }
    public int GCT1Tot { get; set; }
    public int GCT2Tot { get; set; }
    public int GCT3Tot { get; set; }
    public int GCT4Tot { get; set; }
    public int GCT5Tot { get; set; }
    public int GCParIn { get; set; }
    public int GCParOut { get; set; }
    public int GCParTot { get; set; }
}
