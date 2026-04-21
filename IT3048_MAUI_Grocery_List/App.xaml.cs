using IT3048_MAUI_Grocery_List.Services;

namespace IT3048_MAUI_Grocery_List;

public partial class App : Application
{
    public static AppDatabaseService DatabaseService { get; private set; }

    public App(AppDatabaseService databaseService)
    {
        InitializeComponent();

        DatabaseService = databaseService;

        MainPage = new AppShell();
    }
}
