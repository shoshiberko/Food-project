using project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BE;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using project.Commands;
using System.Windows.Threading;

namespace project.ViewModel
{
    public class AddDailyFoodViewModel : DependencyObject
    {
#region properties
        AddDailyFoodModel addDailyFoodModel;//The connection to the model
        public ICommand SaveCommand { get; set; }//The command that save the details of dailyFood in the server

        /// <summary>
        ///The current day property
        /// </summary>
        public static readonly DependencyProperty Date = DependencyProperty.Register("DateProperty", typeof(DateTime), typeof(AddDailyFoodViewModel), new PropertyMetadata(DateTime.Now.Date));
        public DateTime DateProperty
        {
            get { return (DateTime)GetValue(Date); }
            set { SetValue(Date, value); }
        }

        /// <summary>
        ///The email address- that identify the user that the dailyFood details belongs to
        /// </summary>
        public static readonly DependencyProperty EmailAddress = DependencyProperty.Register("EmailAddressProperty", typeof(String), typeof(AddDailyFoodViewModel), new PropertyMetadata("shosh@gmail.com"));
        public String EmailAddressProperty
        {
            get { return (String)GetValue(EmailAddress); }
            set { SetValue(EmailAddress, value); }
        }

        /// <summary>
        ///The current day mood property(will be change from int to enum)
        /// </summary>
        public static readonly DependencyProperty Mood = DependencyProperty.Register("MoodProperty", typeof(int), typeof(AddDailyFoodViewModel));
        public int MoodProperty
        {
            get { return (int)GetValue(Mood); }
            set { SetValue(Mood, value); }
        }

        /// <summary>
        ///The current day activity property(will be change from int to enum)
        /// </summary>
        public static readonly DependencyProperty Activity = DependencyProperty.Register("ActivityProperty", typeof(int), typeof(AddDailyFoodViewModel));
        public int ActivityProperty
        {
            get { return (int)GetValue(Activity); }
            set { SetValue(Activity, value); }
        }

        /// <summary>
        ///The current day total calories property
        /// </summary>
        public static readonly DependencyProperty TotalCalories = DependencyProperty.Register("TotalCaloriesProperty", typeof(String), typeof(AddDailyFoodViewModel));
        public String TotalCaloriesProperty
        {
            get { return (String)GetValue(TotalCalories); }
            set { if (float.Parse(value) > 0 && !value.Contains("E")) SetValue(TotalCalories, value); else SetValue(TotalCalories, "0"); }
        }

        /// <summary>
        ///The current day total fat property
        /// </summary>
        public static readonly DependencyProperty TotalFats = DependencyProperty.Register("TotalFatsProperty", typeof(String), typeof(AddDailyFoodViewModel));
        public String TotalFatsProperty
        {
            get { return (String)GetValue(TotalFats); }
            set { if (float.Parse(value) > 0 && !value.Contains("E")) SetValue(TotalFats, value); else SetValue(TotalFats, "0"); }
        }

        /// <summary>
        ///The current day total carb property
        /// </summary>
        public static readonly DependencyProperty TotalCarbs = DependencyProperty.Register("TotalCarbsProperty", typeof(String), typeof(AddDailyFoodViewModel));
        public String TotalCarbsProperty
        {
            get { return (String)GetValue(TotalCarbs); }
            set { if (float.Parse(value) > 0 && !value.Contains("E")) SetValue(TotalCarbs, value); else SetValue(TotalCarbs, "0"); }
        }

        /// <summary>
        ///The current day total protein property
        /// </summary>
        public static readonly DependencyProperty TotalProteins = DependencyProperty.Register("TotalProteinsProperty", typeof(String), typeof(AddDailyFoodViewModel));
        public String TotalProteinsProperty
        {
            get { return (String)GetValue(TotalProteins); }
            set { if (float.Parse(value) > 0 && !value.Contains("E")) SetValue(TotalProteins, value); else SetValue(TotalProteins, "0"); }
        }

        /// <summary>
        ///The current day total calories of Breakfast property
        /// </summary>
        public static readonly DependencyProperty BreakfastCalories = DependencyProperty.Register("BreakfastCaloriesProperty", typeof(String), typeof(AddDailyFoodViewModel));
        public String BreakfastCaloriesProperty
        {
            get { return (String)GetValue(BreakfastCalories); }
            set { SetValue(BreakfastCalories, value); }
        }

