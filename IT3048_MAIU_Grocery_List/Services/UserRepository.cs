using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using IT3048_MAIU_Grocery_List;
using IT3048_MAIU_Grocery_List.Models;

namespace IT3048_MAIU_Grocery_List.Services
{
    public class UserRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public UserRepository(SQLiteAsyncConnection db)
        {
            _db = db;
        }

        public Task<int> CreateUserAsync(User user)
        {
            return _db.InsertAsync(user);
        }

        public Task<User> GetByEmailAsync(string email)
        {
            return _db.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public Task<User> GetByUsernameAsync(string username)
        {
            return _db.Table<User>().Where(u => u.Username == username).FirstOrDefaultAsync();
        }
    }
}
