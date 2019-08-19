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
       SearchUserControlModel searchUserControlModel;
       // FoodSearchItemModel foodSearchItemModel;
        public static readonly DependencyProperty FoodItemsList = DependencyProperty.Register("FoodItemsListProperty", typeof(ObservableCollection<FoodSearchItemViewModel>), typeof(SearchUserControlViewModel));
        public ObservableCollection<FoodSearchItemViewModel> FoodItemsListProperty
        {
            get { return (ObservableCollection<FoodSearchItemViewModel>)GetValue(FoodItemsList); }
            set { SetValue(FoodItemsList, value); }
        }
        public String FoodNameToSearch { get; set; }
        public ICommand SearchListItemCommand { get; set; }

        public SearchUserControlViewModel()
        {
            searchUserControlModel = new SearchUserControlModel();
            SearchListItemCommand = new SearchListItemCommand();
        }
        public void updateList()
        {
            List<FoodItem> lst = searchUserControlModel.getListOfItems(FoodNameToSearch);
            ObservableCollection<FoodSearchItemViewModel> resultLst = new ObservableCollection<FoodSearchItemViewModel>();
            foreach(var item in lst)
            {
                resultLst.Add(new FoodSearchItemViewModel() { FoodNameProperty = item.Name,FoodNumber = item.Key});
            }
            FoodItemsListProperty = resultLst;
        }
    }
}
