using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE
{
    public class WeekGoals
    {
        public String EmailAddress {get; set; }
        [Key]
        public DateTime SundayDate { get; set; }
        public int GoalCarbs { get; set; }
        public int GoalFats { get; set; }
        public int GoalCalories { get; set; }
        public int GoalPortiens { get; set; }
    }
}

