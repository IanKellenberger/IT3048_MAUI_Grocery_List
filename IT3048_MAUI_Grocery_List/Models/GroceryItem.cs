using SQLite;

namespace IT3048_MAUI_Grocery_List.Models
{
    public class GroceryItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public int Quantity { get; set; }

        [MaxLength(50)]
        public string Category { get; set; } = string.Empty;

        public bool IsChecked { get; set; }
    }
}