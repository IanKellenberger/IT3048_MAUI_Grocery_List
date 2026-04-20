using SQLite;

namespace IT3048_MAUI_Grocery_List.Models
{
    public class HouseholdMember
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int HouseholdId { get; set; }

        public int UserId { get; set; }
    }
}
