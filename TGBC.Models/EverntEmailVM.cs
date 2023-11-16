using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;

using TBGC.Models;

namespace Models
{
    public class EventEmailVM
    {
        public Event Event { get; set; }
        public IEnumerable <SelectListItem>  AvailableRecipients { get; set; }
        public List<string> SelectedRecipients { get; set; }
        public string EmailMessage { get; set; }
    }
}

