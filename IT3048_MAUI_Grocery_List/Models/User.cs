using SQLite;

namespace IT3048_MAUI_Grocery_List.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Unique]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;
    }
}