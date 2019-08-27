using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace project.View
{
    /// <summary>
    /// Interaction logic for WeekPicker.xaml
    /// </summary>
    public partial class WeekPicker : UserControl
    {
        private bool flagOnUpdate;
        public static readonly DependencyProperty sunday = DependencyProperty.Register("SundayProperty", typeof(DateTime), typeof(WeekPicker),new PropertyMetadata( DateTime.Today.AddDays(0-(int)DateTime.Today.DayOfWeek)));//default is sunday of this week
        public DateTime SundayProperty
        {
            get { return (DateTime)GetValue(sunday); }
            set { SetValue(sunday, value); }
        }

        public WeekPicker()
        {
            InitializeComponent();
            flagOnUpdate = false;
            calendar.DisplayDateStart = (DateTime.Now.AddDays(0 - (int)DateTime.Now.DayOfWeek)).AddDays(-21);//3 weeks before sunday of this week
            calendar.DisplayDateEnd = (DateTime.Now.AddDays(0 - (int)DateTime.Now.DayOfWeek)).AddDays(27);//4 weeks from sunday of this week
        }
        private void addSelectedDates()//marks the whole week of the selected date
        {
            flagOnUpdate = true;
            DateTime date = (DateTime)calendar.SelectedDate;
            calendar.SelectedDates.Clear();
            SundayProperty = date.AddDays(0 - (int)date.DayOfWeek);//the sunday of the selected week
            for (int i = (int)SundayProperty.DayOfWeek; i < 7; i++)
                calendar.SelectedDates.Add(SundayProperty.AddDays(i));
           
            flagOnUpdate = false;
        }
        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate != null && flagOnUpdate==false)//if its the first time
                addSelectedDates();
        }
    }
}
