using Microsoft.Maui.Controls;

namespace IT3048_MAUI_Grocery_List.Pages;

public partial class SavedListsPage : ContentPage
{
    public SavedListsPage()
    {
        InitializeComponent();
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

}
