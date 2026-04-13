using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using IT3048_MAIU_Grocery_List;

namespace IT3048_MAIU_Grocery_List.Services
{
    public class AppDatabaseService
    {
        private readonly SQLiteAsyncConnection _db;

        public UserRepository Users { get; }

        public AppDatabaseService(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);

            _db.CreateTableAsync<Models.User>().Wait();

            Users = new UserRepository(_db);
        }
    }
}
