using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace DAL
{
    public interface IDal
    {
        bool addUser(User user);
        bool updateUser(User user);
        bool removeUser(String emailAddress);
        User getUser(String emailAddress);
        List<User> getAllUsers();
        List<User> getUsersByPredicate(Func<User, bool> predicate);

        bool addDailyFood(DailyFood dailyFood);
        bool updateDailyFood(DailyFood dailyFood);
        bool removeDailyFood(string emailAddress, DateTime currentDate);
        DailyFood getDailyFood(string emailAddress,DateTime currentDate);
        List<DailyFood> getAllDailyFoods();
        List<DailyFood> getDailyFoodsByPredicate(Func<DailyFood, bool> predicate);

        bool addWeekGoals(WeekGoals weekGoals);
        bool updateWeekGoals(WeekGoals weekGoals);
        bool removeWeekGoals(DateTime sundayDate);
        WeekGoals getWeekGoals(DateTime sundayDate);
        List<WeekGoals> getAllWeekGoals();
        List<WeekGoals> getWeekGoalsByPredicate(Func<WeekGoals, bool> predicate);

        bool addMeal(Meal meal);
        bool updateMeals(DateTime currentDate, String emailAddress, List<FoodItem> breakfast, List<FoodItem> brunch , List<FoodItem> dinner, List<FoodItem> snacks );
        List<Meal> getMeal(DateTime currentDate, String emailAddress);
        List<Meal> getAllMeals();
        List<Meal> getMealsByPredicate(Func<Meal, bool> predicate);
        List<Meal> getListOfMeal(DateTime currentDate, String emailAddress, MEALTIME mealTime);

        List<FoodItem> getListFoodItems(String food);
        FoodDetails getFoodDetails(string keyFood);
    }
}
