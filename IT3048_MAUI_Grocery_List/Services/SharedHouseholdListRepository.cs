using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IT3048_MAUI_Grocery_List.Models;

namespace IT3048_MAUI_Grocery_List.Services
{
    public class SharedHouseholdListRepository
    {
        private readonly SQLiteAsyncConnection _connection;

        public SharedHouseholdListRepository(SQLiteAsyncConnection connection)
        {
            _connection = connection;
            _connection.CreateTableAsync<SharedHouseholdList>().Wait();
            _connection.CreateTableAsync<SharedHouseholdListItem>().Wait();
        }

        public Task<List<SharedHouseholdList>> GetListsForHouseholdAsync(int householdId)
        {
            return _connection.Table<SharedHouseholdList>()
                .Where(l => l.HouseholdId == householdId)
                .ToListAsync();
        }

        public Task<List<SharedHouseholdListItem>> GetItemsAsync(int sharedListId)
        {
            return _connection.Table<SharedHouseholdListItem>()
                .Where(i => i.SharedListId == sharedListId)
                .ToListAsync();
        }

        public Task<int> AddListAsync(SharedHouseholdList list)
            => _connection.InsertAsync(list);

        public Task<int> AddItemAsync(SharedHouseholdListItem item)
            => _connection.InsertAsync(item);

        public Task<int> UpdateItemAsync(SharedHouseholdListItem item)
            => _connection.UpdateAsync(item);
    }
}
