using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using IT3048_MAUI_Grocery_List;

namespace IT3048_MAUI_Grocery_List.Services
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


            // Un-comment this if you are using the SQLite/SQL Server Compact Toolbox extension and need the db file to view the table contents.
            //Path only works for windows, android path is stored in its private sandbox.
            //System.Diagnostics.Debug.WriteLine($"DB PATH: {dbPath}");
        }

    }
}
