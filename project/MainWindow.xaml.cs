using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BE;
using BL;
using project.View;
using project.ViewModel;

namespace project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mainWindowViewModel;
        ListView listView = new ListView();
        public MainWindow()
        {
            InitializeComponent();
            mainWindowViewModel = new MainWindowViewModel();
            this.DataContext = mainWindowViewModel;
           /* ImpBL myBl = new ImpBL();
            var user = myBl.getUser("efratan1@gmail.com");
            var newUser = new User() { Age = user.Age, BirthDate = user.BirthDate, CurrentWeight = float.Parse("55.5"), EmailAddress = "efratan1@gmail.com", FamilyStatus = user.FamilyStatus, Gender = user.Gender, GoalWeight = user.GoalWeight, Height = user.Height, LastUpdateCurrentWeight =DateTime.Now.AddDays(-8), Password = user.Password };
            myBl.updateUser(newUser);*/
            /*ObservableCollection<String> list1 = new ObservableCollection<string>();
            list1.Add("Home");
            list1.Add("Profile");
            list1.Add("Inbox");
            list1.Add("Outbox");
            list1.Add("Sent");
            list1.Add("Draft");
            listView.ItemsSource = list1;
            list = listView;*/
            /*  using (var db = new FoodContext())
              {

                  db.Users.Add(new User() { EmailAddress = "shosh@gmail.com", Password ="12344321", BirthDate=DateTime.Now, LastUpdateCurrentWeight=DateTime.Now});
                  db.SaveChanges();

          }*/
            //  contentControl.Content = new AddDailyFoodUserControl();
        }



        /* void Handle_Clicked(object sender, System.EventArgs e)
         {
             navigationDrawer.ToggleDrawer();
         }*/
        
    }
}
