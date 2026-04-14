using IT3048_MAIU_Grocery_List.Services;
using IT3048_MAIU_Grocery_List.Pages;
using Microsoft.Extensions.Logging;

namespace IT3048_MAIU_Grocery_List
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db3");
            builder.Services.AddSingleton<AppDatabaseService>(s => new AppDatabaseService(dbPath));

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddTransient<SignUpPage>();
            builder.Services.AddTransient<GroceryListPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
