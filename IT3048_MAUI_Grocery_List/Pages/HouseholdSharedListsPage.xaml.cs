using IT3048_MAUI_Grocery_List.Models;
using IT3048_MAUI_Grocery_List.Services;

namespace IT3048_MAUI_Grocery_List.Pages;


public partial class HouseholdSharedListsPage : ContentPage, IQueryAttributable
{
    private readonly AppDatabaseService _database;
    private int _householdId;
    private int _userId;

    public HouseholdSharedListsPage(AppDatabaseService database)
    {
        InitializeComponent();
        _database = database;
    }

    // Receives householdId + userId from navigation
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _householdId = int.Parse(query["householdId"].ToString());
        _userId = int.Parse(query["userId"].ToString());
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var lists = await _database.SharedLists.GetListsForHouseholdAsync(_householdId);
        SharedListsView.ItemsSource = lists;
    }

    private async void OnListSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is SharedHouseholdList list)
        {
            await Shell.Current.GoToAsync($"SharedListPage?listId={list.Id}");
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnShareListClicked(object sender, EventArgs e)
    {
        // Load user's personal saved lists
        var savedLists = await _database.GroceryLists.GetSavedListsForUserAsync(_userId);

        if (!savedLists.Any())
        {
            await DisplayAlert("No Lists", "You have no saved lists to share.", "OK");
            return;
        }

        // Ask user which list to share
        string action = await DisplayActionSheet(
            "Select a list to share:",
            "Cancel",
            null,
            savedLists.Select(l => l.ListName).ToArray());

        if (action == "Cancel" || string.IsNullOrWhiteSpace(action))
            return;

        var selectedList = savedLists.First(l => l.ListName == action);

        await ShareListToHousehold(selectedList.Id);

        await DisplayAlert("Success", "List shared with household.", "OK");

        // Refresh shared lists
        OnAppearing();
    }

    private async Task ShareListToHousehold(int savedListId)
    {
        // Get the personal list
        var savedList = await _database.GroceryLists.GetSavedListByIdAsync(savedListId);
        var items = await _database.GroceryLists.GetItemsForListAsync(savedListId);

        // Create shared list
        var sharedList = new SharedHouseholdList
        {
            HouseholdId = _householdId,
            CreatedByUserId = _userId,
            ListName = savedList.ListName,
            CreatedAt = DateTime.Now
        };

        await _database.SharedLists.AddListAsync(sharedList);

        // Copy items
        foreach (var item in items)
        {
            await _database.SharedLists.AddItemAsync(new SharedHouseholdListItem
            {
                SharedListId = sharedList.Id,
                ItemName = item.ItemName,
                IsChecked = false
            });
        }
    }
}