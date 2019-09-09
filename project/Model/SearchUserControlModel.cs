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
        ImpBL BL;
        public SearchUserControlModel()
        {
            BL = new ImpBL();
        }
        public List<FoodItem> getListOfItems(String FoodNameToSearch)
        {
           return  BL.getListFoodItems(FoodNameToSearch);
        }
    }
}
