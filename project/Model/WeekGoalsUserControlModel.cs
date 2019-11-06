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
        ImpBL bl;//all the functions use the bl that has the access to DAL function and from there to the db. 
        public WeekGoalsUserControlModel()//constructor
        {
            bl = new ImpBL();
        }
        public void setGoals(WeekGoals weekGoals)//this function save the parameter weekGoals in db.if there is already goals to this week-update it, else-add it to the db.
        {
            
            if (bl.getWeekGoals(weekGoals.EmailAddress, weekGoals.SundayDate) != null)
                bl.updateWeekGoals(weekGoals);
            else
                bl.addWeekGoals(weekGoals);
        }

        public WeekGoals getWeekGoals(string emailAddress, DateTime sunday)//this function get email and sunday date and return the weekGoals of this week (return null if does not exist in db).
        {
            return bl.getWeekGoals(emailAddress, sunday);
        }
    }
}
