using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace project.ViewModel
{
    public class FoodDetailsViewModel:DependencyObject
    {
        public static readonly DependencyProperty FoodName = DependencyProperty.Register("FoodNameProperty", typeof(String), typeof(FoodDetailsViewModel));
        public String FoodNameProperty
        {
            get { return (String)GetValue(FoodName); }
            set { SetValue(FoodName, value); }
        }

        public static readonly DependencyProperty Calories = DependencyProperty.Register("CaloriesProperty", typeof(String), typeof(FoodDetailsViewModel));
        public String CaloriesProperty
        {
            get { return (String)GetValue(Calories); }
            set { SetValue(Calories, value); }
        }

        public static readonly DependencyProperty Fats = DependencyProperty.Register("FatsProperty", typeof(String), typeof(FoodDetailsViewModel));
        public String FatsProperty
        {
            get { return (String)GetValue(Fats); }
            set { SetValue(Fats, value); }
        }

        public static readonly DependencyProperty Protiens = DependencyProperty.Register("ProtiensProperty", typeof(String), typeof(FoodDetailsViewModel));
        public String ProtiensProperty
        {
            get { return (String)GetValue(Protiens); }
            set { SetValue(Protiens, value); }
        }

        public static readonly DependencyProperty Water = DependencyProperty.Register("WaterProperty", typeof(String), typeof(FoodDetailsViewModel));
        public String WaterProperty
        {
            get { return (String)GetValue(Water); }
            set { SetValue(Water, value); }
        }

        public static readonly DependencyProperty Fiber = DependencyProperty.Register("FiberProperty", typeof(String), typeof(FoodDetailsViewModel));
        public String FiberProperty
        {
            get { return (String)GetValue(Fiber); }
            set { SetValue(Fiber, value); }
        }

        public static readonly DependencyProperty Carb = DependencyProperty.Register("CarbProperty", typeof(String), typeof(FoodDetailsViewModel));
        public String CarbProperty
        {
            get { return (String)GetValue(Carb); }
            set { SetValue(Carb, value); }
        }

        public static readonly DependencyProperty Sodium = DependencyProperty.Register("SodiumProperty", typeof(String), typeof(FoodDetailsViewModel));
        public String SodiumProperty
        {
            get { return (String)GetValue(Sodium); }
            set { SetValue(Sodium, value); }
        }

        public static readonly DependencyProperty Sugars = DependencyProperty.Register("SugarsProperty", typeof(String), typeof(FoodDetailsViewModel));
        public String SugarsProperty
        {
            get { return (String)GetValue(Sugars); }
            set { SetValue(Sugars, value); }
        }

        public static readonly DependencyProperty Vitamins = DependencyProperty.Register("VitaminsProperty", typeof(String), typeof(FoodDetailsViewModel));
        public String VitaminsProperty
        {
            get { return (String)GetValue(Vitamins); }
            set { SetValue(Vitamins, value); }
        }
        public void updateFoodDetailsToScreen( String foodDetails)
        {
            String [] lst=foodDetails.Split(',');
            SugarsProperty =lst[6]+"g";
            FatsProperty = lst[2] + "g";
            ProtiensProperty = lst[4] + "g";
            CarbProperty = lst[1] + "g";
            SodiumProperty = lst[5] + "mg";
            VitaminsProperty = lst[7] + "mg";
            FiberProperty = lst[3] + "g";
            WaterProperty = lst[8] + "g";
           CaloriesProperty = lst[0];
            FoodNameProperty = lst[9];
        }
    }
}
