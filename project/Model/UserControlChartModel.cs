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
        ImpBL myBL;
        public UserControlChartModel()
        {
            myBL = new ImpBL();
        }

        public List<DailyFood> getDailyFoodWeekList(string emailAddress, DateTime sunday)
        {
            List<DailyFood> lst=new List<DailyFood>();
            for (int i = 0; i < 7; i++)
            {
                lst.Add(myBL.getDailyFood(emailAddress, sunday.AddDays(i)));
            }
            return lst;
        }
    }
}
