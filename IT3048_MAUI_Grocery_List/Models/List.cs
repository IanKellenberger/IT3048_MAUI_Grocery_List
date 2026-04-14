using System.Collections.ObjectModel;

namespace IT3048_MAUI_Grocery_List.Models
{
    public class List
    {
        public string Name { get; set; } = "My Grocery List";
        public ObservableCollection<GroceryItem> Items { get; set; } = new();
    }
}