        /// <summary>
        ///The current day total calories of Dinner property
        /// </summary>
        public static readonly DependencyProperty DinnerCalories = DependencyProperty.Register("DinnerCaloriesProperty", typeof(String), typeof(AddDailyFoodViewModel));
        public String DinnerCaloriesProperty
        {
            get { return (String)GetValue(DinnerCalories); }
            set { SetValue(DinnerCalories, value); }
        }

        /// <summary>
        ///The current day total calories of Brunch property
        /// </summary>
        public static readonly DependencyProperty BrunchCalories = DependencyProperty.Register("BrunchCaloriesProperty", typeof(String), typeof(AddDailyFoodViewModel));
        public String BrunchCaloriesProperty
        {
            get { return (String)GetValue(BrunchCalories); }
            set { SetValue(BrunchCalories, value); }
        }

        /// <summary>
        ///The current day total calories of Snacks property
        /// </summary>
        public static readonly DependencyProperty SnacksCalories = DependencyProperty.Register("SnacksCaloriesProperty", typeof(String), typeof(AddDailyFoodViewModel));
        public String SnacksCaloriesProperty
        {
            get { return (String)GetValue(SnacksCalories); }
            set { SetValue(SnacksCalories, value); }
        }

        /// <summary>
        ///The current day list of Breakfast foods property
        /// </summary>
        public static readonly DependencyProperty BreakfastList = DependencyProperty.Register("BreakfastListProperty", typeof(ObservableCollection<AddFoodItemViewModel>), typeof(AddDailyFoodViewModel));
        public ObservableCollection<AddFoodItemViewModel> BreakfastListProperty
        {
            get { return (ObservableCollection<AddFoodItemViewModel>)GetValue(BreakfastList); }
            set { SetValue(BreakfastList, value); }
        }

        /// <summary>
        ///The current day list of Brunch foods property
        /// </summary>
        public static readonly DependencyProperty BrunchList = DependencyProperty.Register("BrunchListProperty", typeof(ObservableCollection<AddFoodItemViewModel>), typeof(AddDailyFoodViewModel));
        public ObservableCollection<AddFoodItemViewModel> BrunchListProperty
        {
            get { return (ObservableCollection<AddFoodItemViewModel>)GetValue(BrunchList); }
            set { SetValue(BrunchList, value); }
        }

        /// <summary>
        ///The current day list of Dinner foods property
        /// </summary>
        public static readonly DependencyProperty DinnerList = DependencyProperty.Register("DinnerListProperty", typeof(ObservableCollection<AddFoodItemViewModel>), typeof(AddDailyFoodViewModel));
        public ObservableCollection<AddFoodItemViewModel> DinnerListProperty
        {
            get { return (ObservableCollection<AddFoodItemViewModel>)GetValue(DinnerList); }
            set { SetValue(DinnerList, value); }
        }

        /// <summary>
        ///The current day list of Snacks foods property
        /// </summary>
        public static readonly DependencyProperty SnacksList = DependencyProperty.Register("SnacksListProperty", typeof(ObservableCollection<AddFoodItemViewModel>), typeof(AddDailyFoodViewModel));
        public ObservableCollection<AddFoodItemViewModel> SnacksListProperty
        {
            get { return (ObservableCollection<AddFoodItemViewModel>)GetValue(SnacksList); }
            set { SetValue(SnacksList, value); }
        }

        /// <summary>
        ///The Breakfast foodItem to add
        /// </summary>
        public static readonly DependencyProperty SelectedFoodToAddBreakfast = DependencyProperty.Register("SelectedFoodToAddBreakfastProperty", typeof(FoodItem), typeof(AddDailyFoodViewModel), new UIPropertyMetadata(new PropertyChangedCallback(OnSelectedFoodItemBreakfast)));
        public FoodItem SelectedFoodToAddBreakfastProperty
        {
            get { return (FoodItem)GetValue(SelectedFoodToAddBreakfast); }
            set { SetValue(SelectedFoodToAddBreakfast, value); }
        }

        /// <summary>
        ///The property that handle the information about open/close of popUp window of saveChanges button
        /// </summary>
        public static readonly DependencyProperty IsOpenSaveChange = DependencyProperty.Register("IsOpenSaveChangeProperty", typeof(Boolean), typeof(AddDailyFoodViewModel));
        public Boolean IsOpenSaveChangeProperty
        {
            get { return (Boolean)GetValue(IsOpenSaveChange); }
            set { SetValue(IsOpenSaveChange, value); }
        }

