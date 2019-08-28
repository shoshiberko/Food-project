using BE;
using project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace project.ViewModel
{
    public class MonthChartUserControlVM : DependencyObject
    {
        // public List<KeyValuePair<DateTime, int>> list1 { get; set; }
        List<DailyFood> dailyFoodLst;
        public static readonly DependencyProperty EmailAddress = DependencyProperty.Register("EmailAddressProperty", typeof(String), typeof(MonthChartUserControlVM), new PropertyMetadata("shosh@gmail.com"));
        public String EmailAddressProperty
        {
            get { return (String)GetValue(EmailAddress); }
            set { SetValue(EmailAddress, value); }
        }

        public static readonly DependencyProperty IsCheckedAllChartsLines = DependencyProperty.Register("IsCheckedAllChartsLinesProperty", typeof(Boolean), typeof(MonthChartUserControlVM));
        public Boolean IsCheckedAllChartsLinesProperty
        {
            get { return (Boolean)GetValue(IsCheckedAllChartsLines); }
            set { SetValue(IsCheckedAllChartsLines, value); }
        }

        public static readonly DependencyProperty ChartListCalories = DependencyProperty.Register("ChartListCaloriesProperty", typeof(ObservableCollection<KeyValuePair<DateTime, int>>), typeof(MonthChartUserControlVM));
        public ObservableCollection<KeyValuePair<DateTime, int>> ChartListCaloriesProperty
        {
            get { return (ObservableCollection<KeyValuePair<DateTime, int>>)GetValue(ChartListCalories); }
            set { SetValue(ChartListCalories, value); }
        }


        public static readonly DependencyProperty GoalsMonthComponentsList = DependencyProperty.Register("GoalsMonthComponentsListProperty", typeof(ObservableCollection<String>), typeof(MonthChartUserControlVM));
        public ObservableCollection<String> GoalsMonthComponentsListProperty
        {
            get { return (ObservableCollection<String>)GetValue(GoalsMonthComponentsList); }
            set { SetValue(GoalsMonthComponentsList, value); }
        }


        public static readonly DependencyProperty TotalMonthComponentsList = DependencyProperty.Register("TotalMonthComponentsListProperty", typeof(ObservableCollection<String>), typeof(MonthChartUserControlVM));
        public ObservableCollection<String> TotalMonthComponentsListProperty
        {
            get { return (ObservableCollection<String>)GetValue(TotalMonthComponentsList); }
            set { SetValue(TotalMonthComponentsList, value); }
        }

        public static readonly DependencyProperty GoalsMonthColumns = DependencyProperty.Register("GoalsMonthColumnsProperty", typeof(ObservableCollection<KeyValuePair<String, int>>), typeof(MonthChartUserControlVM));
        public ObservableCollection<KeyValuePair<String, int>> GoalsMonthColumnsProperty
        {
            get { return (ObservableCollection<KeyValuePair<String, int>>)GetValue(GoalsMonthColumns); }
            set { SetValue(GoalsMonthColumns, value); }
        }

        public static readonly DependencyProperty TotalMonthColumns = DependencyProperty.Register("TotalMonthColumnsProperty", typeof(ObservableCollection<KeyValuePair<String, int>>), typeof(MonthChartUserControlVM));
        public ObservableCollection<KeyValuePair<String, int>> TotalMonthColumnsProperty
        {
            get { return (ObservableCollection<KeyValuePair<String, int>>)GetValue(TotalMonthColumns); }
            set { SetValue(TotalMonthColumns, value); }
        }


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



        public static readonly DependencyProperty ChartListFats = DependencyProperty.Register("ChartListFatsProperty", typeof(ObservableCollection<KeyValuePair<DateTime, int>>), typeof(MonthChartUserControlVM));
        public ObservableCollection<KeyValuePair<DateTime, int>> ChartListFatsProperty
        {
            get { return (ObservableCollection<KeyValuePair<DateTime, int>>)GetValue(ChartListFats); }
            set { SetValue(ChartListFats, value); }
        }

        public static readonly DependencyProperty ChartListCarbs = DependencyProperty.Register("ChartListCarbsProperty", typeof(ObservableCollection<KeyValuePair<DateTime, int>>), typeof(MonthChartUserControlVM));
        public ObservableCollection<KeyValuePair<DateTime, int>> ChartListCarbsProperty
        {
            get { return (ObservableCollection<KeyValuePair<DateTime, int>>)GetValue(ChartListCarbs); }
            set { SetValue(ChartListCarbs, value); }
        }

        public static readonly DependencyProperty ChartListProteins = DependencyProperty.Register("ChartListProteinsProperty", typeof(ObservableCollection<KeyValuePair<DateTime, int>>), typeof(MonthChartUserControlVM));
        public ObservableCollection<KeyValuePair<DateTime, int>> ChartListProteinsProperty
        {
            get { return (ObservableCollection<KeyValuePair<DateTime, int>>)GetValue(ChartListProteins); }
            set { SetValue(ChartListProteins, value); }
        }


        public static readonly DependencyProperty sunday = DependencyProperty.Register("SundayProperty", typeof(DateTime), typeof(MonthChartUserControlVM),
                                                                                   new UIPropertyMetadata(new PropertyChangedCallback(OnSelectedSundayPropertyChanged)));
        public DateTime SundayProperty
        {
            get { return (DateTime)GetValue(sunday); }
            set { SetValue(sunday, value); }
        }

        public MonthChartUserControlModel Model { get; set; }
        public MonthChartUserControlVM(String emailAddress)
        {
            EmailAddressProperty = emailAddress;
            Model = new MonthChartUserControlModel();

        }

        private static void OnSelectedSundayPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MonthChartUserControlVM vm = (d as MonthChartUserControlVM);
            DateTime sunday = (DateTime)(e.NewValue);
            vm.OnSelectedSundayPropertyChanged(sunday);
        }

        private void OnSelectedSundayPropertyChanged(DateTime sunday)
        {

            WeekGoals wg = Model.getMonthGoals(EmailAddressProperty, sunday);
            loadWeekGoalsToProperties(wg);//load week goals of sunday's week of the user if it setted, else it clears the properties with 0

            dailyFoodLst = Model.getDailyFoodMonthList(EmailAddressProperty, sunday);

            IsCheckedAllChartsLinesProperty = false;
            IsCheckedAllChartsLinesProperty = true;
            sumTotalComponents();

        }

        private void sumTotalComponents()
        {
            ObservableCollection<String> tmp = new ObservableCollection<String>();
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

            TotalMonthComponentsListProperty = new ObservableCollection<string>() { ((int)totalCalories).ToString(), ((int)totalFats).ToString(), ((int)totalCarbs).ToString(), ((int)totalProteins).ToString() };
            TotalMonthColumnsProperty = new ObservableCollection<KeyValuePair<string, int>>() {new KeyValuePair<string, int>("Calories",(int)totalCalories), new KeyValuePair<string, int>("Fats",(int) totalFats),
                new KeyValuePair<string, int>("Carbs",(int)totalCarbs), new KeyValuePair<string, int>("Proteins", (int) totalProteins) };

        }

        private ObservableCollection<KeyValuePair<DateTime, int>> convertComponentsList(DateTime sunday, List<DailyFood> DailyFoodLst, string property)//property is 
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


        private void loadWeekGoalsToProperties(WeekGoals wg)//loads the WeekGoals- wg to the properties(to the screen) 
        {

            if (wg != null)
            {
                GoalsMonthComponentsListProperty = new ObservableCollection<string>() { wg.GoalCalories.ToString(), wg.GoalFats.ToString(), wg.GoalCarbs.ToString(), wg.GoalProteins.ToString() };
                GoalsMonthColumnsProperty = new ObservableCollection<KeyValuePair<string, int>>() {new KeyValuePair<string, int>("Calories", wg.GoalCalories), new KeyValuePair<string, int>("Fats", wg.GoalFats), new KeyValuePair<string, int>("Carbs", wg.GoalCarbs),
                    new KeyValuePair<string, int>("Proteins", wg.GoalCalories) };
            }
            else//clear
            {
                GoalsMonthComponentsListProperty = new ObservableCollection<string>() { "0", "0", "0", "0" };
                GoalsMonthColumnsProperty = new ObservableCollection<KeyValuePair<string, int>>() {new KeyValuePair<string, int>("Calories",0), new KeyValuePair<string, int>("Fats", 0), new KeyValuePair<string, int>("Carbs", 0),
                    new KeyValuePair<string, int>("Proteins", 0) };
            }
        }

    }
}

