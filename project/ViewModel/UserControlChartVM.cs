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
    public class UserControlChartVM : DependencyObject
    {
        // public List<KeyValuePair<DateTime, int>> list1 { get; set; }
        List<DailyFood> dailyFoodLst;
        public static readonly DependencyProperty EmailAddress = DependencyProperty.Register("EmailAddressProperty", typeof(String), typeof(UserControlChartVM), new PropertyMetadata("shosh@gmail.com"));
        public String EmailAddressProperty
        {
            get { return (String)GetValue(EmailAddress); }
            set { SetValue(EmailAddress, value); }
        }

        public static readonly DependencyProperty IsCheckedAllChartsLines = DependencyProperty.Register("IsCheckedAllChartsLinesProperty", typeof(Boolean), typeof(UserControlChartVM));
        public Boolean IsCheckedAllChartsLinesProperty
        {
            get { return (Boolean)GetValue(IsCheckedAllChartsLines); }
            set { SetValue(IsCheckedAllChartsLines, value); }
        }

        public static readonly DependencyProperty ChartListCalories = DependencyProperty.Register("ChartListCaloriesProperty", typeof(ObservableCollection<KeyValuePair<DateTime, int>>), typeof(UserControlChartVM));
        public ObservableCollection<KeyValuePair<DateTime, int>> ChartListCaloriesProperty
        {
            get { return (ObservableCollection<KeyValuePair<DateTime, int>>)GetValue(ChartListCalories); }
            set { SetValue(ChartListCalories, value); }
        }


        public static readonly DependencyProperty GoalsWeekComponentsList = DependencyProperty.Register("GoalsWeekComponentsListProperty", typeof(ObservableCollection<String>), typeof(UserControlChartVM));
        public ObservableCollection<String> GoalsWeekComponentsListProperty
        {
            get { return (ObservableCollection<String>)GetValue(GoalsWeekComponentsList); }
            set { SetValue(GoalsWeekComponentsList, value); }
        }


        public static readonly DependencyProperty TotalWeekComponentsList = DependencyProperty.Register("TotalWeekComponentsListProperty", typeof(ObservableCollection<String>), typeof(UserControlChartVM));
        public ObservableCollection<String> TotalWeekComponentsListProperty
        {
            get { return (ObservableCollection<String>)GetValue(TotalWeekComponentsList); }
            set { SetValue(TotalWeekComponentsList, value); }
        }

        public static readonly DependencyProperty GoalsWeekColumns = DependencyProperty.Register("GoalsWeekColumnsProperty", typeof(ObservableCollection<KeyValuePair<String, int>>), typeof(UserControlChartVM));
        public ObservableCollection<KeyValuePair<String, int>> GoalsWeekColumnsProperty
        {
            get { return (ObservableCollection<KeyValuePair<String, int>>)GetValue(GoalsWeekColumns); }
            set { SetValue(GoalsWeekColumns, value); }
        }

        public static readonly DependencyProperty TotalWeekColumns = DependencyProperty.Register("TotalWeekColumnsProperty", typeof(ObservableCollection<KeyValuePair<String, int>>), typeof(UserControlChartVM));
        public ObservableCollection<KeyValuePair<String, int>> TotalWeekColumnsProperty
        {
            get { return (ObservableCollection<KeyValuePair<String, int>>)GetValue(TotalWeekColumns); }
            set { SetValue(TotalWeekColumns, value); }
        }


        public void addChartLine(String name)
        {
            switch (name)
            {
                case "Calorieschart":
                ChartListCaloriesProperty = convertWeekComponentsList(SundayProperty, dailyFoodLst, "TotalCalories");
                    break;
                case "FatsChart":
                    ChartListFatsProperty = convertWeekComponentsList(SundayProperty, dailyFoodLst, "TotalFats");
                    break;
                case "ProteinsChart":
                    ChartListProteinsProperty = convertWeekComponentsList(SundayProperty, dailyFoodLst, "TotalPortiens");
                    break;
                case "CarbChart":
                    ChartListCarbsProperty = convertWeekComponentsList(SundayProperty, dailyFoodLst, "TotalCarbs");
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

      

        public static readonly DependencyProperty ChartListFats = DependencyProperty.Register("ChartListFatsProperty", typeof(ObservableCollection<KeyValuePair<DateTime, int>>), typeof(UserControlChartVM));
        public ObservableCollection<KeyValuePair<DateTime, int>> ChartListFatsProperty
        {
            get { return (ObservableCollection<KeyValuePair<DateTime, int>>)GetValue(ChartListFats); }
            set { SetValue(ChartListFats, value); }
        }

        public static readonly DependencyProperty ChartListCarbs = DependencyProperty.Register("ChartListCarbsProperty", typeof(ObservableCollection<KeyValuePair<DateTime, int>>), typeof(UserControlChartVM));
        public ObservableCollection<KeyValuePair<DateTime, int>> ChartListCarbsProperty
        {
            get { return (ObservableCollection<KeyValuePair<DateTime, int>>)GetValue(ChartListCarbs); }
            set { SetValue(ChartListCarbs, value); }
        }

        public static readonly DependencyProperty ChartListProteins = DependencyProperty.Register("ChartListProteinsProperty", typeof(ObservableCollection<KeyValuePair<DateTime, int>>), typeof(UserControlChartVM));
        public ObservableCollection<KeyValuePair<DateTime, int>> ChartListProteinsProperty
        {
            get { return (ObservableCollection<KeyValuePair<DateTime, int>>)GetValue(ChartListProteins); }
            set { SetValue(ChartListProteins, value); }
        }


        public static readonly DependencyProperty sunday = DependencyProperty.Register("SundayProperty", typeof(DateTime), typeof(UserControlChartVM),
                                                                                   new UIPropertyMetadata(new PropertyChangedCallback(OnSelectedSundayPropertyChanged)));
        public DateTime SundayProperty
        {
            get { return (DateTime)GetValue(sunday); }
            set { SetValue(sunday, value); }
        }

        public UserControlChartModel Model { get; set; }
        public UserControlChartVM()
        {
            Model = new UserControlChartModel();

        }

        private static void OnSelectedSundayPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserControlChartVM vm = (d as UserControlChartVM);
            DateTime sunday = (DateTime)(e.NewValue);
            vm.OnSelectedSundayPropertyChanged(sunday);
        }

        private void OnSelectedSundayPropertyChanged(DateTime sunday)
        {
           
            WeekGoals wg = Model.getWeekGoals(EmailAddressProperty, sunday);
            loadWeekGoalsToProperties(wg);//load week goals of sunday's week of the user if it setted, else it clears the properties with 0

            dailyFoodLst = Model.getDailyFoodWeekList(EmailAddressProperty, sunday);
            
            IsCheckedAllChartsLinesProperty = false;
            IsCheckedAllChartsLinesProperty = true;
            sumTotalWeekComponents();
           
        }

        private void sumTotalWeekComponents()
        {
            ObservableCollection<String> tmp=new ObservableCollection<String>();
            float totalFats=0, totalCarbs=0, totalProteins=0, totalCalories = 0;
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

            TotalWeekComponentsListProperty = new ObservableCollection<string>() { ((int)totalCalories).ToString(), ((int)totalFats).ToString(), ((int)totalCarbs).ToString(), ((int)totalProteins).ToString() };
            TotalWeekColumnsProperty= new ObservableCollection<KeyValuePair<string, int>>() {new KeyValuePair<string, int>("Calories",(int)totalCalories), new KeyValuePair<string, int>("Fats",(int) totalFats),
                new KeyValuePair<string, int>("Carbs",(int)totalCarbs), new KeyValuePair<string, int>("Proteins", (int) totalProteins) };

        }

        private ObservableCollection<KeyValuePair<DateTime, int>> convertWeekComponentsList(DateTime sunday, List<DailyFood> DailyFoodLst, string property)//property is 
        {
            ObservableCollection<KeyValuePair<DateTime, int>> tmp = new ObservableCollection<KeyValuePair<DateTime, int>>();
            for (int i = 0; i < 7; i++)
            {
                if (DailyFoodLst[i] != null)
                    tmp.Add(new KeyValuePair<DateTime, int>(DailyFoodLst[i].CurrentDate,(int)( (float)DailyFoodLst[i][property])));
                else
                    tmp.Add(new KeyValuePair<DateTime, int>(sunday.AddDays(i), 0));
            }
            return tmp;
        }

        
        private void loadWeekGoalsToProperties(WeekGoals wg)//loads the WeekGoals- wg to the properties(to the screen) 
        {

            if (wg != null)
            {
                GoalsWeekComponentsListProperty = new ObservableCollection<string>() { wg.GoalCalories.ToString(), wg.GoalFats.ToString(), wg.GoalCarbs.ToString(), wg.GoalProteins.ToString() };
                GoalsWeekColumnsProperty = new ObservableCollection<KeyValuePair<string, int>>() {new KeyValuePair<string, int>("Calories", wg.GoalCalories), new KeyValuePair<string, int>("Fats", wg.GoalFats), new KeyValuePair<string, int>("Carbs", wg.GoalCarbs),
                    new KeyValuePair<string, int>("Proteins", wg.GoalCalories) };
            }
            else//clear
            {
                GoalsWeekComponentsListProperty = new ObservableCollection<string>() { "0", "0", "0", "0" };
                GoalsWeekColumnsProperty = new ObservableCollection<KeyValuePair<string, int>>() {new KeyValuePair<string, int>("Calories",0), new KeyValuePair<string, int>("Fats", 0), new KeyValuePair<string, int>("Carbs", 0),
                    new KeyValuePair<string, int>("Proteins", 0) };
            }
        }

    }
}