        /// <summary>
        ///The Brunch foodItem to add
        /// </summary>
        public static readonly DependencyProperty SelectedFoodToAddBrunch = DependencyProperty.Register("SelectedFoodToAddBrunchProperty", typeof(FoodItem), typeof(AddDailyFoodViewModel), new UIPropertyMetadata(new PropertyChangedCallback(OnSelectedFoodItemBrunch)));
        public FoodItem SelectedFoodToAddBrunchProperty
        {
            get { return (FoodItem)GetValue(SelectedFoodToAddBrunch); }
            set { SetValue(SelectedFoodToAddBrunch, value); }
        }

        /// <summary>
        ///The Dinner foodItem to add
        /// </summary>
        public static readonly DependencyProperty SelectedFoodToAddDinner = DependencyProperty.Register("SelectedFoodToAddDinnerProperty", typeof(FoodItem), typeof(AddDailyFoodViewModel), new UIPropertyMetadata(new PropertyChangedCallback(OnSelectedFoodItemDinner)));
        public FoodItem SelectedFoodToAddDinnerProperty
        {
            get { return (FoodItem)GetValue(SelectedFoodToAddDinner); }
            set { SetValue(SelectedFoodToAddDinner, value); }
        }

        /// <summary>
        ///The Snacks foodItem to add
        /// </summary>
        public static readonly DependencyProperty SelectedFoodToAddSnacks = DependencyProperty.Register("SelectedFoodToAddSnacksProperty", typeof(FoodItem), typeof(AddDailyFoodViewModel), new UIPropertyMetadata(new PropertyChangedCallback(OnSelectedFoodItemSnacks)));
        public FoodItem SelectedFoodToAddSnacksProperty
        {
            get { return (FoodItem)GetValue(SelectedFoodToAddSnacks); }
            set { SetValue(SelectedFoodToAddSnacks, value); }
        }
        #endregion
        /// <summary>
        ///This function get date and put all the details of this date dailyFood in the match properties
        /// </summary>
        /// <param name="selectedDate">the date of the day that it's details showen</param>
        public void getDailyFoodByDate(DateTime selectedDate)
        {
            DailyFood df = addDailyFoodModel.getDailyFoodByDate(EmailAddressProperty, selectedDate);//Get the daily food from the db
            //Get the food lists
            List<FoodItem> breakfastLst = addDailyFoodModel.getMealsList(EmailAddressProperty, selectedDate, MEALTIME.BREAKFAST);
            List<FoodItem> brunchLst = addDailyFoodModel.getMealsList(EmailAddressProperty, selectedDate, MEALTIME.BRUNCH);
            List<FoodItem> dinnerLst = addDailyFoodModel.getMealsList(EmailAddressProperty, selectedDate, MEALTIME.DINNER);
            List<FoodItem> snacksLst = addDailyFoodModel.getMealsList(EmailAddressProperty, selectedDate, MEALTIME.SNACKS);
            //Fill all the regular properties(total,MOOD,ACTIVITY)
            if (df != null)
            {
                MoodProperty =(int) df.DailyMood;
                ActivityProperty = (int)df.DailyActivity;
                TotalCaloriesProperty = df.TotalCalories.ToString();
                TotalCarbsProperty = df.TotalCarbs.ToString();
                TotalFatsProperty = df.TotalFats.ToString();
                TotalProteinsProperty = df.TotalPortiens.ToString();
            }
            else
            {
                MoodProperty =-1 ;
                ActivityProperty =-1 ;
                TotalCaloriesProperty = "0";
                TotalCarbsProperty = "0";
                TotalFatsProperty = "0";
                TotalProteinsProperty = "0";

            }
            //Fill the list food properties
            BreakfastListProperty = convertFoodItemListToObs(breakfastLst,MEALTIME.BREAKFAST);
            BrunchListProperty = convertFoodItemListToObs(brunchLst,MEALTIME.BRUNCH);
            DinnerListProperty = convertFoodItemListToObs(dinnerLst, MEALTIME.DINNER);
            SnacksListProperty = convertFoodItemListToObs(snacksLst, MEALTIME.SNACKS);
            //Fill all the total calories field per meal
            BreakfastCaloriesProperty = "0";
            BrunchCaloriesProperty = "0";
            DinnerCaloriesProperty = "0";
            SnacksCaloriesProperty = "0";
            foreach (var item in BreakfastListProperty)
            {
                BreakfastCaloriesProperty = (float.Parse(BreakfastCaloriesProperty) + item.Calories100Gm * float.Parse(item.FoodAmountProperty)).ToString();
            }
            foreach (var item in BrunchListProperty)
            {
                BrunchCaloriesProperty = (float.Parse(BrunchCaloriesProperty) +item.Calories100Gm * float.Parse(item.FoodAmountProperty)).ToString();
            }
            foreach (var item in DinnerListProperty)
            {
                DinnerCaloriesProperty = (float.Parse(DinnerCaloriesProperty) + item.Calories100Gm * float.Parse(item.FoodAmountProperty)).ToString();
            }
            foreach (var item in SnacksListProperty)
            {
                SnacksCaloriesProperty = (float.Parse(SnacksCaloriesProperty) + +item.Calories100Gm * float.Parse(item.FoodAmountProperty)).ToString();
            }


        }


