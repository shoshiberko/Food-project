﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class FoodItem
    {
        public String Key { get; set; }
        public String Name { get; set; }
        public int AmountGm { get; set; }
        public float Calories100G { get; set; }
        public FoodItem()
        {
            AmountGm = 100;
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
