﻿using System;
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
using BE;
using BL;

namespace project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();

            /*using (var db = new FoodContext())
            {
                db.Users.Add(new User() { EmailAddress = "shosh@gmail.com", Password ="12344321", BirthDate=DateTime.Now, LastUpdateCurrentWeight=DateTime.Now});
                db.SaveChanges();
            }*/
         

        }
        
    }
}