        /// <summary>
        ///This function convert from list of FoodItem to ObservableCollection<AddFoodItemViewModel>
        /// </summary>
        /// <param name="lst">list foodItem to convert</param>
        /// <param name="mt">mt type of list</param>
        /// <returns>ObservableCollection<AddFoodItemViewModel> convert of the input list</returns>
        ObservableCollection<AddFoodItemViewModel> convertFoodItemListToObs(List<FoodItem> lst,MEALTIME mt)
        {
            ObservableCollection<AddFoodItemViewModel> result = new ObservableCollection<AddFoodItemViewModel>();
            AddFoodItemViewModel foodItemVm;
            foreach (var item in lst)//for each item in the input list create new AddFoodItemViewModel according it details.
            {
                foodItemVm = new AddFoodItemViewModel() { FoodNameProperty = item.Name, FoodKey = item.Key, FoodAmountProperty = item.AmountGm.ToString(), Calories100Gm = item.Calories100G, CaloriesProperty= (item.Calories100G* item.AmountGm).ToString(), GMProperty=(100* item.AmountGm).ToString()+"g" };
                //sign to property change according to the mt input variable
                switch (mt)
                {
                    case MEALTIME.BREAKFAST: foodItemVm.PropertyChanged += PropertyChangedFoodItemBreakfast;
                        break;
                    case MEALTIME.BRUNCH:
                        foodItemVm.PropertyChanged += PropertyChangedFoodItemBrunch;
                        break;
                    case MEALTIME.DINNER:
                        foodItemVm.PropertyChanged += PropertyChangedFoodItemDinner;
                        break;
                    case MEALTIME.SNACKS:
                        foodItemVm.PropertyChanged += PropertyChangedFoodItemSnacks;
                        break;
                }
                
                result.Add(foodItemVm);//add the item to the new, in buliding list
            }
            return result;
        }



        /// <summary>
        ///this function done when foodItem selected from Breakfast list- the user want to add this foodItem to the list of Breakfast
        /// </summary>
        /// <param name="d">the sender object</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        private static void OnSelectedFoodItemBreakfast(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddDailyFoodViewModel addDailyFoodViewModel = d as AddDailyFoodViewModel;
            ObservableCollection<AddFoodItemViewModel> tmpLst;
            if (addDailyFoodViewModel.BreakfastListProperty == null)//if the list BreakfastProperty is empty
                tmpLst = new ObservableCollection<AddFoodItemViewModel>();
            else
                tmpLst = addDailyFoodViewModel.BreakfastListProperty;
            AddFoodItemViewModel addFoodItemViewModel = new AddFoodItemViewModel();
            addFoodItemViewModel.PropertyChanged += addDailyFoodViewModel.PropertyChangedFoodItemBreakfast;//sign the AddFoodItemViewModel to  when property change
            //put the addDailyFoodViewModel details in addFoodItemViewModel
            addFoodItemViewModel.FoodNameProperty = ((FoodItem)e.NewValue).Name;
            addFoodItemViewModel.FoodKey = ((FoodItem)e.NewValue).Key;
            addFoodItemViewModel.Calories100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Calories;
            addFoodItemViewModel.Carbs100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Carbohydrate;
            addFoodItemViewModel.Proteins100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Protien;
            addFoodItemViewModel.Fats100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Fats;
            tmpLst.Add(addFoodItemViewModel);
            addDailyFoodViewModel.BreakfastListProperty = tmpLst;
        }

