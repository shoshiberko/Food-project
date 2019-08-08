using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Xml;
using System.Net;
using System.IO;

namespace DAL
{
    public class ImpDal : IDal
    {
        IAPIFood apiFood = new ImpAPIFood();
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
            catch (Exception e){ return false; }
        }

        public bool addUser(User user)
        {
            try
            {
                using (var db = new FoodContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
                return true;
            }
            catch (Exception e) { return false; }
        }

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

        public List<DailyFood> getAllDailyFoods()
        {
            using (var db = new FoodContext())
            {
                return (from b in db.DailyFoods
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


        public DailyFood getDailyFood(DateTime currentDate)
        {
            using (var db = new FoodContext())
            {
                return ((from b in db.DailyFoods
                        where b.CurrentDate==currentDate
                        select b).ToList()).FirstOrDefault(null);
            }
        }

        public User getUser(string emailAddress)
        {
            using (var db = new FoodContext())
            {
                return ((from b in db.Users
                         where b.EmailAddress == emailAddress
                         select b).ToList()).FirstOrDefault(null);
            }
        }

        public List<User> getUsersByQuery(IOrderedQueryable<User> query)
        {
            throw new NotImplementedException();
        }

        public WeekGoals getWeekGoals(DateTime sundayDate)
        {
            using (var db = new FoodContext())
            {
                return ((from b in db.WeekGoals
                         where b.SundayDate == sundayDate
                         select b).ToList()).FirstOrDefault(null);
            }
        }

        public bool removeDailyFood(DateTime currentDate)
        {
            try
            {
                using (var db = new FoodContext())
                {
                    db.DailyFoods.Remove(getDailyFood(currentDate));
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
                    List<WeekGoals> wList = getWeekGoalsByPredicate(w=>w.EmailAddress==emailAddress);
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

        public bool removeWeekGoals(DateTime sundayDate)
        {
            try
            {
                using (var db = new FoodContext())
                {
                    db.WeekGoals.Remove(getWeekGoals(sundayDate));
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e) { return false; }
        }

        public bool updateDailyFood(DailyFood dailyFood)
        {
            try
            {
                using (var db = new FoodContext())
                {
                    //remove and add = update
                    db.DailyFoods.Remove(getDailyFood(dailyFood.CurrentDate));
                    db.DailyFoods.Add(dailyFood);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e) { return false; }
    }

        public bool updateUser(User user)
        {
            try
            {
                using (var db = new FoodContext())
                {
                    //remove and add = update
                    db.Users.Remove(getUser(user.EmailAddress));
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e) { return false; }
        }

        public bool updateWeekGoals(WeekGoals weekGoals)
        {
            try
            {
                using (var db = new FoodContext())
                {
                    //remove and add = update
                    db.WeekGoals.Remove(getWeekGoals(weekGoals.SundayDate));
                    db.WeekGoals.Add(weekGoals);
                    db.SaveChanges();
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
