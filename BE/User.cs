using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BE
{
    public class User
    {
        [Key]
        public String EmailAddress { get; set; }
        public String Password { get; set; }
        public GENDER Gender{ get; set; }
        public DateTime BirthDate { get; set; }
        public FAMILYSTATUS FamilyStatus { get; set; }
        public int Height { get; set; }
        public float CurrentWeight { get; set; }
        public float GoalWeight { get; set; }
        public DateTime LastUpdateCurrentWeight{ get; set; }
        public float Age { get; set; }
    }
}
