using IT3048_MAUI_Grocery_List.Services;
using System.ComponentModel;
using IT3048_MAUI_Grocery_List.Models;

namespace IT3048_MAUI_Grocery_List.ViewModels;

public class SignUpViewModel : INotifyPropertyChanged
{

	public event PropertyChangedEventHandler PropertyChanged;

	private string _username;
	private string _email;
	private string _password;
	private string _confirmPassword;
	private string _errorMessage;
	private bool _hasError;
    private readonly AppDatabaseService _database;

	public string Username
	{
		get => _username;
		set { _username = value; OnPropertyChanged(nameof(Username)); }

	}

	public string Email
	{
		get => _email;
		set { _email = value; OnPropertyChanged(nameof(_email)); }

	}

    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(nameof(_password)); }

    }

    public string ConfirmPassword
    {
        get => _confirmPassword;
        set { _confirmPassword = value; OnPropertyChanged(nameof(_confirmPassword)); }

    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
    }

    public bool HasError
    {
        get => _hasError;
        set { _hasError = value; OnPropertyChanged(nameof(HasError)); }
    }

    public Command SignUpCommand { get; }
    public Command NavigateToLoginCommand { get; }

    public SignUpViewModel(AppDatabaseService database)
    {
        _database = database;

        SignUpCommand = new Command(OnSignUp);
        NavigateToLoginCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync("//Login");
        });
    }

    private async void OnSignUp()
    {
        HasError = false;

        if (string.IsNullOrWhiteSpace(Username) ||
            string.IsNullOrWhiteSpace(Email) ||
            string.IsNullOrWhiteSpace(Password) ||
            string.IsNullOrWhiteSpace(ConfirmPassword))
        {
            ShowError("All fields are required.");
            return;
        }

        if (Password != ConfirmPassword)
        {
            ShowError("Passwords do not match.");
            return;
        }

        var newUser = new User
        {
            Username = Username,
            Email = Email,
            PasswordHash = Password
        };

        await _database.Users.CreateUserAsync(newUser);

        await Shell.Current.GoToAsync("Login");
    }

    private void ShowError(string message)
    {
        ErrorMessage = message;
        HasError = true;
    }

    private void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

}