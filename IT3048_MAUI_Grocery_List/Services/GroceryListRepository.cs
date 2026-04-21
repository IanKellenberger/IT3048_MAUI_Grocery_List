using SQLite;
using IT3048_MAUI_Grocery_List.Models;

namespace IT3048_MAUI_Grocery_List.Services
{
    public class GroceryListRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public GroceryListRepository(SQLiteAsyncConnection db)
        {
            _db = db;
        }

        public async Task<int> CreateListAsync(SavedGroceryList list)
        {
            await _db.InsertAsync(list);
            return list.Id;
        }

        public Task<int> SaveItemsAsync(IEnumerable<SavedGroceryItem> items)
        {
            return _db.InsertAllAsync(items);
        }

        public Task<List<SavedGroceryList>> GetListsByUserAsync(int userId)
        {
            return _db.Table<SavedGroceryList>()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public Task<List<SavedGroceryList>> GetAllListsAsync()
        {
            return _db.Table<SavedGroceryList>()
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public Task<List<SavedGroceryItem>> GetItemsForListAsync(int listId)
        {
            return _db.Table<SavedGroceryItem>()
                .Where(x => x.GroceryListId == listId)
                .ToListAsync();
        }

        public async Task<int> DeleteListAsync(int listId)
        {
            var items = await GetItemsForListAsync(listId);

            foreach (var item in items)
            {
                await _db.DeleteAsync(item);
            }

            return await _db.DeleteAsync<SavedGroceryList>(listId);
        }
    }
}
