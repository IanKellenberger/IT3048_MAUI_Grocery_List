using IT3048_MAUI_Grocery_List.ViewModels;
using IT3048_MAUI_Grocery_List.Pages;

namespace IT3048_MAUI_Grocery_List.Pages;

public partial class GroceryListPage : ContentPage
{
    public GroceryListPage(string username)
    {
        InitializeComponent();
        UsernameLabel.Text = $"Logged in as {username}";

        if (BindingContext is GroceryListViewModel vm)
        {
            vm.ViewSavedListsRequested += async () =>
            {
                await Shell.Current.GoToAsync(nameof(SavedListsPage));
            };
        }
    }
}
