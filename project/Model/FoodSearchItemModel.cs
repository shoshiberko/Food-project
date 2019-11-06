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
        ImpBL myBL;//all the functions use the bl that has the access to DAL function and from there to the db. 
        public FoodSearchItemModel()//constructor
        {
            myBL = new ImpBL();
        }
       public FoodDetails getFoodDetailsByNum(String keyFood)//this function return the foodDetails of the food that has the keyFood key.
        {
            return myBL.getFoodDetails(keyFood);
        }
    }
}
