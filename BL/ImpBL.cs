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
        float calculateBMI(float weightKG, float heightMeter)
        {
            return weightKG / (heightMeter * heightMeter);
        }
        public bool addUser(User user)
        {
            float goalBmi = calculateBMI(user.GoalWeight, user.Height / 100);
            float currentBmi = calculateBMI(user.CurrentWeight, user.Height / 100);
            if (currentBmi < 18.5 && goalBmi < 18.5 && goalBmi < currentBmi)
                throw new Exception("Cannot add you, your BMI is too low and your goal's BMI is lower:(");
            if (currentBmi > 25 && goalBmi > 25 && goalBmi > currentBmi)
                throw new Exception("Cannot add you, your BMI is too high and your goal's BMI is higher:(");
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
        public bool removeWeekGoals(string emailAddress,DateTime sundayDate)
        {
            return myDal.removeWeekGoals(emailAddress,sundayDate);
        }
        public WeekGoals getWeekGoals(String emailAddress, DateTime sundayDate)
        {
            return myDal.getWeekGoals(emailAddress,sundayDate);
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
