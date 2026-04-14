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
            // await Shell.Current.GoToAsync($"GroceryListPage?username={username}");

			//Current logic to display username when logged in
            await Navigation.PushAsync(new GroceryListPage(username));
        }
	}
	private async void OnSignupTapped(object sender, TappedEventArgs e)
	{
		await Shell.Current.GoToAsync("SignUpPage");
	}

	//Database logic, checks the database for the entered username and password
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