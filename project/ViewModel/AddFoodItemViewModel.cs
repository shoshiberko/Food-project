using project.Commands;
using project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace project.ViewModel
{
    public class AddFoodItemViewModel: DependencyObject, INotifyPropertyChanged
    {
#region properties
        AddFoodItemModel addFoodItemModel;//The connection to the model
        public ICommand deleteCommand { get; set; }//delete foodItem command(X button)
        public event PropertyChangedEventHandler PropertyChanged;//eventHandler
        private void NotifyPropertyChanged(String propertyName)//eventHandler function
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        
        //100Gm components fields
        public float Proteins100Gm { get; set; }
        public float Fats100Gm { get; set; }
        public float Carbs100Gm { get; set; }

        /// <summary>
        ///Calories of foodItem
        /// </summary>
        public static readonly DependencyProperty Calories = DependencyProperty.Register("CaloriesProperty", typeof(String), typeof(AddFoodItemViewModel));
        public String CaloriesProperty
        {
            get { return (String)GetValue(Calories); }
            set { SetValue(Calories, value); }
        }

        /// <summary>
        ///GM of foodItem
        /// </summary>
        public static readonly DependencyProperty GM = DependencyProperty.Register("GMProperty", typeof(String), typeof(AddFoodItemViewModel));
        public String GMProperty
        {
            get { return (String)GetValue(GM); }
            set { SetValue(GM, value); }
        }

        /// <summary>
        ///foodName
        /// </summary>
        private String foodName;
        public String FoodNameProperty
        {
            get { return foodName; }
            set
            {
                if (value != this.foodName)
                {
                    this.foodName = value;
                }
            }
        }

        /// <summary>
        ///foodKey
        /// </summary>
        private String foodKey;
        public String FoodKey { get { return foodKey; } set {
                foodKey = value;
                //according the key get the values of all the components properties
                this.Carbs100Gm = addFoodItemModel.getCarbs(value);
                this.Fats100Gm = addFoodItemModel.getFats(value);
                this.Proteins100Gm = addFoodItemModel.getProteins(value);
                this.Calories100Gm=addFoodItemModel.getCalories100GM(value);
                FoodAmountProperty = "1";//start of foodItem
            } }
        
        /// <summary>
        ///Food amount
        /// </summary>
        private String foodAmount;
        public String FoodAmountProperty
        {
            get { return foodAmount; }
            set
            {
                if (value != this.foodAmount)
                {
                    if (value.Equals("0"))//if the item would be deleted
                        NotifyPropertyChanged("deleteItem");
                    else//the amount increased or decreased
                    {
                        String s = "";
                        if (foodAmount != null && foodAmount != "" && float.Parse(this.foodAmount) < float.Parse(value) || (foodAmount == null && value != null))//if it is increase
                            s = "Up";
                        this.foodAmount = value;
                         CaloriesProperty = (Calories100Gm * float.Parse(value)).ToString();
                        GMProperty = (int.Parse(value) * 100).ToString() + "g";
                        NotifyPropertyChanged("FoodAmountProperty" + s);//go to the function it sign to in AddDailyFoodViewModel
                    }
                }
            }
        }

        /// <summary>
        ///Calories of 100GM
        /// </summary>
        private float calories100Gm { get; set; }
        public float Calories100Gm
        {
            get { return calories100Gm; }
            set
            {
                if (value != this.calories100Gm)
                {
                    this.calories100Gm = value;
                    CaloriesProperty = value.ToString();
                    NotifyPropertyChanged("Calories100Gm");//go to the function it sign to in AddDailyFoodViewModel
                }
            }
        }

        /// <summary>
        ///This function delete food item by put 0 in it's amount
        /// </summary>
        public void deleteFoodItem()
        {
            FoodAmountProperty = "0";
        }

        public AddFoodItemViewModel()//constructor
        {
            addFoodItemModel = new AddFoodItemModel();
            deleteCommand = new DeleteFoodItemCommand();
        }
    }
#endregion
}

