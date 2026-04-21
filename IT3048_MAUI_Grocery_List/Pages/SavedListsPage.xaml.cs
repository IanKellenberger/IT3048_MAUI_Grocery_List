using IT3048_MAUI_Grocery_List.ViewModels;

namespace IT3048_MAUI_Grocery_List.Pages;

public partial class SavedListsPage : ContentPage
{
    public SavedListsPage()
    {
        InitializeComponent();

        BindingContext = new SavedListsViewModel(
            App.DatabaseService.GroceryLists
        );
    }
}
