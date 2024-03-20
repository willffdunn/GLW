using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using TBGC.Models;

namespace Models
{
    public class GPScoreVM
    {
        [ValidateNever]
        public List<GolfPlayerScore> GPS { get; set; }
        public GolfPlayer GP { get; set; }
        public GolfCourse GC { get; set; }
        public GolfGroup GG { get; set; }
       
    }
}
