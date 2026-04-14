
using IT3048_MAUI_Grocery_List.Services;
using IT3048_MAUI_Grocery_List.ViewModels;

namespace IT3048_MAUI_Grocery_List.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(AppDatabaseService database)
	{
		InitializeComponent();
		BindingContext = new SignUpViewModel(database);
	}
}