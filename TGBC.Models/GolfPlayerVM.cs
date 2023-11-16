using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBGC.Models;

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
