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
using project.ViewModel;
using project.Model;

namespace project.View
{
    /// <summary>
    /// Interaction logic for FoodSearchItemUserControl.xaml
    /// </summary>
    public partial class FoodSearchItemUserControl : UserControl
    {

        //FoodSearchItemViewModel foodSearchItemViewModel; 
        public FoodSearchItemUserControl()
        {
            InitializeComponent();
        }

        private void Popup_Opened(object sender, EventArgs e)
        {
            foodDetailsUserControl.FoodDependencyProperty = TextFoodDetails.Text;
        }
    }
}
