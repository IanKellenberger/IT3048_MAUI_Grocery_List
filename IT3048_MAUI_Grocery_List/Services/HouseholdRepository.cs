using SQLite;
using IT3048_MAUI_Grocery_List.Models;

namespace IT3048_MAUI_Grocery_List.Services
{
    public class HouseholdRepository
    {
        private readonly SQLiteAsyncConnection _db;

        public HouseholdRepository(SQLiteAsyncConnection db)
        {
            _db = db;
        }

        public async Task<int> CreateHouseholdAsync(Household household)
        {
            await _db.InsertAsync(household);
            return household.Id;
        }

        public Task<Household?> GetByNameAsync(string name)
        {
            return _db.Table<Household>()
                .Where(h => h.Name == name)
                .FirstOrDefaultAsync();
        }

        public Task<int> AddMemberAsync(HouseholdMember member)
        {
            return _db.InsertAsync(member);
        }

        public Task<HouseholdMember?> GetMembershipAsync(int householdId, int userId)
        {
            return _db.Table<HouseholdMember>()
                .Where(x => x.HouseholdId == householdId && x.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public Task<List<Household>> GetHouseholdsForUserAsync(int userId)
        {
            return _db.QueryAsync<Household>(
                @"SELECT h.*
                  FROM Household h
                  INNER JOIN HouseholdMember hm ON h.Id = hm.HouseholdId
                  WHERE hm.UserId = ?",
                userId);
        }

        public Task<List<User>> GetMembersAsync(int householdId)
        {
            return _db.QueryAsync<User>(
                @"SELECT u.*
                  FROM User u
                  INNER JOIN HouseholdMember hm ON u.Id = hm.UserId
                  WHERE hm.HouseholdId = ?",
                householdId);
        }
    }
}
