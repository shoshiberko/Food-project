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
        #region properties
        /// <summary>
        ///FoodName in the current foodItem
        /// </summary>
        public static readonly DependencyProperty FoodName = DependencyProperty.Register("FoodNameProperty", typeof(String), typeof(FoodDetailsViewModel));//dependency property to the foodName value(binding).
        public String FoodNameProperty
        {
            get { return (String)GetValue(FoodName); }
            set { SetValue(FoodName, value); }
        }

        /// <summary>
        ///Calories in the current foodItem
        /// </summary>
        public static readonly DependencyProperty Calories = DependencyProperty.Register("CaloriesProperty", typeof(String), typeof(FoodDetailsViewModel));//dependency property to the calories value(binding).
        public String CaloriesProperty
        {
            get { return (String)GetValue(Calories); }
            set { SetValue(Calories, value); }
        }

        /// <summary>
        ///Fats in the current foodItem
        /// </summary>
        public static readonly DependencyProperty Fats = DependencyProperty.Register("FatsProperty", typeof(String), typeof(FoodDetailsViewModel));//dependency property to the fats value(binding).
        public String FatsProperty
        {
            get { return (String)GetValue(Fats); }
            set { SetValue(Fats, value); }
        }

        /// <summary>
        ///Protiens in the current foodItem
        /// </summary>
        public static readonly DependencyProperty Protiens = DependencyProperty.Register("ProtiensProperty", typeof(String), typeof(FoodDetailsViewModel));//dependency property to the protiens value(binding).
        public String ProtiensProperty
        {
            get { return (String)GetValue(Protiens); }
            set { SetValue(Protiens, value); }
        }

        /// <summary>
        ///Water in the current foodItem
        /// </summary>
        public static readonly DependencyProperty Water = DependencyProperty.Register("WaterProperty", typeof(String), typeof(FoodDetailsViewModel));//dependency property to the water value(binding).
        public String WaterProperty
        {
            get { return (String)GetValue(Water); }
            set { SetValue(Water, value); }
        }

        /// <summary>
        ///Fiber in the current foodItem
        /// </summary>
        public static readonly DependencyProperty Fiber = DependencyProperty.Register("FiberProperty", typeof(String), typeof(FoodDetailsViewModel));//dependency property to the fiber value(binding).
        public String FiberProperty
        {
            get { return (String)GetValue(Fiber); }
            set { SetValue(Fiber, value); }
        }

        /// <summary>
        ///Carb in the current foodItem
        /// </summary>
        public static readonly DependencyProperty Carb = DependencyProperty.Register("CarbProperty", typeof(String), typeof(FoodDetailsViewModel));//dependency property to the carb value(binding).
        public String CarbProperty
        {
            get { return (String)GetValue(Carb); }
            set { SetValue(Carb, value); }
        }

        /// <summary>
        ///Sodium in the current foodItem
        /// </summary>
        public static readonly DependencyProperty Sodium = DependencyProperty.Register("SodiumProperty", typeof(String), typeof(FoodDetailsViewModel));//dependency property to the sodium value(binding).
        public String SodiumProperty
        {
            get { return (String)GetValue(Sodium); }
            set { SetValue(Sodium, value); }
        }

        /// <summary>
        ///Sugar in the current foodItem
        /// </summary>
        public static readonly DependencyProperty Sugars = DependencyProperty.Register("SugarsProperty", typeof(String), typeof(FoodDetailsViewModel));//dependency property to the sugars value(binding).
        public String SugarsProperty
        {
            get { return (String)GetValue(Sugars); }
            set { SetValue(Sugars, value); }
        }

        /// <summary>
        ///Vitamins in the current foodItem
        /// </summary>
        public static readonly DependencyProperty Vitamins = DependencyProperty.Register("VitaminsProperty", typeof(String), typeof(FoodDetailsViewModel));//dependency property to the vitamins value(binding).
        public String VitaminsProperty
        {
            get { return (String)GetValue(Vitamins); }
            set { SetValue(Vitamins, value); }
        }
        #endregion
        /// <summary>
        ///this function get String foodDetails and define all the properties of this class according to this string.
        /// </summary>
        /// <param name="foodDetails">the String that describe the foodDetails</param>
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
