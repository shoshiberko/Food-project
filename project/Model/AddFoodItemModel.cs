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
        public AddFoodItemModel()
        {
            bl = new ImpBL();
        }
        public float getCarbs(string foodKey)
        {
           return  bl.getFoodDetails(foodKey).Carbohydrate;
        }

        public float getFats(string foodKey)
        {
            return bl.getFoodDetails(foodKey).Fats;
        }

        public float getProteins(string foodKey)
        {
            return bl.getFoodDetails(foodKey).Protien;
        }

        public float getCalories100GM(string foodKey)
        {
            return bl.getFoodDetails(foodKey).Calories;
        }
    }
}
