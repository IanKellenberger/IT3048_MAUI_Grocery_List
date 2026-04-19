using IT3048_MAUI_Grocery_List.Models;
using IT3048_MAUI_Grocery_List.Services;

namespace IT3048_MAUI_Grocery_List.Pages;

public partial class HouseholdPage : ContentPage
{
    private readonly string _username;
    private readonly AppDatabaseService _database;
    private User? _currentUser;

    public HouseholdPage(string username, AppDatabaseService database)
    {
        InitializeComponent();
        _username = username;
        _database = database;

        CurrentUserLabel.Text = $"Logged in as {_username}";
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        _currentUser = await _database.Users.GetByUsernameAsync(_username);
        await LoadHouseholdsAsync();
    }

    private async Task LoadHouseholdsAsync()
    {
        if (_currentUser == null)
            return;

        var households = await _database.Households.GetHouseholdsForUserAsync(_currentUser.Id);
        HouseholdCollectionView.ItemsSource = households;
        MembersCollectionView.ItemsSource = null;
    }

    private async void OnCreateHouseholdClicked(object sender, EventArgs e)
    {
        MessageLabel.Text = string.Empty;

        if (_currentUser == null)
            return;

        string householdName = HouseholdNameEntry.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(householdName))
        {
            MessageLabel.Text = "Please enter a household name.";
            return;
        }

        var existing = await _database.Households.GetByNameAsync(householdName);
        if (existing != null)
        {
            MessageLabel.Text = "That household already exists.";
            return;
        }

        var household = new Household
        {
            Name = householdName,
            CreatedByUserId = _currentUser.Id
        };

        await _database.Households.CreateHouseholdAsync(household);

        await _database.Households.AddMemberAsync(new HouseholdMember
        {
            HouseholdId = household.Id,
            UserId = _currentUser.Id
        });

        HouseholdNameEntry.Text = string.Empty;
        await LoadHouseholdsAsync();
        await DisplayAlert("Success", "Household created.", "OK");
    }

    private async void OnJoinHouseholdClicked(object sender, EventArgs e)
    {
        MessageLabel.Text = string.Empty;

        if (_currentUser == null)
            return;

        string householdName = HouseholdNameEntry.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(householdName))
        {
            MessageLabel.Text = "Please enter a household name.";
            return;
        }

        var household = await _database.Households.GetByNameAsync(householdName);
        if (household == null)
        {
            MessageLabel.Text = "Household not found.";
            return;
        }

        var existingMembership = await _database.Households.GetMembershipAsync(household.Id, _currentUser.Id);
        if (existingMembership != null)
        {
            MessageLabel.Text = "You are already in that household.";
            return;
        }

        await _database.Households.AddMemberAsync(new HouseholdMember
        {
            HouseholdId = household.Id,
            UserId = _currentUser.Id
        });

        HouseholdNameEntry.Text = string.Empty;
        await LoadHouseholdsAsync();
        await DisplayAlert("Success", "Joined household.", "OK");
    }

    private async void OnHouseholdSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Household household)
        {
            var members = await _database.Households.GetMembersAsync(household.Id);
            MembersCollectionView.ItemsSource = members;
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}