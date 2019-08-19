using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace BL
{
    public class ImpBL
    {
        IDal myDal;
        public ImpBL()
        {
            myDal = new ImpDal();
        }
        #region Users
        public bool addUser(User user)
        {
            return myDal.addUser(user);
        }
        public bool updateUser(User user)
        {
            return myDal.updateUser(user);
        }
        public bool removeUser(String emailAddress)
        {
            return myDal.removeUser(emailAddress);
        }
        public User getUser(String emailAddress)
        {
            return myDal.getUser(emailAddress);
        }
        public List<User> getAllUsers()
        {
            return myDal.getAllUsers();
        }
        public  List<User> getUsersByPredicate(Func<User, bool> predicate)
        {
            return myDal.getUsersByPredicate(predicate);
        }
        #endregion
        #region DailyFoods
        public bool addDailyFood(DailyFood dailyFood)
        {
            return myDal.addDailyFood(dailyFood);
        }
        public bool updateDailyFood(DailyFood dailyFood)
        {
            return myDal.updateDailyFood(dailyFood);
        }
        public bool removeDailyFood(string emailAddress, DateTime currentDate)
        {
            return myDal.removeDailyFood(emailAddress,currentDate);
        }
        public DailyFood getDailyFood(string emailAddress, DateTime currentDate)
        {
            return myDal.getDailyFood(emailAddress, currentDate);
        }
        public List<DailyFood> getAllDailyFoods()
        {
            return myDal.getAllDailyFoods();
        }
        public List<DailyFood> getDailyFoodsByPredicate(Func<DailyFood, bool> predicate)
        {
            return myDal.getDailyFoodsByPredicate(predicate);
        }
        public List<FoodItem> getDailyFoodMealList(DateTime currentDate, String emailAddress, MEALTIME mealTime)
        {
            List<FoodItem> result = new List<FoodItem>();
            foreach (var item in  myDal.getListOfMeal(currentDate, emailAddress, mealTime))
            {
                result.Add(new FoodItem() { Key = item.FoodKey, Name = item.FoodName, AmountGm = item.FoodAmount, Calories100G = item.Calories100Gm });
            }
            return result;
        }
        #endregion
        #region WeekGoals
        public bool addWeekGoals(WeekGoals weekGoals)
        {
            return myDal.addWeekGoals(weekGoals);
        }
        public bool updateWeekGoals(WeekGoals weekGoals)
        {
            return myDal.updateWeekGoals(weekGoals);
        }
        public bool removeWeekGoals(DateTime sundayDate)
        {
            return myDal.removeWeekGoals(sundayDate);
        }
        public WeekGoals getWeekGoals(DateTime sundayDate)
        {
            return myDal.getWeekGoals(sundayDate);
        }
        public List<WeekGoals> getAllWeekGoals()
        {
            return myDal.getAllWeekGoals();
        }
        public List<WeekGoals> getWeekGoalsByPredicate(Func<WeekGoals, bool> predicate)
        {
            return myDal.getWeekGoalsByPredicate(predicate);
        }

        #endregion
        #region Meals
        public bool addMeal(Meal meal)
        {
            return myDal.addMeal(meal);
        }
        public bool updateMeals(DateTime currentDate, String emailAddress, List<FoodItem> breakfast, List<FoodItem> brunch, List<FoodItem> dinner, List<FoodItem> snacks)
        {
            return myDal.updateMeals(currentDate,emailAddress, breakfast,brunch, dinner, snacks);
        }
        public List<Meal> getMeal(DateTime currentDate, String emailAddress)
        {
           return  myDal.getMeal(currentDate,emailAddress);
        }
        public List<Meal> getAllMeals()
        {
           return myDal.getAllMeals();
        }
        public List<Meal> getMealsByPredicate(Func<Meal, bool> predicate)
        {
            return myDal.getMealsByPredicate(predicate);
        }
        #endregion
        #region  API
        public List<FoodItem> getListFoodItems(String food)
        {
            return myDal.getListFoodItems(food);
        }
        public FoodDetails getFoodDetails(string keyFood)
        {
            return myDal.getFoodDetails(keyFood);
        }
        #endregion
    }
}
