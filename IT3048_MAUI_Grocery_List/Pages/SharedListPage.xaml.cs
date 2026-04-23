using IT3048_MAUI_Grocery_List.Models;
using IT3048_MAUI_Grocery_List.Services;

namespace IT3048_MAUI_Grocery_List.Pages;

public partial class SharedListPage : ContentPage, IQueryAttributable
{
    private readonly AppDatabaseService _database;
    private int _listId;

    public SharedListPage(AppDatabaseService database)
    {
        InitializeComponent();
        _database = database;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _listId = int.Parse(query["listId"].ToString());
    }

    protected override async void OnAppearing()
    {
        ItemsView.ItemsSource = await _database.SharedLists.GetItemsAsync(_listId);
    }

    private async void OnAddItemClicked(object sender, EventArgs e)
    {
        string name = await DisplayPromptAsync("Add Item", "Item name:");
        if (string.IsNullOrWhiteSpace(name)) return;

        await _database.SharedLists.AddItemAsync(new SharedHouseholdListItem
        {
            SharedListId = _listId,
            ItemName = name,
            IsChecked = false
        });

        OnAppearing();
    }

    private async void OnCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox cb && cb.BindingContext is SharedHouseholdListItem item)
        {
            item.IsChecked = e.Value;
            await _database.SharedLists.UpdateItemAsync(item);
        }
    }
}