using SQLite;
using IT3048_MAUI_Grocery_List.Models;

namespace IT3048_MAUI_Grocery_List.Services
{
    public class AppDatabaseService
    {
        private readonly SQLiteAsyncConnection _db;

        public UserRepository Users { get; }
        public GroceryRepository Groceries { get; }

        public AppDatabaseService(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);

            _db.CreateTableAsync<User>().Wait();
            _db.CreateTableAsync<GroceryItem>().Wait();

            Users = new UserRepository(_db);
            Groceries = new GroceryRepository(_db);

            // System.Diagnostics.Debug.WriteLine($"DB PATH: {dbPath}");
        }
    }
}