        /// <summary>
        ///this function done when foodItem selected from Brunch list- the user want to add this foodItem to the list of Brunch
        /// </summary>
        /// <param name="d">the sender object</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        private static void OnSelectedFoodItemBrunch(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddDailyFoodViewModel addDailyFoodViewModel = d as AddDailyFoodViewModel;
            ObservableCollection<AddFoodItemViewModel> tmpLst;
            if (addDailyFoodViewModel.BrunchListProperty == null)//if the list BrunchProperty is empty
                tmpLst = new ObservableCollection<AddFoodItemViewModel>();
            else
                tmpLst = addDailyFoodViewModel.BrunchListProperty;
            AddFoodItemViewModel addFoodItemViewModel = new AddFoodItemViewModel();
            addFoodItemViewModel.PropertyChanged += addDailyFoodViewModel.PropertyChangedFoodItemBrunch;//sign the AddFoodItemViewModel to when property change
            //put the addDailyFoodViewModel details in addFoodItemViewModel
            addFoodItemViewModel.FoodNameProperty = ((FoodItem)e.NewValue).Name;
            addFoodItemViewModel.FoodKey = ((FoodItem)e.NewValue).Key;
            addFoodItemViewModel.Calories100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Calories;
            addFoodItemViewModel.Carbs100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Carbohydrate;
            addFoodItemViewModel.Proteins100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Protien;
            addFoodItemViewModel.Fats100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Fats;
            tmpLst.Add(addFoodItemViewModel);
            addDailyFoodViewModel.BrunchListProperty = tmpLst;
        }

        /// <summary>
        ///this function done when foodItem selected from Dinner list- the user want to add this foodItem to the list of Dinner
        /// </summary>
        /// <param name="d">the sender object</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        private static void OnSelectedFoodItemDinner(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddDailyFoodViewModel addDailyFoodViewModel = d as AddDailyFoodViewModel;
            ObservableCollection<AddFoodItemViewModel> tmpLst;
            if (addDailyFoodViewModel.DinnerListProperty == null)//if the list Dinner Property is empty
                tmpLst = new ObservableCollection<AddFoodItemViewModel>();
            else
                tmpLst = addDailyFoodViewModel.DinnerListProperty;
            AddFoodItemViewModel addFoodItemViewModel = new AddFoodItemViewModel();
            addFoodItemViewModel.PropertyChanged += addDailyFoodViewModel.PropertyChangedFoodItemDinner;//sign the AddFoodItemViewModel to when property change
            //put the addDailyFoodViewModel details in addFoodItemViewModel
            addFoodItemViewModel.FoodNameProperty = ((FoodItem)e.NewValue).Name;
            addFoodItemViewModel.FoodKey = ((FoodItem)e.NewValue).Key;
            addFoodItemViewModel.Calories100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Calories;
            addFoodItemViewModel.Carbs100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Carbohydrate;
            addFoodItemViewModel.Proteins100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Protien;
            addFoodItemViewModel.Fats100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Fats;
            tmpLst.Add(addFoodItemViewModel);
            addDailyFoodViewModel.DinnerListProperty = tmpLst;
        }

        /// <summary>
        ///this function done when foodItem selected from Snacks list- the user want to add this foodItem to the list of Snacks
        /// </summary>
        /// <param name="d">the sender object</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        private static void OnSelectedFoodItemSnacks(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddDailyFoodViewModel addDailyFoodViewModel = d as AddDailyFoodViewModel;
            ObservableCollection<AddFoodItemViewModel> tmpLst;
            if (addDailyFoodViewModel.SnacksListProperty == null)//if the list Snacks Property is empty
                tmpLst = new ObservableCollection<AddFoodItemViewModel>();
            else
                tmpLst = addDailyFoodViewModel.SnacksListProperty;
            AddFoodItemViewModel addFoodItemViewModel = new AddFoodItemViewModel();
            addFoodItemViewModel.PropertyChanged += addDailyFoodViewModel.PropertyChangedFoodItemSnacks;//sign the AddFoodItemViewModel to when property change
            //put the addDailyFoodViewModel details in addFoodItemViewModel
            addFoodItemViewModel.FoodNameProperty = ((FoodItem)e.NewValue).Name;
            addFoodItemViewModel.FoodKey = ((FoodItem)e.NewValue).Key;
            addFoodItemViewModel.Calories100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Calories;
            addFoodItemViewModel.Carbs100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Carbohydrate;
            addFoodItemViewModel.Proteins100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Protien;
            addFoodItemViewModel.Fats100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Fats;
            tmpLst.Add(addFoodItemViewModel);
            addDailyFoodViewModel.SnacksListProperty = tmpLst;
        }

        /// <summary>
        ///This function add the match value to each total propery after adding the addFoodItemViewModel food to one of the dailyFood lists
        /// </summary>
        /// <param name="addFoodItemViewModel">the added food</param>
        private void updateUp100GmTotalComponents(AddFoodItemViewModel addFoodItemViewModel)
        {
            TotalCarbsProperty = (float.Parse(TotalCarbsProperty) + addFoodItemViewModel.Carbs100Gm).ToString();
            TotalProteinsProperty = (float.Parse(TotalProteinsProperty) + addFoodItemViewModel.Proteins100Gm).ToString();
            TotalFatsProperty = (float.Parse(TotalFatsProperty) + addFoodItemViewModel.Fats100Gm).ToString();
            TotalCaloriesProperty = (float.Parse(TotalCaloriesProperty) + addFoodItemViewModel.Calories100Gm).ToString();
        }

        /// <summary>
        ///This function sub the match value to each total propery after removing the addFoodItemViewModel food from one of the dailyFood lists
        /// </summary>
        /// <param name="addFoodItemViewModel">the removed food</param>
        private void updateDown100GmTotalComponents(AddFoodItemViewModel addFoodItemViewModel)
        {
            TotalCarbsProperty = (float.Parse(TotalCarbsProperty) - addFoodItemViewModel.Carbs100Gm).ToString();
            TotalProteinsProperty = (float.Parse(TotalProteinsProperty) - addFoodItemViewModel.Proteins100Gm).ToString();
            TotalFatsProperty = (float.Parse(TotalFatsProperty) - addFoodItemViewModel.Fats100Gm).ToString();
            TotalCaloriesProperty = (float.Parse(TotalCaloriesProperty) - addFoodItemViewModel.Calories100Gm).ToString();
        }

        /// <summary>
        ///This function sub the match value to each total propery after removing the addFoodItemViewModel food from one of the dailyFood lists(with notice to the food amount)
        /// </summary>
        /// <param name="addFoodItemViewModel">the removed food</param>
        private void updateDownItemTotalComponents(AddFoodItemViewModel addFoodItemViewModel)
        {
            TotalCarbsProperty = (float.Parse(TotalCarbsProperty) - (addFoodItemViewModel.Carbs100Gm) * (int.Parse(addFoodItemViewModel.FoodAmountProperty))).ToString();
            TotalProteinsProperty = (float.Parse(TotalProteinsProperty) - (addFoodItemViewModel.Proteins100Gm) * (int.Parse(addFoodItemViewModel.FoodAmountProperty))).ToString();
            TotalFatsProperty = (float.Parse(TotalFatsProperty) - (addFoodItemViewModel.Fats100Gm) * (int.Parse(addFoodItemViewModel.FoodAmountProperty))).ToString();
            TotalCaloriesProperty = (float.Parse(TotalCaloriesProperty) - (addFoodItemViewModel.Calories100Gm) * (int.Parse(addFoodItemViewModel.FoodAmountProperty))).ToString();
        }


        /// <summary>
        ///this function save all the dailyFoods in the db
        /// </summary>
        public void saveMeals()
        {
            //temp lists
            List<FoodItem> breakfast = new List<FoodItem>();
            List<FoodItem> brunch = new List<FoodItem>();
            List<FoodItem> dinner = new List<FoodItem>();
            List<FoodItem> snacks = new List<FoodItem>();
            foreach (var item in BreakfastListProperty)//for each addFoodItemViewModel in BreakfastListProperty create new match FoodItem and add it to the temp FoodItem list.
            {
                breakfast.Add(new FoodItem()
                {
                    Name = item.FoodNameProperty,
                    Key = item.FoodKey,
                    AmountGm = int.Parse(item.FoodAmountProperty),
                    Calories100G = item.Calories100Gm
                });
            }

            foreach (var item in BrunchListProperty)//for each addFoodItemViewModel in BrunchListProperty create new match FoodItem and add it to the temp FoodItem list.
            {
                brunch.Add(new FoodItem()
                {
                    Name = item.FoodNameProperty,
                    Key = item.FoodKey,
                    AmountGm = int.Parse(item.FoodAmountProperty),
                    Calories100G = item.Calories100Gm
                });
            }

            foreach (var item in DinnerListProperty)//for each addFoodItemViewModel in DinnerListProperty create new match FoodItem and add it to the temp FoodItem list.
            {
                dinner.Add(new FoodItem()
                {
                    Name = item.FoodNameProperty,
                    Key = item.FoodKey,
                    AmountGm = int.Parse(item.FoodAmountProperty),
                    Calories100G = item.Calories100Gm
                });
            }

            foreach (var item in SnacksListProperty)//for each addFoodItemViewModel in SnacksListProperty create new match FoodItem and add it to the temp FoodItem list.
            {
                snacks.Add(new FoodItem()
                {
                    Name = item.FoodNameProperty,
                    Key = item.FoodKey,
                    AmountGm = int.Parse(item.FoodAmountProperty),
                    Calories100G = item.Calories100Gm
                });
            }
            addDailyFoodModel.saveDailyFood(new DailyFood { CurrentDate = DateProperty, DailyActivity = (ACTIVITY)ActivityProperty, DailyMood = (MOOD)MoodProperty, EmailAddress = EmailAddressProperty, TotalCalories = float.Parse(TotalCaloriesProperty), TotalCarbs = float.Parse(TotalCarbsProperty), TotalFats = float.Parse(TotalFatsProperty), TotalPortiens = float.Parse(TotalProteinsProperty) });//save dailyFood according the current details in properties
            addDailyFoodModel.saveMeals(DateProperty, EmailAddressProperty, breakfast, brunch, dinner, snacks);//save lists
            IsOpenSaveChangeProperty = true;//open popUp
            startTimerPopUpToolTip(2.2);//popUp timer
        }


        public AddDailyFoodViewModel(String emailAddress)//constructor
        {
            EmailAddressProperty = emailAddress;
            addDailyFoodModel = new AddDailyFoodModel();
            SaveCommand = new SaveDateMealsCommand();
            BreakfastCaloriesProperty = "0";
            BrunchCaloriesProperty = "0";
            DinnerCaloriesProperty = "0";
            SnacksCaloriesProperty = "0";
            TotalCarbsProperty = "0";
            TotalProteinsProperty = "0";
            TotalFatsProperty = "0";
            TotalCaloriesProperty = "0";
            BreakfastListProperty = new ObservableCollection<AddFoodItemViewModel>();
            BrunchListProperty = new ObservableCollection<AddFoodItemViewModel>();
            DinnerListProperty = new ObservableCollection<AddFoodItemViewModel>();
            SnacksListProperty = new ObservableCollection<AddFoodItemViewModel>();
            IsOpenSaveChangeProperty = false;
        }

        /// <summary>
        ///this event call onPropertyChange of AddFoodItemViewModel of Breakfast meal type
        /// </summary>
        /// <param name="sender"> the sender object</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void PropertyChangedFoodItemBreakfast(object sender, PropertyChangedEventArgs e)
        {
            AddFoodItemViewModel addFoodItemViewModel = (sender as AddFoodItemViewModel);
            switch (e.PropertyName)
            {
                case "deleteItem"://delete the addFoodItemViewModel from BreakfastListProperty and all the total fields
                    BreakfastCaloriesProperty = (float.Parse(BreakfastCaloriesProperty) - float.Parse(addFoodItemViewModel.CaloriesProperty)).ToString();
                    updateDownItemTotalComponents(addFoodItemViewModel);
                    BreakfastListProperty.Remove(addFoodItemViewModel);
                    break;
                case "FoodAmountPropertyUp"://the amount of the fooditem increased
                    if (BreakfastCaloriesProperty != null && (sender as AddFoodItemViewModel).FoodAmountProperty != null)
                    {
                        BreakfastCaloriesProperty = (float.Parse(BreakfastCaloriesProperty) + ((sender as AddFoodItemViewModel).Calories100Gm)).ToString();
                        updateUp100GmTotalComponents(addFoodItemViewModel);
                    }
                    break;
                case "FoodAmountProperty"://the amount of the item is decreased
                    if (BreakfastCaloriesProperty != null && (sender as AddFoodItemViewModel).FoodAmountProperty != null)
                    {
                        BreakfastCaloriesProperty = (float.Parse(BreakfastCaloriesProperty) - ((sender as AddFoodItemViewModel).Calories100Gm)).ToString();
                        updateDown100GmTotalComponents(addFoodItemViewModel);
                    }

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///this event call onPropertyChange of AddFoodItemViewModel of Brunch meal type
        /// </summary>
        /// <param name="sender"> the sender object</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void PropertyChangedFoodItemBrunch(object sender, PropertyChangedEventArgs e)
        {
            AddFoodItemViewModel addFoodItemViewModel = (sender as AddFoodItemViewModel);
            switch (e.PropertyName)
            {
                case "deleteItem"://delete the addFoodItemViewModel from BrunchListProperty and all the total fields
                    BrunchCaloriesProperty = (float.Parse(BrunchCaloriesProperty) - float.Parse(addFoodItemViewModel.CaloriesProperty)).ToString();
                    updateDownItemTotalComponents(addFoodItemViewModel);
                    BrunchListProperty.Remove(addFoodItemViewModel);
                    break;
                case "FoodAmountPropertyUp"://the amount of the fooditem increased
                    if (BrunchCaloriesProperty != null && (sender as AddFoodItemViewModel).FoodAmountProperty != null)
                    {
                        BrunchCaloriesProperty = (float.Parse(BrunchCaloriesProperty) + ((sender as AddFoodItemViewModel).Calories100Gm)).ToString();
                        updateUp100GmTotalComponents(addFoodItemViewModel);
                    }
                    break;
                case "FoodAmountProperty"://the amount of the item is decreased
                    if (BrunchCaloriesProperty != null && (sender as AddFoodItemViewModel).FoodAmountProperty != null)
                    {
                        BrunchCaloriesProperty = (float.Parse(BrunchCaloriesProperty) - ((sender as AddFoodItemViewModel).Calories100Gm)).ToString();
                        updateDown100GmTotalComponents(addFoodItemViewModel);
                    }

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///this event call onPropertyChange of AddFoodItemViewModel of Dinner meal type
        /// </summary>
        /// <param name="sender"> the sender object</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void PropertyChangedFoodItemDinner(object sender, PropertyChangedEventArgs e)
        {
            AddFoodItemViewModel addFoodItemViewModel = (sender as AddFoodItemViewModel);
            switch (e.PropertyName)
            {
                case "deleteItem"://delete the addFoodItemViewModel from DinnerListProperty and all the total fields
                    DinnerCaloriesProperty = (float.Parse(DinnerCaloriesProperty) - float.Parse(addFoodItemViewModel.CaloriesProperty)).ToString();
                    updateDownItemTotalComponents(addFoodItemViewModel);
                    DinnerListProperty.Remove(addFoodItemViewModel);
                    break;
                case "FoodAmountPropertyUp"://the amount of the fooditem increased
                    if (DinnerCaloriesProperty != null && (sender as AddFoodItemViewModel).FoodAmountProperty != null)
                    {
                        DinnerCaloriesProperty = (float.Parse(DinnerCaloriesProperty) + ((sender as AddFoodItemViewModel).Calories100Gm)).ToString();
                        updateUp100GmTotalComponents(addFoodItemViewModel);
                    }
                    break;
                case "FoodAmountProperty"://the amount of the item is decreased
                    if (DinnerCaloriesProperty != null && (sender as AddFoodItemViewModel).FoodAmountProperty != null)
                    {
                        DinnerCaloriesProperty = (float.Parse(DinnerCaloriesProperty) - ((sender as AddFoodItemViewModel).Calories100Gm)).ToString();
                        updateDown100GmTotalComponents(addFoodItemViewModel);
                    }

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///this event call onPropertyChange of AddFoodItemViewModel of Snacks meal type
        /// </summary>
        /// <param name="sender"> the sender object</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void PropertyChangedFoodItemSnacks(object sender, PropertyChangedEventArgs e)
        {
            AddFoodItemViewModel addFoodItemViewModel = (sender as AddFoodItemViewModel);
            switch (e.PropertyName)
            {
                case "deleteItem"://delete the addFoodItemViewModel from SnacksListProperty and all the total fields
                    SnacksCaloriesProperty = (float.Parse(SnacksCaloriesProperty) - float.Parse(addFoodItemViewModel.CaloriesProperty)).ToString();
                    updateDownItemTotalComponents(addFoodItemViewModel);
                    SnacksListProperty.Remove(addFoodItemViewModel);
                    break;
                case "FoodAmountPropertyUp"://the amount of the fooditem increased
                    if (SnacksCaloriesProperty != null && (sender as AddFoodItemViewModel).FoodAmountProperty != null)
                    {
                        SnacksCaloriesProperty = (float.Parse(SnacksCaloriesProperty) + ((sender as AddFoodItemViewModel).Calories100Gm)).ToString();
                        updateUp100GmTotalComponents(addFoodItemViewModel);
                    }
                    break;
                case "FoodAmountProperty"://the amount of the item is decreased
                    if (SnacksCaloriesProperty != null && (sender as AddFoodItemViewModel).FoodAmountProperty != null)
                    {
                        SnacksCaloriesProperty = (float.Parse(SnacksCaloriesProperty) - ((sender as AddFoodItemViewModel).Calories100Gm)).ToString();
                        updateDown100GmTotalComponents(addFoodItemViewModel);
                    }

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// //will close the toolTipPopUp after interval seconds
        /// </summary>
        /// <param name="interval">the time to stay the popUp open</param>
        private void startTimerPopUpToolTip(double interval)
        {
            DispatcherTimer time = new DispatcherTimer();
            time.Interval = TimeSpan.FromSeconds(interval);
            time.Start();
            time.Tick += delegate
            {
                time.Interval = TimeSpan.FromSeconds(interval);
                IsOpenSaveChangeProperty = false;
                time.Stop();
            };

        }
    }
}
