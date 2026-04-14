using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace IT3048_MAUI_Grocery_List.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string Username { get; set; }

        [Unique]
        public string Email {  get; set; }

        public string PasswordHash {  get; set; }

    }
}
