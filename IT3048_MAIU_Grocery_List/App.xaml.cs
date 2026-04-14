using IT3048_MAIU_Grocery_List.Pages;

namespace IT3048_MAIU_Grocery_List
{
    public partial class App : Application
    {
        public App(LoginPage loginPage)
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}