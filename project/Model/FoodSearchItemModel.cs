using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Model
{
    public class FoodSearchItemModel
    {
        ImpBL myBL;
        public FoodSearchItemModel()
        {
            myBL = new ImpBL();
        }
       public FoodDetails getFoodDetailsByNum(String keyFood)
        {
            return myBL.getFoodDetails(keyFood);
        }
    }
}
