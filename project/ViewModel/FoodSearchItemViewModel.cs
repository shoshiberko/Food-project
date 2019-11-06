using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project.ViewModel;
using System.Windows;
using System.Windows.Input;
using project.Commands;
using BE;

namespace project.Model
{
    public class FoodSearchItemViewModel: DependencyObject
    {
        #region properties
        FoodSearchItemModel foodSearchItemModel;//The Connection to the model

        /// <summary>
        ///the food details as string to take apart
        /// </summary>
        public static readonly DependencyProperty FoodDetailsDP = DependencyProperty.Register("FoodDetailsProperty", typeof(String), typeof(FoodSearchItemViewModel));
         public String FoodDetailsProperty
        {
            get { return (String)GetValue(FoodDetailsDP); }
            set { SetValue(FoodDetailsDP, value); }
         }

        /// <summary>
        ///the food name to search
        /// </summary>
        public static readonly DependencyProperty FoodName = DependencyProperty.Register("FoodNameProperty", typeof(String), typeof(FoodSearchItemViewModel));
        public String FoodNameProperty
        {
            get { return (String)GetValue(FoodName); }
            set { SetValue(FoodName, value); }
        }

        /// <summary>
        ///food key
        /// </summary>
        public String FoodNumber { get; set; }

        /// <summary>
        ///property that connected to a popUp window with all the food details and it's value make the popUp open/close
        /// </summary>
        public static readonly DependencyProperty MyIsOpen = DependencyProperty.Register("MyIsOpenProperty", typeof(Boolean), typeof(FoodSearchItemViewModel));
        public bool MyIsOpenProperty
        {
            get { return (bool)GetValue(MyIsOpen); }
            set { SetValue(MyIsOpen, value); }
        }

        public ICommand ClosePopUpCommandProperty { get; set; }//Command of close(click on X button) the popUp window after click on searchFoodList item.
        public ICommand GetFoodDetailsCommandProperty { get; set; }//Command of open the popUp window after click on searchFoodList item.
#endregion
        public FoodSearchItemViewModel()//constructor
        {
            foodSearchItemModel = new FoodSearchItemModel();
            ClosePopUpCommandProperty = new ClosePopUpCommand();
            GetFoodDetailsCommandProperty = new GetFoodDetailsCommand();
        }

        /// <summary>
        ///this function make the popUp window to open after put all the details in it.
        /// </summary>
        public void openPopUp()
        {
            FoodDetails fd= foodSearchItemModel.getFoodDetailsByNum(FoodNumber);
            FoodDetailsProperty = (fd.Calories).ToString() + "," + (fd.Carbohydrate).ToString() + "," + (fd.Fats).ToString() + "," + (fd.Fiber).ToString() +
                "," + (fd.Protien).ToString() + "," + (fd.Sodium).ToString() + "," + (fd.Sugars).ToString() + "," + (fd.Vitamins).ToString() + "," + (fd.Water).ToString()+","+FoodNameProperty;
            MyIsOpenProperty = true;
        }

    }
}
