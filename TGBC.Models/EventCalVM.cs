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
    public class EventCalVM
    {
        public int EvId { get; set; }
        public string EvStartDate { get; set; }
        public string EvStartTime { get; set; }
        public string EvEndDate { get; set; }
        public string EvEndTime { get; set; }
        public string EvType { get; set; }
        public string EvShortDescription { get; set; }
        public string EvDescription { get; set; }
    }
}
