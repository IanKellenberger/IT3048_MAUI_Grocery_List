using IT3048_MAIU_Grocery_List.Services;
namespace IT3048_MAIU_Grocery_List.Pages;

public partial class LoginPage : ContentPage
{
	private readonly AppDatabaseService _database;
	public LoginPage(AppDatabaseService database)
	{
		InitializeComponent();
		_database = database;
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
		await Shell.Current.GoToAsync("//SignUpPage");
	}


	//This is the temporary logic to make sure the loginpage looks alright and functions. Replace with SQLite database logic.
	private async Task<bool> ValidateCredentialsAsync(string username, string password)
	{
		var user = await _database.Users.GetByUsernameAsync(username);

		if (user == null)
		{
			return false;
		}

		return user.PasswordHash == password;
	}
}