using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE
{
    public class DailyFood
    {
        public String EmailAddress { get; set; }
        [Key]
        public DateTime CurrentDate { get; set; }
        virtual public List<String> BreakFast { get; set; }
        virtual public List<String> Brunch { get; set; }
        virtual public List<String> Dinner { get; set; }
        virtual public List<String> Snacks { get; set; }
        public MOOD DailyMood { get; set; }
        public ACTIVITY DailyActivity { get; set; }
        public int TotalCarbs { get; set; }
        public int TotalFats { get; set; }
        public int TotalCalories { get; set; }
        public int TotalPortiens { get; set; }
    }
}

