
namespace IT3048_MAIU_Grocery_List.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private async void OnSubmitClicked(object sender, EventArgs e)
	{
		string? username = UsernameEntry.Text?.Trim();
		string password = PasswordEntry.Text;

		if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
		{
			ErrorLabel.Text = "Please enter both username and password.";
			return;
		}

		bool isValid = await ValidateCredentialsAsync(username, password);

		if(!isValid)
		{
            ErrorLabel.Text = "Invalid username or password";
			return;
		}
		else
		{
			ErrorLabel.Text = "";
            await Shell.Current.GoToAsync("//GroceryListPage");
        }
	}
	private async void OnSignupTapped(object sender, TappedEventArgs e)
	{
		await Shell.Current.GoToAsync("//SignupPage");
	}


	//This is the temporary logic to make sure the loginpage looks alright and functions. Replace with SQLite database logic.
	private Task<bool> ValidateCredentialsAsync(string username, string password)
	{
		return Task.FromResult(username == "test" && password == "1234");
	}
}