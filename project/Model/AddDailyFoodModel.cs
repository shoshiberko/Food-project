using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;
namespace project.Model
{

    public class AddDailyFoodModel
    {
        ImpBL bl;
        public AddDailyFoodModel()
        {
            bl = new ImpBL();
        }
        public void saveMeals(DateTime currentDate, String emailAddress, List<FoodItem> breakfastList , List<FoodItem> brunchList, List<FoodItem> dinnerList, List<FoodItem> snacksList)
        {
            bl.updateMeals(currentDate, emailAddress, breakfastList, brunchList, dinnerList, snacksList);
        }
        public void saveDailyFood(DailyFood df)
        {
            if (bl.getDailyFood(df.EmailAddress, df.CurrentDate) == null)
                bl.addDailyFood(df);
            else
                bl.updateDailyFood(df);
        }
        public FoodDetails getFoodDetails(string foodKey)
        {
            return (bl.getFoodDetails(foodKey));
        }

        public DailyFood getDailyFoodByDate(string emailAddressProperty, DateTime selectedDate)
        {
            return bl.getDailyFood(emailAddressProperty, selectedDate);
        }

        public List<FoodItem> getMealsList(string emailAddressProperty, DateTime selectedDate, MEALTIME mealTime)
        {
            return bl.getDailyFoodMealList(selectedDate, emailAddressProperty,mealTime);
        }
    }
}
