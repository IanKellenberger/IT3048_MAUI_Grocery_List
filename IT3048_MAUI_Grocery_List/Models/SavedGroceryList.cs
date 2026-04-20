using SQLite;

namespace IT3048_MAUI_Grocery_List.Models
{
    public class SavedGroceryList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }

        public string ListName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
