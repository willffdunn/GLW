using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class GGScoreVP
    {
        public GolfPlayerScore GPS { get; set; }
        public GolfPlayer GP { get; set; }
        public GolfCourse GC { get; set; }
        public GolfGroup GG { get; set; }
    }
}
