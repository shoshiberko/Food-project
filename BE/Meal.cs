using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE
{
    public class Meal
    {
        [Key, Column(Order = 0)]
        public String EmailAddress { get; set; }
        [Key, Column(Order = 1)]
        public DateTime CurrentDate { get; set; }
        [Key, Column(Order = 2)]
        public  MEALTIME MealTime { get; set; }
        [Key, Column(Order = 3)]
        public String FoodKey { get; set; }
        public String FoodName { get; set; }
        public int FoodAmount { get; set; }
        public float Calories100Gm { get; set; }
    }
}
