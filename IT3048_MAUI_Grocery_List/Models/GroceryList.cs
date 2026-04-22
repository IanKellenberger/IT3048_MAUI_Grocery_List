using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

namespace IT3048_MAUI_Grocery_List.Models
{
    public class GroceryList
    {
        public string Name { get; set; }
        public ObservableCollection<GroceryItem> Items { get; set; }
    }
}
