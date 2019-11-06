using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Model
{
    public class SearchUserControlModel
    {
        ImpBL BL;//all the functions use the bl that has the access to DAL function and from there to the db. 
        public SearchUserControlModel()//constructor
        {
            BL = new ImpBL();
        }
        public List<FoodItem> getListOfItems(String FoodNameToSearch)//this function return list of foodItems that their names include FoodNameToSearch
        {
           return  BL.getListFoodItems(FoodNameToSearch);
        }
    }
}
