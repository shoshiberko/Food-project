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

        public static readonly DependencyProperty EmailAddress = DependencyProperty.Register("EmailAddressProperty", typeof(String), typeof(UserControlChartVM), new PropertyMetadata("shosh@gmail.com"));
        public String EmailAddressProperty
        {
            get { return (String)GetValue(EmailAddress); }
            set { SetValue(EmailAddress, value); }
        }

        public static readonly DependencyProperty ChartListCalories = DependencyProperty.Register("ChartListCaloriesProperty", typeof(ObservableCollection<KeyValuePair<DateTime, int>>), typeof(UserControlChartVM));
        public ObservableCollection<KeyValuePair<DateTime, int>> ChartListCaloriesProperty
        {
            get { return (ObservableCollection<KeyValuePair<DateTime, int>>)GetValue(ChartListCalories); }
            set { SetValue(ChartListCalories, value); }
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


        //when the selected week is changed the button will be not enabled if the selected week has already passed, 
        //and if the data base has information about the selected week it will load this to the screen
        private void OnSelectedSundayPropertyChanged(DateTime sunday)
        {
            //WeekGoals wg = Model.getWeekGoals(EmailAddressProperty, sunday);
            //loadWeekGoalsToProperties(wg);//load week goals of sunday's week of the user if it setted, else it clears the properties with 0

            List<DailyFood>  dailyFoodLst = Model.getDailyFoodWeekList(EmailAddressProperty, sunday);
           ChartListCaloriesProperty= convertWeekComponentsList(sunday, dailyFoodLst, "TotalCalories");
           ChartListFatsProperty= convertWeekComponentsList(sunday, dailyFoodLst, "TotalFats");
             ChartListCarbsProperty= convertWeekComponentsList(sunday, dailyFoodLst, "TotalCarbs");
            ChartListProteinsProperty= convertWeekComponentsList(sunday, dailyFoodLst, "TotalPortiens");

            /*
            if ((DateTime.Today.AddDays(0 - (int)DateTime.Today.DayOfWeek)).CompareTo(sunday) > 0)//the selected week has already passed
                                                                                                  //therefore user can't set the goals
            {

            }
            else//the selected week has not passed yet
                Console.WriteLine("bbb");
                */
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


    }
}
