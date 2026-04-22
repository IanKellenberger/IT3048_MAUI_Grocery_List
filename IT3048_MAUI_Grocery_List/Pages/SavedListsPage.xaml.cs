using IT3048_MAUI_Grocery_List.ViewModels;
using IT3048_MAUI_Grocery_List.Services;

namespace IT3048_MAUI_Grocery_List.Pages;

public partial class SavedListsPage : ContentPage
{
    public SavedListsPage(GroceryListRepository repo)
    {
        InitializeComponent();
        BindingContext = new SavedListsViewModel(repo);
    }
}
