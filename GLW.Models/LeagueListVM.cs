using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LeagueListVM
    {
        public int LId { get; set; }
        public string LeagueName { get; set; }
        public int MemberId { get; set; }
        public string LastName { get; set; }
        public bool Registered { get; set; }
        public string FirstName { get; set; }
        [RegularExpression(@"^\(?\d{3}\)?[-.]?\d{3}[-.]?\d{4}$",
            ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }


    }
}
