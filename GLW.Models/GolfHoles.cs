using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

public class GolfCourse
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GHId { get; set; }
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
    [AllowNull]
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
    [DisplayName("Course Type")]
    public string GCType { get; set; }
    [AllowNull]
    [DisplayName("Comments / Special Instructions")]
    public string GCComments { get; set; }

}