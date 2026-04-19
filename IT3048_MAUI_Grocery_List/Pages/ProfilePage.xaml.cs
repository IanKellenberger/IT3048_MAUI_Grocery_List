using IT3048_MAUI_Grocery_List.Models;
using IT3048_MAUI_Grocery_List.Services;

namespace IT3048_MAUI_Grocery_List.Pages;

public partial class ProfilePage : ContentPage
{
    private readonly string _username;
    private readonly AppDatabaseService _database;
    private User? _currentUser;

    public ProfilePage(string username, AppDatabaseService database)
    {
        InitializeComponent();
        _username = username;
        _database = database;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _currentUser = await _database.Users.GetByUsernameAsync(_username);

        if (_currentUser == null)
        {
            MessageLabel.Text = "Unable to load user profile.";
            return;
        }

        UsernameLabel.Text = _currentUser.Username;
        EmailLabel.Text = string.IsNullOrWhiteSpace(_currentUser.Email)
            ? "No email on file"
            : _currentUser.Email;
    }

    private async void OnGroceryListClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GroceryListPage(_username, _database));
    }

    private async void OnHouseholdsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HouseholdPage(_username, _database));
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Login");
    }

    private async void OnDeleteAccountClicked(object sender, EventArgs e)
    {
        if (_currentUser == null)
        {
            MessageLabel.Text = "User not found.";
            return;
        }

        bool confirm = await DisplayAlert(
            "Delete Account",
            "Are you sure you want to delete this account?",
            "Yes",
            "No");

        if (!confirm)
            return;

        await _database.Users.DeleteUserAsync(_currentUser.Id);

        await DisplayAlert("Deleted", "Your account has been deleted.", "OK");
        await Shell.Current.GoToAsync("//Login");
    }
}
