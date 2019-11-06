using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace project.Model
{
    public class UserControlChartModel
    {
        ImpBL myBL;//all the functions use the bl that has the access to DAL function and from there to the db. 
        public UserControlChartModel()//constructor
        {
            myBL = new ImpBL();
        }

        public List<DailyFood> getDailyFoodWeekList(string emailAddress, DateTime sunday)//this function get email and sunday date and return list of dailyFood of all the week days.
        {
            List<DailyFood> lst=new List<DailyFood>();
            for (int i = 0; i < 7; i++)//go over 7 days in week
            {
                lst.Add(myBL.getDailyFood(emailAddress, sunday.AddDays(i)));
            }
            return lst;
        }

        public WeekGoals getWeekGoals(string emailAddress, DateTime sunday)//this function get email and sunday date and returnweekGoals of the sunday week.
        {
            return myBL.getWeekGoals(emailAddress, sunday);
        }
    }
}
