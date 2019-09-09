using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BE
{
    public class FoodContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<DailyFood> DailyFoods { get; set; }
        public DbSet<WeekGoals> WeekGoals { get; set; }
        public DbSet<Meal> Meals { get; set; }
    }
}

