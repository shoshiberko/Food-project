using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface IAPIFood
    {
        List<FoodItem> getListFoodItems(String food);
        FoodDetails getFoodDetails(string keyFood);
    }
}
