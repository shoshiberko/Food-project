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

        public static readonly DependencyProperty FoodDetailsDP = DependencyProperty.Register("FoodDetailsProperty", typeof(String), typeof(FoodSearchItemViewModel));
    public String FoodDetailsProperty
    {
        get { return (String)GetValue(FoodDetailsDP); }
        set { SetValue(FoodDetailsDP, value); }
    }

    FoodSearchItemModel foodSearchItemModel;
        public static readonly DependencyProperty FoodName = DependencyProperty.Register("FoodNameProperty", typeof(String), typeof(FoodSearchItemViewModel));
        public String FoodNameProperty
        {
            get { return (String)GetValue(FoodName); }
            set { SetValue(FoodName, value); }
        }

        public String FoodNumber { get; set; }

        public static readonly DependencyProperty MyIsOpen = DependencyProperty.Register("MyIsOpenProperty", typeof(Boolean), typeof(FoodSearchItemViewModel));
        public bool MyIsOpenProperty
        {
            get { return (bool)GetValue(MyIsOpen); }
            set { SetValue(MyIsOpen, value); }
        }

        public ICommand ClosePopUpCommandProperty { get; set; }
        public ICommand GetFoodDetailsCommandProperty { get; set; }
        public FoodSearchItemViewModel()
        {
            foodSearchItemModel = new FoodSearchItemModel();
            ClosePopUpCommandProperty = new ClosePopUpCommand();
            GetFoodDetailsCommandProperty = new GetFoodDetailsCommand();
        }

        public void openPopUp()
        {
            FoodDetails fd= foodSearchItemModel.getFoodDetailsByNum(FoodNumber);
            FoodDetailsProperty = (fd.Calories).ToString() + "," + (fd.Carbohydrate).ToString() + "," + (fd.Fats).ToString() + "," + (fd.Fiber).ToString() +
                "," + (fd.Protien).ToString() + "," + (fd.Sodium).ToString() + "," + (fd.Sugars).ToString() + "," + (fd.Vitamins).ToString() + "," + (fd.Water).ToString()+","+FoodNameProperty;
            MyIsOpenProperty = true;
        }

    }
}
