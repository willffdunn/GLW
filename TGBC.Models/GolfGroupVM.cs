using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBGC.Models;

namespace Models
{
    public class GolfGroupVM
    {
        [ValidateNever]
        public List<GolfPlayerVM> GP { get; set; }
        public GolfCourse GC { get; set; }
        
    }
}
