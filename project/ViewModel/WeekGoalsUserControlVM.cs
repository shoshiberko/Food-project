using project.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BE;
using project.Model;

namespace project.ViewModel
{
    public class WeekGoalsUserControlVM: DependencyObject
    {
        public ICommand SetGoalsCommand{ get; set; }
        public WeekGoalsUserControlModel Model{ get; set; }

        
    public static readonly DependencyProperty sunday = DependencyProperty.Register("SundayProperty", typeof(DateTime), typeof(WeekGoalsUserControlVM),
                                                                                    new UIPropertyMetadata(new PropertyChangedCallback(OnSelectedSundayPropertyChanged)));
        
        public DateTime SundayProperty
        {
            get { return (DateTime)GetValue(sunday); }
            set { SetValue(sunday, value); }
        }

        public static readonly DependencyProperty EmailAddress = DependencyProperty.Register("EmailAddressProperty", typeof(String), typeof(WeekGoalsUserControlVM),new PropertyMetadata("shosh@gmail.com"));
        public String EmailAddressProperty
        {
            get { return (String)GetValue(EmailAddress); }
            set { SetValue(EmailAddress, value); }
        }

        //true-button set goals is enabled
        public static readonly DependencyProperty IsEnabled = DependencyProperty.Register("IsEnabledProperty", typeof(Boolean), typeof(WeekGoalsUserControlVM),new PropertyMetadata(false));
        public Boolean IsEnabledProperty
        {
            get { return (Boolean)GetValue(IsEnabled); }
            set { SetValue(IsEnabled, value); }
        }

        public static readonly DependencyProperty Calories = DependencyProperty.Register("CaloriesProperty", typeof(String), typeof(WeekGoalsUserControlVM),new PropertyMetadata("0"));
        public String CaloriesProperty
        {
            get { return (String)GetValue(Calories); }
            set { SetValue(Calories, value); }
        }
        public static readonly DependencyProperty Fats = DependencyProperty.Register("FatsProperty", typeof(String), typeof(WeekGoalsUserControlVM), new PropertyMetadata("0"));
        public String FatsProperty
        {
            get { return (String)GetValue(Fats); }
            set { SetValue(Fats, value); }
        }
        public static readonly DependencyProperty Proteins = DependencyProperty.Register("ProteinsProperty", typeof(String), typeof(WeekGoalsUserControlVM), new PropertyMetadata("0"));
        public String ProteinsProperty
        {
            get { return (String)GetValue(Proteins); }
            set { SetValue(Proteins, value); }
        }
        public static readonly DependencyProperty Carbs = DependencyProperty.Register("CarbsProperty", typeof(String), typeof(WeekGoalsUserControlVM), new PropertyMetadata("0"));
        public String CarbsProperty
        {
            get { return (String)GetValue(Carbs); }
            set { SetValue(Carbs, value); }
        }

        public WeekGoalsUserControlVM()
        {
            SetGoalsCommand = new SetGoalsCommand();
            Model = new WeekGoalsUserControlModel();
        }
        public void setGoals()//set goals of week in db
        {
            Model.setGoals(new WeekGoals() { SundayDate = this.SundayProperty, EmailAddress = EmailAddressProperty, GoalCalories = int.Parse(CaloriesProperty), GoalCarbs = int.Parse(CarbsProperty), GoalFats = int.Parse(FatsProperty), GoalProteins = int.Parse(ProteinsProperty) });
        }
        
        
        private static void OnSelectedSundayPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WeekGoalsUserControlVM vm = (d as WeekGoalsUserControlVM);
            DateTime sunday = (DateTime)(e.NewValue);
            vm.OnSelectedSundayPropertyChanged(sunday);
        }


        //when the selected week is changed the button will be not enabled if the selected week has already passed, 
        //and if the data base has information about the selected week it will load this to the screen
        private void OnSelectedSundayPropertyChanged(DateTime sunday)
        {
            WeekGoals wg = Model.getWeekGoals(EmailAddressProperty, sunday);
            loadWeekGoalsToProperties(wg);//load week goals of sunday's week of the user if it setted, else it clears the properties with 0
            
            if ((DateTime.Today.AddDays(0 - (int)DateTime.Today.DayOfWeek)).CompareTo(sunday)>0)//the selected week has already passed
                //therefore user can't set the goals
            {
                IsEnabledProperty = false;
            }
            else//the selected week has not passed yet
                IsEnabledProperty = true;
        }

        private void loadWeekGoalsToProperties(WeekGoals wg)//loads the WeekGoals- wg to the properties(to the screen) 
        {
            if (wg != null)
            {
                this.CaloriesProperty = wg.GoalCalories.ToString();
                this.CarbsProperty = wg.GoalCarbs.ToString();
                this.FatsProperty = wg.GoalFats.ToString();
                this.ProteinsProperty = wg.GoalProteins.ToString();
            }
            else//clear
            {
                this.CaloriesProperty = "0";
                this.CarbsProperty = "0";
                this.FatsProperty = "0";
                this.ProteinsProperty = "0";
            }
        }
    }
}
