using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModels;
using TBGC.Models;

namespace Models
{
    public class EventVM
    {
        public Event Event { get; set; }
        public IEnumerable <SelectListItem>  AvailableRecipients { get; set; }
        public string EmailSender { get; set; }
        public List<string> EmailRecipients { get; set; }
        public string? EmailMessage { get; set; }
        public IEnumerable<SelectListItem> AvailableParticipants { get; set; }
        public List<EventParticipant> Participants { get; set; }
        public List<string>? EventParticipants { get; set; }
        public string? GuestFName { get; set; }
        public string? GuestLName { get; set; }
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            ErrorMessage = "Invalid email address.")]
        public string? GuestEmail { get; set; }
        [RegularExpression(@"^\(?\d{3}\)?[-.]?\d{3}[-.]?\d{4}$",
            ErrorMessage = "Invalid phone number.")]
        public string? GuestPhone { get; set; }

    }
}

