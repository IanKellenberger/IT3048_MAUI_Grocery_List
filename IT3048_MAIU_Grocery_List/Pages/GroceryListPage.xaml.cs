using IT3048_MAIU_Grocery_List.ViewModels;
using IT3048_MAIU_Grocery_List.Pages;

namespace IT3048_MAIU_Grocery_List.Pages;

public partial class GroceryListPage : ContentPage
{

    public GroceryListPage(string username)
    {
        InitializeComponent();
        //Current logic to display username when logged itn
        UsernameLabel.Text = $"Logged in as {username}";
    }
}