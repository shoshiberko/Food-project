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
        ImpBL bl;//all the functions use the bl that has the access to DAL function and from there to the db. 
        public AddDailyFoodModel()//constructor
        {
            bl = new ImpBL();
        }
        public void saveMeals(DateTime currentDate, String emailAddress, List<FoodItem> breakfastList , List<FoodItem> brunchList, List<FoodItem> dinnerList, List<FoodItem> snacksList)//get meals details of specific user and save it.
        {
            bl.updateMeals(currentDate, emailAddress, breakfastList, brunchList, dinnerList, snacksList);
        }
        public void saveDailyFood(DailyFood df)//this function get dailyFood and save it in db.
        {
            if (bl.getDailyFood(df.EmailAddress, df.CurrentDate) == null)//if there is no already dailyFood like the parameter dailyFood-add it, else-update it.
                bl.addDailyFood(df);
            else
                bl.updateDailyFood(df);
        }
        public FoodDetails getFoodDetails(string foodKey)//this function get foodKey and return the details of the food that has this key.(return null if does not exist in db)
        {
            return (bl.getFoodDetails(foodKey));
        }

        public DailyFood getDailyFoodByDate(string emailAddressProperty, DateTime selectedDate)//this function get email and date and return the dailyFood of the parameter date.(return null if does not exist in db)
        {
            return bl.getDailyFood(emailAddressProperty, selectedDate);
        }

        public List<FoodItem> getMealsList(string emailAddressProperty, DateTime selectedDate, MEALTIME mealTime)//this function get email and date and mealTome and return the list of foodItems of the specific mealTime of the parameter date.(return null if does not exist in db)
        {
            return bl.getDailyFoodMealList(selectedDate, emailAddressProperty,mealTime);
        }
    }
}
