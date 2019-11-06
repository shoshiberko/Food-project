using BE;
using project.Commands;
using project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace project.ViewModel
{
    public class MonthChartUserControlVM : DependencyObject
    {
#region properties
        List<DailyFood> dailyFoodLst;//list of DailyFood

        /// <summary>
        ///this property contain the user email address to identify him
        /// </summary>
        public static readonly DependencyProperty EmailAddress = DependencyProperty.Register("EmailAddressProperty", typeof(String), typeof(MonthChartUserControlVM), new PropertyMetadata("shosh@gmail.com"));
        public String EmailAddressProperty
        {
            get { return (String)GetValue(EmailAddress); }
            set { SetValue(EmailAddress, value); }
        }

        /// <summary>
        ///this property contain true if all the chartLines need to be checked, else-false.
        /// </summary>
        public static readonly DependencyProperty IsCheckedAllChartsLines = DependencyProperty.Register("IsCheckedAllChartsLinesProperty", typeof(Boolean), typeof(MonthChartUserControlVM));
        public Boolean IsCheckedAllChartsLinesProperty
        {
            get { return (Boolean)GetValue(IsCheckedAllChartsLines); }
            set { SetValue(IsCheckedAllChartsLines, value); }
        }

        /// <summary>
        ///this property contain the user totalCalories per DateTime
        /// </summary>
        public static readonly DependencyProperty ChartListCalories = DependencyProperty.Register("ChartListCaloriesProperty", typeof(ObservableCollection<KeyValuePair<DateTime, int>>), typeof(MonthChartUserControlVM));
        public ObservableCollection<KeyValuePair<DateTime, int>> ChartListCaloriesProperty
        {
            get { return (ObservableCollection<KeyValuePair<DateTime, int>>)GetValue(ChartListCalories); }
            set { SetValue(ChartListCalories, value); }
        }

        /// <summary>
        ///this property contain list of weekGoals strings(fats,calories,carbs and protiens)
        /// </summary>
        public static readonly DependencyProperty GoalsMonthComponentsList = DependencyProperty.Register("GoalsMonthComponentsListProperty", typeof(ObservableCollection<String>), typeof(MonthChartUserControlVM));
        public ObservableCollection<String> GoalsMonthComponentsListProperty
        {
            get { return (ObservableCollection<String>)GetValue(GoalsMonthComponentsList); }
            set { SetValue(GoalsMonthComponentsList, value); }
        }

        /// <summary>
        ///this property contain list of taotals of the month(total of fats ,protiens ,carbs and calories)
        /// </summary>
        public static readonly DependencyProperty TotalMonthComponentsList = DependencyProperty.Register("TotalMonthComponentsListProperty", typeof(ObservableCollection<String>), typeof(MonthChartUserControlVM));
        public ObservableCollection<String> TotalMonthComponentsListProperty
        {
            get { return (ObservableCollection<String>)GetValue(TotalMonthComponentsList); }
            set { SetValue(TotalMonthComponentsList, value); }
        }

        /// <summary>
        ///this property contain collection of <key,value> when the key is fats/protiens/calories/carbs and the value it's a amount of the key for month according the month goals that the user entered
        /// </summary>
        public static readonly DependencyProperty GoalsMonthColumns = DependencyProperty.Register("GoalsMonthColumnsProperty", typeof(ObservableCollection<KeyValuePair<String, int>>), typeof(MonthChartUserControlVM));
        public ObservableCollection<KeyValuePair<String, int>> GoalsMonthColumnsProperty
        {
            get { return (ObservableCollection<KeyValuePair<String, int>>)GetValue(GoalsMonthColumns); }
            set { SetValue(GoalsMonthColumns, value); }
        }

        /// <summary>
        ///this property contain collection of <key,value> when the key is fats/protiens/calories/carbs and the value it's a amount of the key for month(not of goals-what actually happened)
        /// </summary>
        public static readonly DependencyProperty TotalMonthColumns = DependencyProperty.Register("TotalMonthColumnsProperty", typeof(ObservableCollection<KeyValuePair<String, int>>), typeof(MonthChartUserControlVM));
        public ObservableCollection<KeyValuePair<String, int>> TotalMonthColumnsProperty
        {
            get { return (ObservableCollection<KeyValuePair<String, int>>)GetValue(TotalMonthColumns); }
            set { SetValue(TotalMonthColumns, value); }
        }

        /// <summary>
        ///this property contain list that describe the line of the graph of fats-of date and the number of fats in that day
        /// </summary>
        public static readonly DependencyProperty ChartListFats = DependencyProperty.Register("ChartListFatsProperty", typeof(ObservableCollection<KeyValuePair<DateTime, int>>), typeof(MonthChartUserControlVM));
        public ObservableCollection<KeyValuePair<DateTime, int>> ChartListFatsProperty
        {
            get { return (ObservableCollection<KeyValuePair<DateTime, int>>)GetValue(ChartListFats); }
            set { SetValue(ChartListFats, value); }
        }

        /// <summary>
        ///this property contain list that describe the line of the graph of carbs-of date and the number of carbs in that day
        /// </summary>
        public static readonly DependencyProperty ChartListCarbs = DependencyProperty.Register("ChartListCarbsProperty", typeof(ObservableCollection<KeyValuePair<DateTime, int>>), typeof(MonthChartUserControlVM));
        public ObservableCollection<KeyValuePair<DateTime, int>> ChartListCarbsProperty
        {
            get { return (ObservableCollection<KeyValuePair<DateTime, int>>)GetValue(ChartListCarbs); }
            set { SetValue(ChartListCarbs, value); }
        }

        /// <summary>
        ///this property contain list that describe the line of the graph of protiens-of date and the number of fats in that daythis property contain the user email address to identify him
        /// </summary>
        public static readonly DependencyProperty ChartListProteins = DependencyProperty.Register("ChartListProteinsProperty", typeof(ObservableCollection<KeyValuePair<DateTime, int>>), typeof(MonthChartUserControlVM));
        public ObservableCollection<KeyValuePair<DateTime, int>> ChartListProteinsProperty
        {
            get { return (ObservableCollection<KeyValuePair<DateTime, int>>)GetValue(ChartListProteins); }
            set { SetValue(ChartListProteins, value); }
        }

        /// <summary>
        ///this property contain the first sunday of the selected month
        /// </summary>
        public static readonly DependencyProperty sunday = DependencyProperty.Register("SundayProperty", typeof(DateTime), typeof(MonthChartUserControlVM),
                                                                                   new UIPropertyMetadata(new PropertyChangedCallback(OnSelectedSundayPropertyChanged)));
        public DateTime SundayProperty
        {
            get { return (DateTime)GetValue(sunday); }
            set { SetValue(sunday, value); }
        }

        /// <summary>
        ///this property contain the last date of the selected month
        /// </summary>
        public static readonly DependencyProperty EndMonth = DependencyProperty.Register("EndMonthProperty", typeof(DateTime), typeof(MonthChartUserControlVM),
                                                                                   new UIPropertyMetadata(new PropertyChangedCallback(OnSelectedSundayPropertyChanged)));
        public DateTime EndMonthProperty
        {
            get { return (DateTime)GetValue(EndMonth); }
            set { SetValue(EndMonth, value); }
        }

        public MonthChartUserControlModel Model { get; set; }//the connection to it's model


        #endregion

        public MonthChartUserControlVM(String emailAddress)//constructor
        {
            EmailAddressProperty = emailAddress;
            Model = new MonthChartUserControlModel();

        }
        /// <summary>
        ///this function change the userControlProperty according to the input string- and match all the view of the mainWindow according this userControl
        /// </summary>
        /// <param name="name">contain string that describe a user control</param>
        public void addChartLine(String name)
        {
            switch (name)
            {
                case "Calorieschart":
                    ChartListCaloriesProperty = convertComponentsList(SundayProperty, dailyFoodLst, "TotalCalories");
                    break;
                case "FatsChart":
                    ChartListFatsProperty = convertComponentsList(SundayProperty, dailyFoodLst, "TotalFats");
                    break;
                case "ProteinsChart":
                    ChartListProteinsProperty = convertComponentsList(SundayProperty, dailyFoodLst, "TotalPortiens");
                    break;
                case "CarbChart":
                    ChartListCarbsProperty = convertComponentsList(SundayProperty, dailyFoodLst, "TotalCarbs");
                    break;
            }
        }

        /// <summary>
        ///this function change the userControlProperty according to the input string- and match all the view of the mainWindow according this userControl
        /// </summary>
        /// <param name="name">contain string that describe a user control</param>
        public void removeChartLine(String name)
        {
            switch (name)
            {
                case "Calorieschart":
                    ChartListCaloriesProperty = new ObservableCollection<KeyValuePair<DateTime, int>>();
                    break;
                case "FatsChart":
                    ChartListFatsProperty = new ObservableCollection<KeyValuePair<DateTime, int>>();
                    break;
                case "ProteinsChart":
                    ChartListProteinsProperty = new ObservableCollection<KeyValuePair<DateTime, int>>();
                    break;
                case "CarbChart":
                    ChartListCarbsProperty = new ObservableCollection<KeyValuePair<DateTime, int>>();
                    break;
            }
        }

        /// <summary>
        ///this function fill the TotalMonthComponentsListProperty and the TotalMonthColumnsProperty that connected to the graph according the dailyFoodLst fields items
        /// </summary>
        private void sumTotalComponents()
        {
            float totalFats = 0, totalCarbs = 0, totalProteins = 0, totalCalories = 0;
            foreach (var item in dailyFoodLst)
            {
                if (item != null)
                {
                    totalCalories += item.TotalCalories;
                    totalFats += item.TotalFats;
                    totalCarbs += item.TotalCarbs;
                    totalProteins += item.TotalPortiens;
                }
            }

            TotalMonthComponentsListProperty = new ObservableCollection<string>() { ((int)totalCalories).ToString(), ((int)totalFats).ToString(),((int)totalProteins).ToString(), ((int)totalCarbs).ToString() };
            TotalMonthColumnsProperty = new ObservableCollection<KeyValuePair<string, int>>() {new KeyValuePair<string, int>("Calories",(int)totalCalories), new KeyValuePair<string, int>("Proteins", (int) totalProteins),
                new KeyValuePair<string, int>("Fats",(int) totalFats),
                new KeyValuePair<string, int>("Carbohydrates",(int)totalCarbs) };

        }

        /// <summary>
        ///this function get a DailyFoodList and return a match ObservableCollection<KeyValuePair<DateTime, int>>(convert between types)
        /// </summary>
        /// <param name="sunday">contain the first sunday ofmonth</param>
        /// /// <param name="DailyFoodLst">contain a list of dailyFoods</param>
        /// /// <param name="property">contain string that describe the type-totalCalories,totalFats, totalProtiens or totalCarbs</param>
        /// <returns></returns>
        private ObservableCollection<KeyValuePair<DateTime, int>> convertComponentsList(DateTime sunday, List<DailyFood> DailyFoodLst, string property)
        {
            ObservableCollection<KeyValuePair<DateTime, int>> tmp = new ObservableCollection<KeyValuePair<DateTime, int>>();
            for (int i = 0; i < 28; i++)
            {
                if (DailyFoodLst[i] != null)
                    tmp.Add(new KeyValuePair<DateTime, int>(DailyFoodLst[i].CurrentDate, (int)((float)DailyFoodLst[i][property])));
                else
                    tmp.Add(new KeyValuePair<DateTime, int>(sunday.AddDays(i), 0));
            }
            return tmp;
        }

        /// <summary>
        ///this function loads the WeekGoals- wg to the properties(to the screen) 
        /// </summary>
        /// <param name="wg">contain WeekGoals</param>
        private void loadWeekGoalsToProperties(WeekGoals wg)
        {

            if (wg != null)//get into GoalsMonthComponentsListProperty and GoalsMonthColumnsProperty lists that will show on screen
            {
                GoalsMonthComponentsListProperty = new ObservableCollection<string>() { wg.GoalCalories.ToString(), wg.GoalFats.ToString(), wg.GoalProteins.ToString(), wg.GoalCarbs.ToString() };
                GoalsMonthColumnsProperty = new ObservableCollection<KeyValuePair<string, int>>() {new KeyValuePair<string, int>("Calories", wg.GoalCalories), new KeyValuePair<string, int>("Fats", wg.GoalFats),   new KeyValuePair<string, int>("Proteins", wg.GoalProteins) ,
                    new KeyValuePair<string, int>("Carbohydrates", wg.GoalCarbs)};
            }
            else//clear
            {
                GoalsMonthComponentsListProperty = new ObservableCollection<string>() { "0", "0", "0", "0" };
                GoalsMonthColumnsProperty = new ObservableCollection<KeyValuePair<string, int>>() {new KeyValuePair<string, int>("Calories",0), new KeyValuePair<string, int>("Fats", 0), new KeyValuePair<string, int>("Proteins", 0),
                    new KeyValuePair<string, int>("Carbohydrates", 0) };
            }
        }

        /// <summary>
        ///this function done when the user change his choose of the sunday of start the month
        /// </summary>
        /// <param name="d">the event caller</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        private static void OnSelectedSundayPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MonthChartUserControlVM vm = (d as MonthChartUserControlVM);
            DateTime sundayMonth = (DateTime)(e.NewValue);
            vm.OnSelectedSundayPropertyChanged(sundayMonth);
        }

        /// <summary>
        ///this function done when the user change his choose of the sunday of start the month(called from the event)
        /// </summary>
        /// <param name="sundayMonth">the new first month sunday</param>
        private void OnSelectedSundayPropertyChanged(DateTime sundayMonth)
        {
            WeekGoals wg = Model.getMonthGoals(EmailAddressProperty, sundayMonth);//get the month goals
            loadWeekGoalsToProperties(wg);//load week goals of sunday's week of the user if it setted, else it clears the properties with 0

            dailyFoodLst = Model.getDailyFoodMonthList(EmailAddressProperty, sundayMonth);//get all the week dailyFood

            //refreshView- all the chartLines are checked
            IsCheckedAllChartsLinesProperty = false;
            IsCheckedAllChartsLinesProperty = true;
            sumTotalComponents();//put the update sum properties on screen

        }
    }
}

