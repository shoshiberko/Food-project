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
        ImpBL myBL;
        public MonthChartUserControlModel()
        {
            myBL = new ImpBL();
        }

        public List<DailyFood> getDailyFoodMonthList(string emailAddress, DateTime sunday)
        {
            List<DailyFood> lst = new List<DailyFood>();
            for (int i = 0; i < 28; i++)
            {
                lst.Add(myBL.getDailyFood(emailAddress, sunday.AddDays(i)));
            }
            return lst;
        }

        public WeekGoals getMonthGoals(string emailAddress, DateTime sunday)
        {
            WeekGoals result = new WeekGoals() { GoalCalories = 0, GoalCarbs = 0, GoalFats = 0, GoalProteins = 0 };
            WeekGoals tmp;
            for (int i=0;i<4;i++)
            {
                tmp= myBL.getWeekGoals(emailAddress, sunday.AddDays(7*i));
                if (tmp != null)
                {
                    result.GoalCalories += tmp.GoalCalories; result.GoalFats += tmp.GoalFats; result.GoalCarbs += tmp.GoalCarbs; result.GoalProteins += tmp.GoalProteins;
                }
            }
            return result;
        }
    }
}
