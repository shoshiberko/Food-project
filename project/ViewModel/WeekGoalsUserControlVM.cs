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
using System.Windows.Threading;

namespace project.ViewModel
{
    public class WeekGoalsUserControlVM: DependencyObject
    {
#region properties
        public ICommand SetGoalsCommand{ get; set; }//this command done when the user click on set-button
        public WeekGoalsUserControlModel Model{ get; set; }//this s the connection to the model

        /// <summary>
        ///this property contains the DateTime of the sunday of the selected week
        /// </summary>  
        public static readonly DependencyProperty sunday = DependencyProperty.Register("SundayProperty", typeof(DateTime), typeof(WeekGoalsUserControlVM),
                                                                                    new UIPropertyMetadata(new PropertyChangedCallback(OnSelectedSundayPropertyChanged)));
        public DateTime SundayProperty
        {
            get { return (DateTime)GetValue(sunday); }
            set { SetValue(sunday, value); }
        }

        /// <summary>
        ///this property contains the Email addressof th user for identify him
        /// </summary>  
        public static readonly DependencyProperty EmailAddress = DependencyProperty.Register("EmailAddressProperty", typeof(String), typeof(WeekGoalsUserControlVM),new PropertyMetadata("shosh@gmail.com"));
        public String EmailAddressProperty
        {
            get { return (String)GetValue(EmailAddress); }
            set { SetValue(EmailAddress, value); }
        }

        /// <summary>
        ///this property contains true if the button of set goals is enabled.else, false.
        /// </summary>  
        public static readonly DependencyProperty IsEnabled = DependencyProperty.Register("IsEnabledProperty", typeof(Boolean), typeof(WeekGoalsUserControlVM),new PropertyMetadata(false));
        public Boolean IsEnabledProperty
        {
            get { return (Boolean)GetValue(IsEnabled); }
            set { SetValue(IsEnabled, value); }
        }

        /// <summary>
        ///this property contains string of the amount of the calories in the weekGoals
        /// </summary> 
        public static readonly DependencyProperty Calories = DependencyProperty.Register("CaloriesProperty", typeof(String), typeof(WeekGoalsUserControlVM),new PropertyMetadata("0"));
        public String CaloriesProperty
        {
            get { return (String)GetValue(Calories); }
            set { SetValue(Calories, value); }
        }

        /// <summary>
        ///this property contains string of the amount of the fats in the weekGoals
        /// </summary> 
        public static readonly DependencyProperty Fats = DependencyProperty.Register("FatsProperty", typeof(String), typeof(WeekGoalsUserControlVM), new PropertyMetadata("0"));
        public String FatsProperty
        {
            get { return (String)GetValue(Fats); }
            set { SetValue(Fats, value); }
        }

        /// <summary>
        ///this property contains string of the amount of the protiens in the weekGoals
        /// </summary> 
        public static readonly DependencyProperty Proteins = DependencyProperty.Register("ProteinsProperty", typeof(String), typeof(WeekGoalsUserControlVM), new PropertyMetadata("0"));
        public String ProteinsProperty
        {
            get { return (String)GetValue(Proteins); }
            set { SetValue(Proteins, value); }
        }

        /// <summary>
        ///this property contains string of the amount of the carbs in the weekGoals
        /// </summary> 
        public static readonly DependencyProperty Carbs = DependencyProperty.Register("CarbsProperty", typeof(String), typeof(WeekGoalsUserControlVM), new PropertyMetadata("0"));
        public String CarbsProperty
        {
            get { return (String)GetValue(Carbs); }
            set { SetValue(Carbs, value); }
        }

        /// <summary>
        ///this property contains true if the popUp (that showen when the set goals button clicked and the details saved) is open. else, false.
        /// </summary> 
        public static readonly DependencyProperty IsOpenSaveGoals = DependencyProperty.Register("IsOpenSaveGoalsProperty", typeof(Boolean), typeof(WeekGoalsUserControlVM));
        public Boolean IsOpenSaveGoalsProperty
        {
            get { return (Boolean)GetValue(IsOpenSaveGoals); }
            set { SetValue(IsOpenSaveGoals, value); }
        }
#endregion
        public WeekGoalsUserControlVM(String emailAddress)//constructor
        {
            EmailAddressProperty = emailAddress;
            SetGoalsCommand = new SetGoalsCommand();
            Model = new WeekGoalsUserControlModel();
            IsOpenSaveGoalsProperty = false;
        }
        /// <summary>
        ///this function set goals of week in db
        /// </summary> 
        public void setGoals()
        {
            Model.setGoals(new WeekGoals() { SundayDate = this.SundayProperty, EmailAddress = EmailAddressProperty, GoalCalories = int.Parse(CaloriesProperty), GoalCarbs = int.Parse(CarbsProperty), GoalFats = int.Parse(FatsProperty), GoalProteins = int.Parse(ProteinsProperty) });
            IsOpenSaveGoalsProperty = true;
            startTimerPopUpToolTip(2.2);//start the popUp showing time
        }

        /// <summary>
        ///this function open the popUp for interval seconds (will close the toolTipPopUp after interval seconds)
        /// </summary> 
        /// <param name="interval">the time in second that the set popUp open</param>
        private void startTimerPopUpToolTip(double interval)
        {
            DispatcherTimer time = new DispatcherTimer();
            time.Interval = TimeSpan.FromSeconds(interval);
            time.Start();
            time.Tick += delegate
            {
                time.Interval = TimeSpan.FromSeconds(interval);
                IsOpenSaveGoalsProperty = false;
                time.Stop();
            };

        }

        /// <summary>
        ///this function called when the user change his week choose
        /// </summary> 
        /// <param name="d">the event caller</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        private static void OnSelectedSundayPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WeekGoalsUserControlVM vm = (d as WeekGoalsUserControlVM);
            DateTime sunday = (DateTime)(e.NewValue);
            vm.OnSelectedSundayPropertyChanged(sunday);
        }

        /// <summary>
        ///this function called when the selected week is changed the button will be not enabled if the selected week has already passed, 
        ///and if the data base has information about the selected week it will load this to the screen
        /// </summary> 
        /// <param name="sunday">the sunday DateTime of the start of the week</param>
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

        /// <summary>
        ///this function loads the WeekGoals- wg to the properties(to the screen)
        /// </summary> 
        /// <param name="wg">the weekGoals that it's details will showen on screen</param>
        private void loadWeekGoalsToProperties(WeekGoals wg) 
        {
            if (wg != null)//put details on screen
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
