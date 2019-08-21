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

namespace project.ViewModel
{
    public class AddDailyFoodViewModel : DependencyObject
    {
        AddDailyFoodModel addDailyFoodModel;
        public ICommand SaveCommand { get; set; }


        public static readonly DependencyProperty Date = DependencyProperty.Register("DateProperty", typeof(DateTime), typeof(AddDailyFoodViewModel), new PropertyMetadata(DateTime.Now.Date));
        public DateTime DateProperty
        {
            get { return (DateTime)GetValue(Date); }
            set { SetValue(Date, value); }
        }

        public static readonly DependencyProperty EmailAddress = DependencyProperty.Register("EmailAddressProperty", typeof(String), typeof(AddDailyFoodViewModel), new PropertyMetadata("shosh@gmail.com"));
        public String EmailAddressProperty
        {
            get { return (String)GetValue(EmailAddress); }
            set { SetValue(EmailAddress, value); }
        }

        public void getDailyFoodByDate(DateTime selectedDate)
        {
            DailyFood df = addDailyFoodModel.getDailyFoodByDate(EmailAddressProperty, selectedDate);
            List<FoodItem> breakfastLst = addDailyFoodModel.getMealsList(EmailAddressProperty, selectedDate, MEALTIME.BREAKFAST);
            List<FoodItem> brunchLst = addDailyFoodModel.getMealsList(EmailAddressProperty, selectedDate, MEALTIME.BRUNCH);
            List<FoodItem> dinnerLst = addDailyFoodModel.getMealsList(EmailAddressProperty, selectedDate, MEALTIME.DINNER);
            List<FoodItem> snacksLst = addDailyFoodModel.getMealsList(EmailAddressProperty, selectedDate, MEALTIME.SNACKS);
            if (df != null)
            {
                MoodProperty = df.DailyMood;
                ActivityProperty = df.DailyActivity;
                TotalCaloriesProperty = df.TotalCalories.ToString();
                TotalCarbsProperty = df.TotalCarbs.ToString();
                TotalFatsProperty = df.TotalFats.ToString();
                TotalProteinsProperty = df.TotalPortiens.ToString();
            }
            else
            {
                //  MoodProperty = ;
                //ActivityProperty = ;
                TotalCaloriesProperty = "0";
                TotalCarbsProperty = "0";
                TotalFatsProperty = "0";
                TotalProteinsProperty = "0";

            }
            BreakfastListProperty = convertFoodItemListToObs(breakfastLst,MEALTIME.BREAKFAST);
            BrunchListProperty = convertFoodItemListToObs(brunchLst,MEALTIME.BRUNCH);
            DinnerListProperty = convertFoodItemListToObs(dinnerLst, MEALTIME.DINNER);
            SnacksListProperty = convertFoodItemListToObs(snacksLst, MEALTIME.SNACKS);
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
                BrunchCaloriesProperty = (float.Parse(BrunchCaloriesProperty) + +item.Calories100Gm * float.Parse(item.FoodAmountProperty)).ToString();
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

        ObservableCollection<AddFoodItemViewModel> convertFoodItemListToObs(List<FoodItem> lst,MEALTIME mt)
        {
            ObservableCollection<AddFoodItemViewModel> result = new ObservableCollection<AddFoodItemViewModel>();
            AddFoodItemViewModel foodItemVm;
            foreach (var item in lst)
            {
                foodItemVm = new AddFoodItemViewModel() { FoodNameProperty = item.Name, FoodKey = item.Key, FoodAmountProperty = item.AmountGm.ToString(), Calories100Gm = item.Calories100G };
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
                
                result.Add(foodItemVm);
            }
            return result;
        }


        public static readonly DependencyProperty Mood = DependencyProperty.Register("MoodProperty", typeof(MOOD), typeof(AddDailyFoodViewModel));
        public MOOD MoodProperty
        {
            get { return (MOOD)GetValue(Mood); }
            set { SetValue(Mood, value); }
        }

        public static readonly DependencyProperty Activity = DependencyProperty.Register("ActivityProperty", typeof(ACTIVITY), typeof(AddDailyFoodViewModel));
        public ACTIVITY ActivityProperty
        {
            get { return (ACTIVITY)GetValue(Activity); }
            set { SetValue(Activity, value); }
        }

        public static readonly DependencyProperty TotalCalories = DependencyProperty.Register("TotalCaloriesProperty", typeof(String), typeof(AddDailyFoodViewModel));
        public String TotalCaloriesProperty
        {
            get { return (String)GetValue(TotalCalories); }
            set { if (float.Parse(value) > 0 && !value.Contains("E")) SetValue(TotalCalories, value); else SetValue(TotalCalories, "0"); }
        }

        public static readonly DependencyProperty TotalFats = DependencyProperty.Register("TotalFatsProperty", typeof(String), typeof(AddDailyFoodViewModel));
        public String TotalFatsProperty
        {
            get { return (String)GetValue(TotalFats); }
            set { if (float.Parse(value) > 0 && !value.Contains("E")) SetValue(TotalFats, value); else SetValue(TotalFats, "0"); }
        }

        public static readonly DependencyProperty TotalCarbs = DependencyProperty.Register("TotalCarbsProperty", typeof(String), typeof(AddDailyFoodViewModel));
        public String TotalCarbsProperty
        {
            get { return (String)GetValue(TotalCarbs); }
            set { if (float.Parse(value) > 0 && !value.Contains("E")) SetValue(TotalCarbs, value); else SetValue(TotalCarbs, "0"); }
        }

        public static readonly DependencyProperty TotalProteins = DependencyProperty.Register("TotalProteinsProperty", typeof(String), typeof(AddDailyFoodViewModel));
        public String TotalProteinsProperty
        {
            get { return (String)GetValue(TotalProteins); }
            set { if (float.Parse(value) > 0 && !value.Contains("E")) SetValue(TotalProteins, value); else SetValue(TotalProteins, "0"); }
        }
        public static readonly DependencyProperty BreakfastCalories = DependencyProperty.Register("BreakfastCaloriesProperty", typeof(String), typeof(AddDailyFoodViewModel));
        public String BreakfastCaloriesProperty
        {
            get { return (String)GetValue(BreakfastCalories); }
            set { SetValue(BreakfastCalories, value); }
        }

        public static readonly DependencyProperty BrunchfastCalories = DependencyProperty.Register("BrunchfastCaloriesProperty", typeof(String), typeof(AddDailyFoodViewModel));
        public String BrunchfastCaloriesProperty
        {
            get { return (String)GetValue(BrunchfastCalories); }
            set { SetValue(BrunchfastCalories, value); }
        }

        public static readonly DependencyProperty DinnerCalories = DependencyProperty.Register("DinnerCaloriesProperty", typeof(String), typeof(AddDailyFoodViewModel));
        public String DinnerCaloriesProperty
        {
            get { return (String)GetValue(DinnerCalories); }
            set { SetValue(DinnerCalories, value); }
        }

        public static readonly DependencyProperty BrunchCalories = DependencyProperty.Register("BrunchCaloriesProperty", typeof(String), typeof(AddDailyFoodViewModel));
        public String BrunchCaloriesProperty
        {
            get { return (String)GetValue(BrunchCalories); }
            set { SetValue(BrunchCalories, value); }
        }

        public static readonly DependencyProperty SnacksCalories = DependencyProperty.Register("SnacksCaloriesProperty", typeof(String), typeof(AddDailyFoodViewModel));
        public String SnacksCaloriesProperty
        {
            get { return (String)GetValue(SnacksCalories); }
            set { SetValue(SnacksCalories, value); }
        }

        public static readonly DependencyProperty BreakfastList = DependencyProperty.Register("BreakfastListProperty", typeof(ObservableCollection<AddFoodItemViewModel>), typeof(AddDailyFoodViewModel));
        public ObservableCollection<AddFoodItemViewModel> BreakfastListProperty
        {
            get { return (ObservableCollection<AddFoodItemViewModel>)GetValue(BreakfastList); }
            set { SetValue(BreakfastList, value); }
        }

        public static readonly DependencyProperty BrunchList = DependencyProperty.Register("BrunchListProperty", typeof(ObservableCollection<AddFoodItemViewModel>), typeof(AddDailyFoodViewModel));
        public ObservableCollection<AddFoodItemViewModel> BrunchListProperty
        {
            get { return (ObservableCollection<AddFoodItemViewModel>)GetValue(BrunchList); }
            set { SetValue(BrunchList, value); }
        }

        public static readonly DependencyProperty DinnerList = DependencyProperty.Register("DinnerListProperty", typeof(ObservableCollection<AddFoodItemViewModel>), typeof(AddDailyFoodViewModel));
        public ObservableCollection<AddFoodItemViewModel> DinnerListProperty
        {
            get { return (ObservableCollection<AddFoodItemViewModel>)GetValue(DinnerList); }
            set { SetValue(DinnerList, value); }
        }

        public static readonly DependencyProperty SnacksList = DependencyProperty.Register("SnacksListProperty", typeof(ObservableCollection<AddFoodItemViewModel>), typeof(AddDailyFoodViewModel));
        public ObservableCollection<AddFoodItemViewModel> SnacksListProperty
        {
            get { return (ObservableCollection<AddFoodItemViewModel>)GetValue(SnacksList); }
            set { SetValue(SnacksList, value); }
        }


        public static readonly DependencyProperty SelectedFoodToAddBreakfast = DependencyProperty.Register("SelectedFoodToAddBreakfastProperty", typeof(FoodItem), typeof(AddDailyFoodViewModel), new UIPropertyMetadata(new PropertyChangedCallback(OnSelectedFoodItemBreakfast)));
        public FoodItem SelectedFoodToAddBreakfastProperty
        {
            get { return (FoodItem)GetValue(SelectedFoodToAddBreakfast); }
            set { SetValue(SelectedFoodToAddBreakfast, value); }
        }
        private static void OnSelectedFoodItemBreakfast(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddDailyFoodViewModel addDailyFoodViewModel = d as AddDailyFoodViewModel;
            ObservableCollection<AddFoodItemViewModel> tmpLst;
            if (addDailyFoodViewModel.BreakfastListProperty == null)
                tmpLst = new ObservableCollection<AddFoodItemViewModel>();
            else
                tmpLst = addDailyFoodViewModel.BreakfastListProperty;
            AddFoodItemViewModel addFoodItemViewModel = new AddFoodItemViewModel();
            addFoodItemViewModel.PropertyChanged += addDailyFoodViewModel.PropertyChangedFoodItemBreakfast;
            addFoodItemViewModel.FoodNameProperty = ((FoodItem)e.NewValue).Name;
            addFoodItemViewModel.FoodKey = ((FoodItem)e.NewValue).Key;
            addFoodItemViewModel.Calories100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Calories;
            addFoodItemViewModel.Carbs100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Carbohydrate;
            addFoodItemViewModel.Proteins100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Protien;
            addFoodItemViewModel.Fats100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Fats;
            tmpLst.Add(addFoodItemViewModel);
            addDailyFoodViewModel.BreakfastListProperty = tmpLst;
        }

        public static readonly DependencyProperty SelectedFoodToAddBrunch = DependencyProperty.Register("SelectedFoodToAddBrunchProperty", typeof(FoodItem), typeof(AddDailyFoodViewModel), new UIPropertyMetadata(new PropertyChangedCallback(OnSelectedFoodItemBrunch)));
        public FoodItem SelectedFoodToAddBrunchProperty
        {
            get { return (FoodItem)GetValue(SelectedFoodToAddBrunch); }
            set { SetValue(SelectedFoodToAddBrunch, value); }
        }
        private static void OnSelectedFoodItemBrunch(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddDailyFoodViewModel addDailyFoodViewModel = d as AddDailyFoodViewModel;
            ObservableCollection<AddFoodItemViewModel> tmpLst;
            if (addDailyFoodViewModel.BrunchListProperty == null)
                tmpLst = new ObservableCollection<AddFoodItemViewModel>();
            else
                tmpLst = addDailyFoodViewModel.BrunchListProperty;
            AddFoodItemViewModel addFoodItemViewModel = new AddFoodItemViewModel();
            addFoodItemViewModel.PropertyChanged += addDailyFoodViewModel.PropertyChangedFoodItemBrunch;
            addFoodItemViewModel.FoodNameProperty = ((FoodItem)e.NewValue).Name;
            addFoodItemViewModel.FoodKey = ((FoodItem)e.NewValue).Key;
            addFoodItemViewModel.Calories100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Calories;
            addFoodItemViewModel.Carbs100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Carbohydrate;
            addFoodItemViewModel.Proteins100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Protien;
            addFoodItemViewModel.Fats100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Fats;
            tmpLst.Add(addFoodItemViewModel);
            addDailyFoodViewModel.BrunchListProperty = tmpLst;
        }

        public static readonly DependencyProperty SelectedFoodToAddDinner = DependencyProperty.Register("SelectedFoodToAddDinnerProperty", typeof(FoodItem), typeof(AddDailyFoodViewModel), new UIPropertyMetadata(new PropertyChangedCallback(OnSelectedFoodItemDinner)));
        public FoodItem SelectedFoodToAddDinnerProperty
        {
            get { return (FoodItem)GetValue(SelectedFoodToAddDinner); }
            set { SetValue(SelectedFoodToAddDinner, value); }
        }
        private static void OnSelectedFoodItemDinner(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddDailyFoodViewModel addDailyFoodViewModel = d as AddDailyFoodViewModel;
            ObservableCollection<AddFoodItemViewModel> tmpLst;
            if (addDailyFoodViewModel.DinnerListProperty == null)
                tmpLst = new ObservableCollection<AddFoodItemViewModel>();
            else
                tmpLst = addDailyFoodViewModel.DinnerListProperty;
            AddFoodItemViewModel addFoodItemViewModel = new AddFoodItemViewModel();
            addFoodItemViewModel.PropertyChanged += addDailyFoodViewModel.PropertyChangedFoodItemDinner;
            addFoodItemViewModel.FoodNameProperty = ((FoodItem)e.NewValue).Name;
            addFoodItemViewModel.FoodKey = ((FoodItem)e.NewValue).Key;
            addFoodItemViewModel.Calories100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Calories;
            addFoodItemViewModel.Carbs100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Carbohydrate;
            addFoodItemViewModel.Proteins100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Protien;
            addFoodItemViewModel.Fats100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Fats;
            tmpLst.Add(addFoodItemViewModel);
            addDailyFoodViewModel.DinnerListProperty = tmpLst;
        }

        public static readonly DependencyProperty SelectedFoodToAddSnacks = DependencyProperty.Register("SelectedFoodToAddSnacksProperty", typeof(FoodItem), typeof(AddDailyFoodViewModel), new UIPropertyMetadata(new PropertyChangedCallback(OnSelectedFoodItemSnacks)));
        public FoodItem SelectedFoodToAddSnacksProperty
        {
            get { return (FoodItem)GetValue(SelectedFoodToAddSnacks); }
            set { SetValue(SelectedFoodToAddSnacks, value); }
        }
        private static void OnSelectedFoodItemSnacks(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddDailyFoodViewModel addDailyFoodViewModel = d as AddDailyFoodViewModel;
            ObservableCollection<AddFoodItemViewModel> tmpLst;
            if (addDailyFoodViewModel.SnacksListProperty == null)
                tmpLst = new ObservableCollection<AddFoodItemViewModel>();
            else
                tmpLst = addDailyFoodViewModel.SnacksListProperty;
            AddFoodItemViewModel addFoodItemViewModel = new AddFoodItemViewModel();
            addFoodItemViewModel.PropertyChanged += addDailyFoodViewModel.PropertyChangedFoodItemSnacks;
            addFoodItemViewModel.FoodNameProperty = ((FoodItem)e.NewValue).Name;
            addFoodItemViewModel.FoodKey = ((FoodItem)e.NewValue).Key;
            addFoodItemViewModel.Calories100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Calories;
            addFoodItemViewModel.Carbs100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Carbohydrate;
            addFoodItemViewModel.Proteins100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Protien;
            addFoodItemViewModel.Fats100Gm = (addDailyFoodViewModel.addDailyFoodModel.getFoodDetails(((FoodItem)e.NewValue).Key)).Fats;
            tmpLst.Add(addFoodItemViewModel);
            addDailyFoodViewModel.SnacksListProperty = tmpLst;
        }
        private void updateUp100GmTotalComponents(AddFoodItemViewModel addFoodItemViewModel)
        {
            TotalCarbsProperty = (float.Parse(TotalCarbsProperty) + addFoodItemViewModel.Carbs100Gm).ToString();
            TotalProteinsProperty = (float.Parse(TotalProteinsProperty) + addFoodItemViewModel.Proteins100Gm).ToString();
            TotalFatsProperty = (float.Parse(TotalFatsProperty) + addFoodItemViewModel.Fats100Gm).ToString();
            TotalCaloriesProperty = (float.Parse(TotalCaloriesProperty) + addFoodItemViewModel.Calories100Gm).ToString();
        }

        private void updateDown100GmTotalComponents(AddFoodItemViewModel addFoodItemViewModel)
        {
            TotalCarbsProperty = (float.Parse(TotalCarbsProperty) - addFoodItemViewModel.Carbs100Gm).ToString();
            TotalProteinsProperty = (float.Parse(TotalProteinsProperty) - addFoodItemViewModel.Proteins100Gm).ToString();
            TotalFatsProperty = (float.Parse(TotalFatsProperty) - addFoodItemViewModel.Fats100Gm).ToString();
            TotalCaloriesProperty = (float.Parse(TotalCaloriesProperty) - addFoodItemViewModel.Calories100Gm).ToString();
        }

        private void updateDownItemTotalComponents(AddFoodItemViewModel addFoodItemViewModel)
        {
            TotalCarbsProperty = (float.Parse(TotalCarbsProperty) - (addFoodItemViewModel.Carbs100Gm) * (int.Parse(addFoodItemViewModel.FoodAmountProperty))).ToString();
            TotalProteinsProperty = (float.Parse(TotalProteinsProperty) - (addFoodItemViewModel.Proteins100Gm) * (int.Parse(addFoodItemViewModel.FoodAmountProperty))).ToString();
            TotalFatsProperty = (float.Parse(TotalFatsProperty) - (addFoodItemViewModel.Fats100Gm) * (int.Parse(addFoodItemViewModel.FoodAmountProperty))).ToString();
            TotalCaloriesProperty = (float.Parse(TotalCaloriesProperty) - (addFoodItemViewModel.Calories100Gm) * (int.Parse(addFoodItemViewModel.FoodAmountProperty))).ToString();
        }

        public void saveMeals()
        {
            List<FoodItem> breakfast = new List<FoodItem>();
            List<FoodItem> brunch = new List<FoodItem>();
            List<FoodItem> dinner = new List<FoodItem>();
            List<FoodItem> snacks = new List<FoodItem>();
            foreach (var item in BreakfastListProperty)
            {
                breakfast.Add(new FoodItem()
                {
                    Name = item.FoodNameProperty,
                    Key = item.FoodKey,
                    AmountGm = int.Parse(item.FoodAmountProperty),
                    Calories100G = item.Calories100Gm
                });
            }

            foreach (var item in BrunchListProperty)
            {
                brunch.Add(new FoodItem()
                {
                    Name = item.FoodNameProperty,
                    Key = item.FoodKey,
                    AmountGm = int.Parse(item.FoodAmountProperty),
                    Calories100G = item.Calories100Gm
                });
            }

            foreach (var item in DinnerListProperty)
            {
                dinner.Add(new FoodItem()
                {
                    Name = item.FoodNameProperty,
                    Key = item.FoodKey,
                    AmountGm = int.Parse(item.FoodAmountProperty),
                    Calories100G = item.Calories100Gm
                });
            }

            foreach (var item in SnacksListProperty)
            {
                snacks.Add(new FoodItem()
                {
                    Name = item.FoodNameProperty,
                    Key = item.FoodKey,
                    AmountGm = int.Parse(item.FoodAmountProperty),
                    Calories100G = item.Calories100Gm
                });
            }
            addDailyFoodModel.saveDailyFood(new DailyFood { CurrentDate = DateProperty, DailyActivity = ActivityProperty, DailyMood = MoodProperty, EmailAddress = EmailAddressProperty, TotalCalories = float.Parse(TotalCaloriesProperty), TotalCarbs = float.Parse(TotalCarbsProperty), TotalFats = float.Parse(TotalFatsProperty), TotalPortiens = float.Parse(TotalProteinsProperty) });
            addDailyFoodModel.saveMeals(DateProperty, EmailAddressProperty, breakfast, brunch, dinner, snacks);
        }


        public AddDailyFoodViewModel()
        {
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

        }


        private void PropertyChangedFoodItemBreakfast(object sender, PropertyChangedEventArgs e)
        {
            AddFoodItemViewModel addFoodItemViewModel = (sender as AddFoodItemViewModel);
            switch (e.PropertyName)
            {
                case "deleteItem":

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

        private void PropertyChangedFoodItemBrunch(object sender, PropertyChangedEventArgs e)
        {
            AddFoodItemViewModel addFoodItemViewModel = (sender as AddFoodItemViewModel);
            switch (e.PropertyName)
            {
                case "deleteItem":

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

        private void PropertyChangedFoodItemDinner(object sender, PropertyChangedEventArgs e)
        {
            AddFoodItemViewModel addFoodItemViewModel = (sender as AddFoodItemViewModel);
            switch (e.PropertyName)
            {
                case "deleteItem":

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

        private void PropertyChangedFoodItemSnacks(object sender, PropertyChangedEventArgs e)
        {
            AddFoodItemViewModel addFoodItemViewModel = (sender as AddFoodItemViewModel);
            switch (e.PropertyName)
            {
                case "deleteItem":

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

    }
}
