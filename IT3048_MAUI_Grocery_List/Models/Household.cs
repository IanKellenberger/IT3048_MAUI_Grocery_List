using SQLite;

namespace IT3048_MAUI_Grocery_List.Models
{
    public class Household
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string Name { get; set; } = string.Empty;

        public int CreatedByUserId { get; set; }
    }
}
