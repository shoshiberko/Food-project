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
        
        List<DailyFood> dailyFoodLst;
        #region properties
        /// <summary>
        ///user's email address
        /// </summary>
        public static readonly DependencyProperty EmailAddress = DependencyProperty.Register("EmailAddressProperty", typeof(String), typeof(UserControlChartVM), new PropertyMetadata("shosh@gmail.com"));
        public String EmailAddressProperty
        {
            get { return (String)GetValue(EmailAddress); }
            set { SetValue(EmailAddress, value); }
        }

        /// <summary>
        ///false and after true when SundayProperty is changed (binding to toggle buttons )
        /// </summary>
        public static readonly DependencyProperty IsCheckedAllChartsLines = DependencyProperty.Register("IsCheckedAllChartsLinesProperty", typeof(Boolean), typeof(UserControlChartVM));
        public Boolean IsCheckedAllChartsLinesProperty
        {
            get { return (Boolean)GetValue(IsCheckedAllChartsLines); }
            set { SetValue(IsCheckedAllChartsLines, value); }
        }

        

        /// <summary>
        /// information list of week goals components.
        /// </summary>
        public static readonly DependencyProperty GoalsWeekComponentsList = DependencyProperty.Register("GoalsWeekComponentsListProperty", typeof(ObservableCollection<String>), typeof(UserControlChartVM));
        public ObservableCollection<String> GoalsWeekComponentsListProperty
        {
            get { return (ObservableCollection<String>)GetValue(GoalsWeekComponentsList); }
            set { SetValue(GoalsWeekComponentsList, value); }
        }

        /// <summary>
        /// information list of total week components.
        /// </summary>
        public static readonly DependencyProperty TotalWeekComponentsList = DependencyProperty.Register("TotalWeekComponentsListProperty", typeof(ObservableCollection<String>), typeof(UserControlChartVM));
        public ObservableCollection<String> TotalWeekComponentsListProperty
        {
            get { return (ObservableCollection<String>)GetValue(TotalWeekComponentsList); }
            set { SetValue(TotalWeekComponentsList, value); }
        }

        /// <summary>
        /// information list of week goals components - for columns chart.
        /// </summary>
        public static readonly DependencyProperty GoalsWeekColumns = DependencyProperty.Register("GoalsWeekColumnsProperty", typeof(ObservableCollection<KeyValuePair<String, int>>), typeof(UserControlChartVM));
        public ObservableCollection<KeyValuePair<String, int>> GoalsWeekColumnsProperty
        {
            get { return (ObservableCollection<KeyValuePair<String, int>>)GetValue(GoalsWeekColumns); }
            set { SetValue(GoalsWeekColumns, value); }
        }

        /// <summary>
        /// information list of total week components - for columns chart.
        /// </summary>
        public static readonly DependencyProperty TotalWeekColumns = DependencyProperty.Register("TotalWeekColumnsProperty", typeof(ObservableCollection<KeyValuePair<String, int>>), typeof(UserControlChartVM));
        public ObservableCollection<KeyValuePair<String, int>> TotalWeekColumnsProperty
        {
            get { return (ObservableCollection<KeyValuePair<String, int>>)GetValue(TotalWeekColumns); }
            set { SetValue(TotalWeekColumns, value); }
        }


        /// <summary>
        /// information list of total calories in the week - for calories line chart.
        /// </summary>
        public static readonly DependencyProperty ChartListCalories = DependencyProperty.Register("ChartListCaloriesProperty", typeof(ObservableCollection<KeyValuePair<DateTime, int>>), typeof(UserControlChartVM));
        public ObservableCollection<KeyValuePair<DateTime, int>> ChartListCaloriesProperty
        {
            get { return (ObservableCollection<KeyValuePair<DateTime, int>>)GetValue(ChartListCalories); }
            set { SetValue(ChartListCalories, value); }
        }
        /// <summary>
        /// information list of total fats in the week - for calories line chart.
        /// </summary>
        public static readonly DependencyProperty ChartListFats = DependencyProperty.Register("ChartListFatsProperty", typeof(ObservableCollection<KeyValuePair<DateTime, int>>), typeof(UserControlChartVM));
        public ObservableCollection<KeyValuePair<DateTime, int>> ChartListFatsProperty
        {
            get { return (ObservableCollection<KeyValuePair<DateTime, int>>)GetValue(ChartListFats); }
            set { SetValue(ChartListFats, value); }
        }

        /// <summary>
        /// information list of total carbs in the week - for calories line chart.
        /// </summary>
        public static readonly DependencyProperty ChartListCarbs = DependencyProperty.Register("ChartListCarbsProperty", typeof(ObservableCollection<KeyValuePair<DateTime, int>>), typeof(UserControlChartVM));
        public ObservableCollection<KeyValuePair<DateTime, int>> ChartListCarbsProperty
        {
            get { return (ObservableCollection<KeyValuePair<DateTime, int>>)GetValue(ChartListCarbs); }
            set { SetValue(ChartListCarbs, value); }
        }

        /// <summary>
        /// information list of total proteins in the week - for calories line chart.
        /// </summary>
        public static readonly DependencyProperty ChartListProteins = DependencyProperty.Register("ChartListProteinsProperty", typeof(ObservableCollection<KeyValuePair<DateTime, int>>), typeof(UserControlChartVM));
        public ObservableCollection<KeyValuePair<DateTime, int>> ChartListProteinsProperty
        {
            get { return (ObservableCollection<KeyValuePair<DateTime, int>>)GetValue(ChartListProteins); }
            set { SetValue(ChartListProteins, value); }
        }

        /// <summary>
        /// sunday of the selected week
        /// </summary>
        public static readonly DependencyProperty sunday = DependencyProperty.Register("SundayProperty", typeof(DateTime), typeof(UserControlChartVM),
                                                                                   new UIPropertyMetadata(new PropertyChangedCallback(OnSelectedSundayPropertyChanged)));
        public DateTime SundayProperty
        {
            get { return (DateTime)GetValue(sunday); }
            set { SetValue(sunday, value); }
        }

        public UserControlChartModel Model { get; set; }
        #endregion
        public UserControlChartVM(String emailAddress)//ctor
        {
            Model = new UserControlChartModel();
            EmailAddressProperty = emailAddress;

        }


        /// <summary>
        /// put in the suitable property of ChartList component- list of KeyValuePair with- date of every day in the sunday's week, with it's  DailyFoodLst's suitable component
        /// </summary>
        /// <param name="name">fit to property of ChartList by name- options: Calorieschart, FatsChart, ProteinsChart, CarbChart</param>
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

        /// <summary>
        /// put in the suitable property of ChartList component- list of KeyValuePair with- date of every day in the sunday's week, with zero 
        /// </summary>
        /// <param name="name">it to property of ChartList by name- options: Calorieschart, FatsChart, ProteinsChart, CarbChart</param>
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
        /// this function is called when SundayProperty is changed and it restarts propertis with details of sunday's week
        /// </summary>
        /// <param name="d"> UserControlChartVM</param>
        /// <param name="e"> contains the date of the new value</param>
        private static void OnSelectedSundayPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserControlChartVM vm = (d as UserControlChartVM);
            DateTime sunday = (DateTime)(e.NewValue);
            vm.OnSelectedSundayPropertyChanged(sunday);
        }

        /// <summary>
        ///restarts propertis with details of sunday's week-
        ///loads week goals of sunday's week, restart dailyFoodLst with dailyFood of sunday's week, 
        /// </summary>
        /// <param name="sunday"> the </param>
        private void OnSelectedSundayPropertyChanged(DateTime sunday)
        {
           
            WeekGoals wg = Model.getWeekGoals(EmailAddressProperty, sunday);
            loadWeekGoalsToProperties(wg);//loads week goals of sunday's week of the user if it setted, else it clears the properties with 0

            dailyFoodLst = Model.getDailyFoodWeekList(EmailAddressProperty, sunday);//restart dailyFoodLst with dailyFood of sunday's week
            
            IsCheckedAllChartsLinesProperty = false;
            IsCheckedAllChartsLinesProperty = true;//if toggle buttons binding to this property they will be pressed all
            sumTotalWeekComponents();// restart summary details of sunday's week

        }

        /// <summary>
        /// sum week components of dailyFoodLst field, and create list with this data- [totalCalories, totalFats, totalProteins, totalCarbs] 
        /// and put it in TotalWeekComponentsListProperty, and in a similar way- creates similar list in TotalWeekColumnsProperty.
        /// </summary>
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

            TotalWeekComponentsListProperty = new ObservableCollection<string>() { ((int)totalCalories).ToString(), ((int)totalFats).ToString(), ((int)totalProteins).ToString(), ((int)totalCarbs).ToString() };
            TotalWeekColumnsProperty= new ObservableCollection<KeyValuePair<string, int>>() {new KeyValuePair<string, int>("Calories",(int)totalCalories), new KeyValuePair<string, int>("Fats",(int) totalFats),
                new KeyValuePair<string, int>("Proteins", (int) totalProteins), new KeyValuePair<string, int>("Carbohydrates",(int)totalCarbs) };

        }

        /// <summary>
        /// converts DailyFoodLst of one week(list in size 7) to ObservableCollection<KeyValuePair<DateTime, int>> where the int is DailyFood's property
        /// </summary>
        /// <param name="sunday">sunday date of the week of the DailyFoodLst</param>
        /// <param name="DailyFoodLst">list to convertion</param>
        /// <param name="property">the DailyFood's property name</param>
        /// <returns>list of KeyValuePair -date of every day in the sunday's week, with it's  DailyFoodLst.property</returns>
        private ObservableCollection<KeyValuePair<DateTime, int>> convertWeekComponentsList(DateTime sunday, List<DailyFood> DailyFoodLst, string property)
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

        /// <summary>
        /// loads the WeekGoals- wg to the properties(to the screen)- GoalsWeekComponentsListProperty, GoalsWeekColumnsProperty
        /// </summary>
        /// <param name="wg"></param>
        private void loadWeekGoalsToProperties(WeekGoals wg)
        {

            if (wg != null)
            {
                GoalsWeekComponentsListProperty = new ObservableCollection<string>() { wg.GoalCalories.ToString(), wg.GoalFats.ToString(), wg.GoalProteins.ToString(), wg.GoalCarbs.ToString() };
                GoalsWeekColumnsProperty = new ObservableCollection<KeyValuePair<string, int>>() {new KeyValuePair<string, int>("Calories", wg.GoalCalories), new KeyValuePair<string, int>("Fats", wg.GoalFats), new KeyValuePair<string, int>("Proteins", wg.GoalProteins),
                    new KeyValuePair<string, int>("Carbohydrates", wg.GoalCarbs)};
            }
            else
            {
                GoalsWeekComponentsListProperty = new ObservableCollection<string>() { "0", "0", "0", "0" };
                GoalsWeekColumnsProperty = new ObservableCollection<KeyValuePair<string, int>>() {new KeyValuePair<string, int>("Calories",0), new KeyValuePair<string, int>("Fats", 0),new KeyValuePair<string, int>("Proteins", 0) ,
                new KeyValuePair<string, int>("Carbohydrates", 0)};
            }
        }

    }
}
