using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BE;
using BL;
namespace project.Model
{
    public class WeekGoalsUserControlModel
    {
        ImpBL bl;
        public WeekGoalsUserControlModel()
        {
            bl = new ImpBL();
        }
        public void setGoals(WeekGoals weekGoals)
        {
            /*******************************************
            if (bl.getWeekGoals(weekGoals.EmailAddress, weekGoals.SundayDate) != null)
                bl.updateWeekGoals(weekGoals);
            else
                bl.addWeekGoals(weekGoals);*/
        }

        public WeekGoals getWeekGoals(string emailAddress, DateTime sunday)
        {
            //return bl.getWeekGoals(emailAddress, sunday);***********************************
            return new WeekGoals() { SundayDate = DateTime.Now, GoalCalories = 100, GoalCarbs = 200, GoalProteins = 300, GoalFats = 1500 };
        }
    }
}
