//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using SQLite;
//using IT3048_MAUI_Grocery_List;
//using IT3048_MAUI_Grocery_List.Models;

//namespace IT3048_MAUI_Grocery_List.Services
//{
//    public class UserRepository
//    {
//        private readonly SQLiteAsyncConnection _db;

//        public UserRepository(SQLiteAsyncConnection db)
//        {
//            _db = db;
//        }

//        public Task<int> CreateUserAsync(User user)
//        {
//            return _db.InsertAsync(user);
//        }

//        public Task<User> GetByEmailAsync(string email)
//        {
//            return _db.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();
//        }

//        public Task<User> GetByUsernameAsync(string username)
//        {
//            return _db.Table<User>().Where(u => u.Username == username).FirstOrDefaultAsync();
//        }
//    }
//}

using SQLite;
using IT3048_MAUI_Grocery_List.Models;

namespace IT3048_MAUI_Grocery_List.Services
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

        public Task<User?> GetByUsernameAsync(string username)
        {
            return _db.Table<User>()
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return _db.Table<User>()
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }

        public Task<int> DeleteUserAsync(int userId)
        {
            return _db.DeleteAsync<User>(userId);
        }
    }
}