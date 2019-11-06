using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Model
{
    public class AddFoodItemModel
    {
        ImpBL bl;
        public AddFoodItemModel()//constructor
        {
            bl = new ImpBL();
        }
        public float getCarbs(string foodKey)//get the carbs in food that keyFood it's key
        {
           return  bl.getFoodDetails(foodKey).Carbohydrate;
        }

        public float getFats(string foodKey)//get the fats in food that keyFood it's key
        {
            return bl.getFoodDetails(foodKey).Fats;
        }

        public float getProteins(string foodKey)//get the protien in food that keyFood it's key
        {
            return bl.getFoodDetails(foodKey).Protien;
        }

        public float getCalories100GM(string foodKey)//get the calories in 100GM of food that keyFood it's key
        {
            return bl.getFoodDetails(foodKey).Calories;
        }
    }
}
