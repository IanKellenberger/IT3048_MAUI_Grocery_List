using IT3048_MAUI_Grocery_List.Pages;

namespace IT3048_MAUI_Grocery_List
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(GroceryListPage), typeof(GroceryListPage));
            Routing.RegisterRoute(nameof(SavedListsPage), typeof(SavedListsPage));

            Routing.RegisterRoute("Login", typeof(LoginPage));
            Routing.RegisterRoute("SignUpPage", typeof(SignUpPage));
            Routing.RegisterRoute("GroceryListPage", typeof(GroceryListPage));
            Routing.RegisterRoute("HouseholdPage", typeof(HouseholdPage));
            Routing.RegisterRoute("ProfilePage", typeof(ProfilePage));
            Routing.RegisterRoute("HouseholdSharedListsPage", typeof(HouseholdSharedListsPage));
            Routing.RegisterRoute("SharedListPage", typeof(SharedListPage));
        }
    }
}
