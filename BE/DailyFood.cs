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
        [Key, Column(Order = 0)]
        public String EmailAddress { get; set; }
        [Key, Column(Order = 1)]
        public DateTime CurrentDate { get; set; }
        public MOOD DailyMood { get; set; }
        public ACTIVITY DailyActivity { get; set; }
        public float TotalCarbs { get; set; }
        public float TotalFats { get; set; }
        public float TotalCalories { get; set; }
        public float TotalPortiens { get; set; }
        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }
    }
}

