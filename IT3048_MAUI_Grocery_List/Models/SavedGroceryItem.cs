using SQLite;

namespace IT3048_MAUI_Grocery_List.Models
{
    public class SavedGroceryItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int GroceryListId { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public bool IsChecked { get; set; }
    }
}
