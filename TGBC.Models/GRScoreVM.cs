using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models
{
    public class GRScoreVM
    {
        [ValidateNever]
        public int GPId { get; set; }
        public int MemberId { get; set; }
        public string GPFName { get; set; }
        public string GPLName { get; set; }
        public string GPType { get; set; }
        public string GGName { get; set; }
        public TimeSpan GGTime { get; set; }
        public int GScore { get; set; }
            
    }
}

