using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Xml;
using System.Net;
using System.IO;
using System.Data.Entity.Migrations;
using System.Data.Entity;

namespace DAL
{
    public class ImpDal : IDal
    {
        IAPIFood apiFood = new ImpAPIFood();
        public float DecreaseProteins { get; set; }
        public float DecreaseFats { get; set; }
        public float DecreaseCarbs { get; set; }
        public bool addWeekGoals(WeekGoals weekGoals)
        {
            try
            {
                using (var db = new FoodContext())
                {
                    db.WeekGoals.Add(weekGoals);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e) { return false; }
        }
        public bool addDailyFood(DailyFood dailyFood)
        {
            try
            {
                using (var db = new FoodContext())
                {
                    db.DailyFoods.Add(dailyFood);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e) { return false; }
        }

        public bool addMeal(Meal meal)
        {
            try
            {
                using (var db = new FoodContext())
                {
                    db.Meals.Add(meal);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e) { return false; }
        }

        public bool addUser(User user)
        {
            try
            {
                using (var db = new FoodContext())
                {
                    if (getUser(user.EmailAddress) != null)
                        throw new Exception("User already has an acount");

                    db.Users.Add(user);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e) { throw e; }
        }

      
        public List<DailyFood> getAllDailyFoods()
        {
            using (var db = new FoodContext())
            {
                return (from b in db.DailyFoods
                        select b).ToList();
            }
        }

        public List<Meal> getAllMeals()
        {
            using (var db = new FoodContext())
            {
                return (from b in db.Meals
                        select b).ToList();
            }
        }

        public List<User> getAllUsers()
        {
            using (var db = new FoodContext())
            {
                return (from b in db.Users
                        select b).ToList();
            }
        }
        public List<WeekGoals> getAllWeekGoals()
        {
            using (var db = new FoodContext())
            {
                return (from b in db.WeekGoals
                        select b).ToList();
            }
        }
        public List<User> getUsersByPredicate(Func<User, bool> predicate)
        {
            using (var db = new FoodContext())
            {
                return (from b in db.Users
                        where predicate(b)
                        select b).ToList();
            }
        }

        public List<Meal> getMealsByPredicate(Func<Meal, bool> predicate)
        {
            using (var db = new FoodContext())
            {
                return (from b in db.Meals
                        where predicate(b)
                        select b).ToList();
            }
        }

        public List<DailyFood> getDailyFoodsByPredicate(Func<DailyFood, bool> predicate)
        {
            using (var db = new FoodContext())
            {
                return (from b in db.DailyFoods
                        where predicate(b)
                        select b).ToList();
            }
        }
        public List<WeekGoals> getWeekGoalsByPredicate(Func<WeekGoals, bool> predicate)
        {
            using (var db = new FoodContext())
            {
                return (from b in db.WeekGoals
                        where predicate(b)
                        select b).ToList();
            }
        }


        public DailyFood getDailyFood(string emailAddress, DateTime currentDate)
        {
            using (var db = new FoodContext())
            {
                var query = ((from b in db.DailyFoods
                              where b.EmailAddress.Equals(emailAddress) && b.CurrentDate.Day == currentDate.Day && b.CurrentDate.Month == currentDate.Month
                              && b.CurrentDate.Year == currentDate.Year
                              select b)).ToList();

                return query.FirstOrDefault();
            }
        }

        public List<Meal> getMeal(DateTime currentDate, String emailAddress)
        {
            using (var db = new FoodContext())
            {
                return ((from b in db.Meals
                         where b.CurrentDate.Day == currentDate.Day && b.CurrentDate.Month == currentDate.Month
                              && b.CurrentDate.Year == currentDate.Year && b.EmailAddress == emailAddress
                         select b).ToList());
            }
        }


        public User getUser(string emailAddress)
        {
            using (var db = new FoodContext())
            {
                var user=db.Users.SingleOrDefault(b=>b.EmailAddress.Equals(emailAddress));
                return user;
                    /*((from b in db.Users
                         where b.EmailAddress == emailAddress
                         select b).ToList()).FirstOrDefault();*/
            }
        }

        public List<User> getUsersByQuery(IOrderedQueryable<User> query)
        {
            throw new NotImplementedException();
        }

       

        public List<Meal> getListOfMeal(DateTime currentDate, String emailAddress, MEALTIME mealTime)
        {
            using (var db = new FoodContext())
            {
                return ((from b in db.Meals
                         where b.CurrentDate.Day == currentDate.Day && b.CurrentDate.Month == currentDate.Month
                              && b.CurrentDate.Year == currentDate.Year && b.EmailAddress.Equals(emailAddress) && b.MealTime == mealTime
                         select b).ToList());
            }
        }

        public bool removeDailyFood(string emailAddress, DateTime currentDate)
        {
            try
            {
                using (var db = new FoodContext())
                {
                    db.DailyFoods.Remove(getDailyFood(emailAddress, currentDate));
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e) { return false; }
        }
        
        public bool updateMeals(DateTime currentDate, String emailAddress, List<FoodItem> breakfast, List<FoodItem> brunch, List<FoodItem> dinner, List<FoodItem> snacks)
        {
            try
            {
               /* DecreaseProteins = 0;
                DecreaseFats = 0;
                DecreaseCarbs = 0;*/
                using (var db = new FoodContext())
                {
                    foreach (var breakfastItem in breakfast)
                    {
                        Meal meal = new Meal() { CurrentDate = currentDate, EmailAddress = emailAddress, MealTime = MEALTIME.BREAKFAST, FoodKey = breakfastItem.Key, FoodName = breakfastItem.Name, FoodAmount = breakfastItem.AmountGm, Calories100Gm = breakfastItem.Calories100G };
                        var mealTmp = db.Meals.SingleOrDefault(m => m.EmailAddress.Equals(meal.EmailAddress) && (m.CurrentDate.Day == meal.CurrentDate.Day && m.CurrentDate.Month == meal.CurrentDate.Month && m.CurrentDate.Year == meal.CurrentDate.Year) && m.MealTime == meal.MealTime && m.FoodKey.Equals(meal.FoodKey));
                        if (mealTmp != null)
                        {
                            db.Entry(mealTmp).CurrentValues.SetValues(meal);
                            db.SaveChanges();
                        }
                        else
                        {
                            db.Meals.Add(meal);
                            db.SaveChanges();
                        }
                    }
                    var queryBreakfast = ((from b in db.Meals
                                           where b.MealTime == MEALTIME.BREAKFAST && b.EmailAddress.Equals(emailAddress) && b.CurrentDate.Day == currentDate.Day&&
                                           b.CurrentDate.Month == currentDate.Month&& b.CurrentDate.Year == currentDate.Year
                                           select new FoodItem() { AmountGm=b.FoodAmount, Calories100G=b.Calories100Gm, Key=b.FoodKey, Name=b.FoodName }).ToList());
                    foreach (var item in queryBreakfast)
                    {
                      if (breakfast.Find(c=>c.Name.Equals(item.Name)&&c.Key.Equals(item.Key)&&c.Calories100G==item.Calories100G&&c.AmountGm==item.AmountGm)==null)
                        {
                            Meal m = new Meal() { CurrentDate = currentDate, EmailAddress = emailAddress, MealTime = MEALTIME.BREAKFAST, FoodKey = item.Key, FoodName = item.Name, FoodAmount =item.AmountGm, Calories100Gm = item.Calories100G };
                           // DecreaseProteins -= getFoodDetails(item.Key).Protien;
                            //DecreaseFats-= getFoodDetails(item.Key).Fats;
                            //DecreaseCarbs -= getFoodDetails(item.Key).Carbohydrate;
                            db.Meals.Attach(m);
                            db.Meals.Remove(m);
                            db.SaveChanges();
                        }
                    }
                        foreach (var brunchItem in brunch)
                    {
                        Meal meal = new Meal() { CurrentDate = currentDate, EmailAddress = emailAddress, MealTime = MEALTIME.BRUNCH, FoodKey = brunchItem.Key, FoodName = brunchItem.Name, FoodAmount = brunchItem.AmountGm, Calories100Gm = brunchItem.Calories100G };
                        var mealTmp = db.Meals.SingleOrDefault(m => m.EmailAddress.Equals(meal.EmailAddress) && (m.CurrentDate.Day == meal.CurrentDate.Day && m.CurrentDate.Month == meal.CurrentDate.Month && m.CurrentDate.Year == meal.CurrentDate.Year) && m.MealTime == meal.MealTime && m.FoodKey.Equals(meal.FoodKey));
                        if (mealTmp != null)
                        {
                            db.Entry(mealTmp).CurrentValues.SetValues(meal);
                            db.SaveChanges();
                        }
                        else
                        {
                            db.Meals.Add(meal);
                            db.SaveChanges();
                        }
                    }
                    var queryBrunch = ((from b in db.Meals
                                        where b.MealTime == MEALTIME.BRUNCH && b.EmailAddress.Equals(emailAddress) && b.CurrentDate.Day == currentDate.Day &&
                                           b.CurrentDate.Month == currentDate.Month && b.CurrentDate.Year == currentDate.Year
                                        select new FoodItem() { AmountGm = b.FoodAmount, Calories100G = b.Calories100Gm, Key = b.FoodKey, Name = b.FoodName }).ToList());
                    foreach (var item in queryBrunch)
                    {
                        if (brunch.Find(c => c.Name.Equals(item.Name) && c.Key.Equals(item.Key) && c.Calories100G == item.Calories100G && c.AmountGm == item.AmountGm) == null)
                        {
                            Meal m = new Meal() { CurrentDate = currentDate, EmailAddress = emailAddress, MealTime = MEALTIME.BRUNCH, FoodKey = item.Key, FoodName = item.Name, FoodAmount = item.AmountGm, Calories100Gm = item.Calories100G };
                            //DecreaseProteins -= getFoodDetails(item.Key).Protien;
                            //DecreaseFats -= getFoodDetails(item.Key).Fats;
                            //DecreaseCarbs -= getFoodDetails(item.Key).Carbohydrate;
                            db.Meals.Attach(m);
                            db.Meals.Remove(m);
                            db.SaveChanges();
                        }
                    }
                    foreach (var dinnerItem in dinner)
                    {
                        Meal meal = new Meal() { CurrentDate = currentDate, EmailAddress = emailAddress, MealTime = MEALTIME.DINNER, FoodKey = dinnerItem.Key, FoodName = dinnerItem.Name, FoodAmount = dinnerItem.AmountGm, Calories100Gm = dinnerItem.Calories100G };
                        var mealTmp = db.Meals.SingleOrDefault(m => m.EmailAddress.Equals(meal.EmailAddress) && (m.CurrentDate.Day == meal.CurrentDate.Day && m.CurrentDate.Month == meal.CurrentDate.Month && m.CurrentDate.Year == meal.CurrentDate.Year) && m.MealTime == meal.MealTime && m.FoodKey.Equals(meal.FoodKey));
                        if (mealTmp != null)
                        {
                            db.Entry(mealTmp).CurrentValues.SetValues(meal);
                            db.SaveChanges();
                        }
                        else
                        {
                            db.Meals.Add(meal);
                            db.SaveChanges();
                        }
                    }
                    var queryDinner= ((from b in db.Meals
                                       where b.MealTime == MEALTIME.DINNER && b.EmailAddress.Equals(emailAddress) && b.CurrentDate.Day == currentDate.Day &&
                                           b.CurrentDate.Month == currentDate.Month && b.CurrentDate.Year == currentDate.Year
                                       select new FoodItem() { AmountGm = b.FoodAmount, Calories100G = b.Calories100Gm, Key = b.FoodKey, Name = b.FoodName }).ToList());
                    foreach (var item in queryDinner)
                    {
                        if (dinner.Find(c => c.Name.Equals(item.Name) && c.Key.Equals(item.Key) && c.Calories100G == item.Calories100G && c.AmountGm == item.AmountGm) == null)
                        {
                            Meal m = new Meal() { CurrentDate = currentDate, EmailAddress = emailAddress, MealTime = MEALTIME.DINNER, FoodKey = item.Key, FoodName = item.Name, FoodAmount = item.AmountGm, Calories100Gm = item.Calories100G };
                           // DecreaseProteins -= getFoodDetails(item.Key).Protien;
                            //DecreaseFats -= getFoodDetails(item.Key).Fats;
                            //DecreaseCarbs -= getFoodDetails(item.Key).Carbohydrate;
                            db.Meals.Attach(m);
                            db.Meals.Remove(m);
                            db.SaveChanges();
                        }
                    }
                    foreach (var snacksItem in snacks)
                    {
                        Meal meal = new Meal() { CurrentDate = currentDate, EmailAddress = emailAddress, MealTime = MEALTIME.SNACKS, FoodKey = snacksItem.Key, FoodName = snacksItem.Name, FoodAmount = snacksItem.AmountGm, Calories100Gm = snacksItem.Calories100G };
                        var mealTmp = db.Meals.SingleOrDefault(m => m.EmailAddress.Equals(meal.EmailAddress) && (m.CurrentDate.Day == meal.CurrentDate.Day && m.CurrentDate.Month == meal.CurrentDate.Month && m.CurrentDate.Year == meal.CurrentDate.Year) && m.MealTime == meal.MealTime && m.FoodKey.Equals(meal.FoodKey));
                        if (mealTmp != null)
                        {
                            db.Entry(mealTmp).CurrentValues.SetValues(meal);
                            db.SaveChanges();
                        }
                        else
                        {
                            db.Meals.Add(meal);
                            db.SaveChanges();
                        }
                    }
                    var querySnacks = ((from b in db.Meals
                                        where b.MealTime == MEALTIME.SNACKS && b.EmailAddress.Equals(emailAddress) && b.CurrentDate.Day == currentDate.Day &&
                                           b.CurrentDate.Month == currentDate.Month && b.CurrentDate.Year == currentDate.Year
                                        select new FoodItem() { AmountGm = b.FoodAmount, Calories100G = b.Calories100Gm, Key = b.FoodKey, Name = b.FoodName }).ToList());
                    foreach (var item in querySnacks)
                    {
                        if (snacks.Find(c => c.Name.Equals(item.Name) && c.Key.Equals(item.Key) && c.Calories100G == item.Calories100G && c.AmountGm == item.AmountGm) == null)
                        {
                            Meal m = new Meal() { CurrentDate = currentDate, EmailAddress = emailAddress, MealTime = MEALTIME.SNACKS, FoodKey = item.Key, FoodName = item.Name, FoodAmount = item.AmountGm, Calories100Gm = item.Calories100G };
                           // DecreaseProteins -= getFoodDetails(item.Key).Protien;
                            //DecreaseFats -= getFoodDetails(item.Key).Fats;
                            //DecreaseCarbs -= getFoodDetails(item.Key).Carbohydrate;
                            db.Meals.Attach(m);
                            db.Meals.Remove(m);
                            db.SaveChanges();
                        }
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e) { return false; }
        }

        public bool removeUser(string emailAddress)
        {
            try
            {
                using (var db = new FoodContext())
                {
                    List<WeekGoals> wList = getWeekGoalsByPredicate(w => w.EmailAddress == emailAddress);
                    List<DailyFood> dList = getDailyFoodsByPredicate(d => d.EmailAddress == emailAddress);
                    if (wList == null && dList == null)// if there are no dailyFoods and no weekGoals with user's emailAdress -as foreign key
                    {
                        db.Users.Remove(getUser(emailAddress));
                        db.SaveChanges();
                    }
                    else// can't remove because there is most one (dailyFood or weekGoals) with those emailAddress -as foreign key
                        return false;
                }
                return true;
            }
            catch (Exception e) { return false; }
        }

      

        public bool updateDailyFood(DailyFood dailyFood)
        {
            try
            {
                using (var myDb = new FoodContext())
                {
                    var daily = myDb.DailyFoods.SingleOrDefault(d => d.EmailAddress.Equals(dailyFood.EmailAddress) && (d.CurrentDate.Day == dailyFood.CurrentDate.Day && d.CurrentDate.Month == dailyFood.CurrentDate.Month && d.CurrentDate.Year == dailyFood.CurrentDate.Year));
                    if (daily != null)
                    {
                        myDb.Entry(daily).CurrentValues.SetValues(dailyFood);
                        myDb.SaveChanges();
                    }

                }
                return true;
            }
            catch (Exception e) { return false; }
        }

        public bool updateUser(User user)
        {
            try
            {
                using (var myDb = new FoodContext())
                {
                    var userTemp = myDb.Users.SingleOrDefault(d => d.EmailAddress.Equals(user.EmailAddress));
                    if (userTemp != null)
                    {
                        myDb.Entry(userTemp).CurrentValues.SetValues(user);
                        myDb.SaveChanges();
                    }

                }
                return true;
            }
            catch (Exception e) { return false; }
        }

        public bool removeWeekGoals(string emailAddress, DateTime sundayDate)
        {
            try
            {
                using (var db = new FoodContext())
                {
                    db.WeekGoals.Remove(getWeekGoals(emailAddress, sundayDate));
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e) { return false; }
        }

        public WeekGoals getWeekGoals(string emailAddress, DateTime sundayDate)
        {
            using (var db = new FoodContext())
            {
                var query = ((from b in db.WeekGoals
                              where b.EmailAddress.Equals(emailAddress) && b.SundayDate.Day == sundayDate.Day && b.SundayDate.Month == sundayDate.Month
                              && b.SundayDate.Year == sundayDate.Year
                              select b)).ToList();

                return query.FirstOrDefault();
            }
        }

        public bool updateWeekGoals(WeekGoals weekGoals)
        {
            try
            {
                using (var myDb = new FoodContext())
                {
                    var weekGoalsTemp = myDb.WeekGoals.SingleOrDefault(d => d.EmailAddress.Equals(weekGoals.EmailAddress) && (d.SundayDate.Day== weekGoals.SundayDate.Day)
                                && (d.SundayDate.Year == weekGoals.SundayDate.Year) && (d.SundayDate.Month == weekGoals.SundayDate.Month));
                    if (weekGoalsTemp != null)
                    {
                        myDb.Entry(weekGoalsTemp).CurrentValues.SetValues(weekGoals);
                        myDb.SaveChanges();
                    }

                }
                return true;
            }
            catch (Exception e) { return false; }
        }

        #region apiSearchFood


        public List<FoodItem> getListFoodItems(String food)
        {
            return apiFood.getListFoodItems(food);
        }
        public FoodDetails getFoodDetails(string keyFood)
        {
            return apiFood.getFoodDetails(keyFood);
        }
        #endregion
    }
}

