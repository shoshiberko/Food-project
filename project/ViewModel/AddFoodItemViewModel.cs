﻿using project.Commands;
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
        AddFoodItemModel addFoodItemModel;
        public ICommand deleteCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
           /*if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }*/
        }
        
        public float Proteins100Gm { get; set; }
        public float Fats100Gm { get; set; }
        public float Carbs100Gm { get; set; }

        public static readonly DependencyProperty Calories = DependencyProperty.Register("CaloriesProperty", typeof(String), typeof(AddFoodItemViewModel));
        public String CaloriesProperty
        {
            get { return (String)GetValue(Calories); }
            set { SetValue(Calories, value); }
        }

        public static readonly DependencyProperty GM = DependencyProperty.Register("GMProperty", typeof(String), typeof(AddFoodItemViewModel));
        public String GMProperty
        {
            get { return (String)GetValue(GM); }
            set { SetValue(GM, value); }
        }




        private String foodName;
        public String FoodNameProperty
        {
            get { return foodName; }
            set
            {
                if (value != this.foodName)
                {
                    this.foodName = value;
                    // NotifyPropertyChanged("FoodNameProperty");
                }
            }
        }

        private String foodKey;
        public String FoodKey { get { return foodKey; } set {
                foodKey = value;
                this.Carbs100Gm = addFoodItemModel.getCarbs(value);
                this.Fats100Gm = addFoodItemModel.getFats(value);
                this.Proteins100Gm = addFoodItemModel.getProteins(value);
                this.Calories100Gm=addFoodItemModel.getCalories100GM(value);
                FoodAmountProperty = "1";
            } }



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
                        if (foodAmount != null && foodAmount != "" && float.Parse(this.foodAmount) < float.Parse(value) || (foodAmount == null && value != null))//if it is rise
                            s = "Up";
                        this.foodAmount = value;
                        // AmountProperty = value;
                         CaloriesProperty = (Calories100Gm * float.Parse(value)).ToString();
                        GMProperty = (int.Parse(value) * 100).ToString() + "g";
                        NotifyPropertyChanged("FoodAmountProperty" + s);
                    }
                }
            }
        }


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
                    NotifyPropertyChanged("Calories100Gm");
                }
            }
        }


        public void deleteFoodItem()
        {
            FoodAmountProperty = "0";
        }

        public AddFoodItemViewModel()
        {
            addFoodItemModel = new AddFoodItemModel();
            deleteCommand = new DeleteFoodItemCommand();
            // AmountProperty = "1";
        }
    }
}

