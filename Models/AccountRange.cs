using System.ComponentModel.DataAnnotations;

namespace ScheduleManagement.Models
{
    public class AccountRange
    {
        public int AccountRangeID { get; set; }

        [Range(001, 999, ErrorMessage = "Enter number between 001 to 999")]
        public int CostCenterFrom { get; set; }

        [Range(001, 999, ErrorMessage = "Enter number between 001 to 999")]
        public int CostCenterTo { get; set; }

        [Range(100000, 999999, ErrorMessage = "Enter number between 100000 to 999999")]
        public int NaturalAccountFrom { get; set; }

        [Range(100000, 999999, ErrorMessage = "Enter number between 100000 to 999999")]
        public int NaturalAccountTo { get; set; }

        [System.ComponentModel.DefaultValue(1)]
        public int Org { get; set; }

        public int MasterScheduleID { get; set; }

        public MasterSchedule MasterSchedule { get; set; }
    }
}