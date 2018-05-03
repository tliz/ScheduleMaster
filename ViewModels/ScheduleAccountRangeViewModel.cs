using ScheduleManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScheduleManagement.ViewModels
{
    public class ScheduleAccountRangeViewModel
    {
        public IEnumerable<MasterSchedule> MasterSchedules { get; set; }
        public IEnumerable<AccountRange> AccountRanges { get; set; }
    }
}