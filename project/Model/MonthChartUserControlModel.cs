using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace project.Model
{
    public class MonthChartUserControlModel
    {
        ImpBL myBL;//all the functions use the bl that has the access to DAL function and from there to the db. 
        public MonthChartUserControlModel()//constructor
        {
            myBL = new ImpBL();
        }

        public List<DailyFood> getDailyFoodMonthList(string emailAddress, DateTime sunday)//this function get email and sundayDtae and return list of dailyFood of all this sunday month days
        {
            List<DailyFood> lst = new List<DailyFood>();
            for (int i = 0; i < 28; i++)//get all the week dailtFood
            {
                lst.Add(myBL.getDailyFood(emailAddress, sunday.AddDays(i)));
            }
            return lst;
        }

        public WeekGoals getMonthGoals(string emailAddress, DateTime sunday)//this function get email and sunday date and return this month goals(ass of all the weekGoals of the weeks of this month).
        {
            WeekGoals result = new WeekGoals() { GoalCalories = 0, GoalCarbs = 0, GoalFats = 0, GoalProteins = 0 };
            WeekGoals tmp;
            for (int i=0;i<4;i++)//go over 4 week in month
            {
                tmp= myBL.getWeekGoals(emailAddress, sunday.AddDays(7*i));
                if (tmp != null)//if there is weekGoals to this week
                {
                    result.GoalCalories += tmp.GoalCalories; result.GoalFats += tmp.GoalFats; result.GoalCarbs += tmp.GoalCarbs; result.GoalProteins += tmp.GoalProteins;
                }
            }
            return result;
        }
    }
}
