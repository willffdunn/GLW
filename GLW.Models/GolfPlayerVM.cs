using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models
{
    public class GolfPlayerVM
    {
        [ValidateNever]
        public Member Member { get; set; }
        public int GGId { get; set; }
        public int GRId { get; set; }
    }
}
