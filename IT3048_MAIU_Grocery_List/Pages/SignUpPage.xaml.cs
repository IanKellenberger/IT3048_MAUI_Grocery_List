namespace IT3048_MAIU_Grocery_List.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
		BindingContext = new ViewModels.SignUpViewModel();
	}
}