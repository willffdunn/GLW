﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBGC.Models;

namespace Models
{
    public class GGScoreVM
    {
        [ValidateNever]
        public List<GPScoreVM> GGS { get; set; }
    }
}
