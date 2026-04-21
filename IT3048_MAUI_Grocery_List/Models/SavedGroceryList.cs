using SQLite;

namespace IT3048_MAUI_Grocery_List.Models
{
    public class SavedGroceryList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ListName { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
