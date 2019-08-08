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

        public DateTime sunday { get; set; }
        public WeekPicker()
        {
            InitializeComponent();
            flagOnUpdate = false;

        }
        private void addSelectedDates()
        {
            flagOnUpdate = true;
            DateTime date = (DateTime)calendar.SelectedDate;
            calendar.SelectedDates.Clear();
            sunday = date.AddDays(0 - (int)date.DayOfWeek);//the sunday of the selected week
            for (int i = (int)sunday.DayOfWeek; i < 7; i++)
                calendar.SelectedDates.Add(sunday.AddDays(i));
           
            flagOnUpdate = false;
        }
        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate != null && flagOnUpdate==false)
                addSelectedDates();
        }
    }
}
