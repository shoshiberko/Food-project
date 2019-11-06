using project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Windows.Input;
using project.Commands;
using System.Windows;

namespace project.ViewModel
{
   public class SearchUserControlViewModel:DependencyObject
    {
#region properties
        SearchUserControlModel searchUserControlModel;//the connection to the model
        /// <summary>
        /// this property contain a list of foods that contains the string that the user enter for searching
        /// </summary>
        public static readonly DependencyProperty FoodItemsList = DependencyProperty.Register("FoodItemsListProperty", typeof(ObservableCollection<FoodSearchItemViewModel>), typeof(SearchUserControlViewModel));
        public ObservableCollection<FoodSearchItemViewModel> FoodItemsListProperty
        {
            get { return (ObservableCollection<FoodSearchItemViewModel>)GetValue(FoodItemsList); }
            set { SetValue(FoodItemsList, value); }
        }
        public String FoodNameToSearch { get; set; }//the string that the user search food that contain it.
        public ICommand SearchListItemCommand { get; set; }//this command done when the user click on search-Button
#endregion
        public SearchUserControlViewModel()//constructor
        {
            searchUserControlModel = new SearchUserControlModel();
            SearchListItemCommand = new SearchListItemCommand();
        }
        /// <summary>
        /// this task update the search list
        /// </summary>
        public async Task updateList()
        {
            updateListTask();
        }
        /// <summary>
        /// this function update the search list
        /// </summary>
        public void updateListTask()
        {
            List<FoodItem> lst = searchUserControlModel.getListOfItems(FoodNameToSearch);
            ObservableCollection<FoodSearchItemViewModel> resultLst = new ObservableCollection<FoodSearchItemViewModel>();
            foreach(var item in lst)
            {
                resultLst.Add(new FoodSearchItemViewModel() { FoodNameProperty = item.Name,FoodNumber = item.Key});
            }
            FoodItemsListProperty = resultLst;//show list on screen
        }
    }
}
