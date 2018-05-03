using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScheduleManagement.Models
{
    public class MasterSchedule
    {
        public int MasterScheduleID { get; set; }

        [Required]
        public string ScheduleID { get; set; }

        [Required]
        public string ScheduleDescription { get; set; }

        public Boolean HasReport { get; set; }

        public int? MinorScheduleID { get; set; }

        public ICollection<MasterSchedule> MinorSchedules { get; set; }
        public ICollection<AccountRange> AccountRanges { get; set; }
    }
}