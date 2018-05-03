using ScheduleManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScheduleManagement.ViewModels
{
    public class MasterScheduleViewModel
    {
        public int MS_VM_ID { get; set; }
        public string ScheduleID { get; set; }
        public string ScheduleDescription { get; set; }
        public Boolean HasReport { get; set; }

        [DisplayFormat(NullDisplayText = "No Parent")]
        public string MinorScheduleDescription { get; set; }

        public ICollection<AccountRange> AccountRanges { get; set; }
    }
}