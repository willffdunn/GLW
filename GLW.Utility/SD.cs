using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class SD
    {
        public const string Role_Admin = "Admin";
        public const string Role_NonMember = "Non Member";
        public const string Role_League_Admin = "League Admin";
        public const string Role_Member_Basic = "League Member (Basic)";
        public const string Role_Member_Premium = "League Member (Premium)";
        public const string Role_Member_PremiumPlus = "League Member (PremiumPlus)";

        public static string APIBase { get; set; }
        public static int LeagueId { get; set; }
        public static string LeagueName { get; set; }
        public static string UserFirstName { get; set; }
        public static string UserLastName { get; set; }
        public static string Email { get; set; }
        public static string MemberPlan { get; set; }

    }
}
