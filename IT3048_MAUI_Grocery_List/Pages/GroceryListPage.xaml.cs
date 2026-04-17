using System.Collections.ObjectModel;
using IT3048_MAUI_Grocery_List.Models;
using IT3048_MAUI_Grocery_List.Services;

namespace IT3048_MAUI_Grocery_List.Pages
{
    public partial class GroceryListPage : ContentPage
    {
        private readonly AppDatabaseService _database;
        private readonly ObservableCollection<GroceryItem> _items = new();

        public GroceryListPage(AppDatabaseService database)
        {
            InitializeComponent();
            _database = database;
            GroceryCollectionView.ItemsSource = _items;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadItemsAsync();
        }

        private async Task LoadItemsAsync()
        {
            _items.Clear();
            var items = await _database.Groceries.GetAllItemsAsync();

            foreach (var item in items)
            {
                _items.Add(item);
            }
        }

        private async void OnAddItemClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                await DisplayAlert("Error", "Please enter an item name.", "OK");
                return;
            }

            int quantity = 1;
            if (!string.IsNullOrWhiteSpace(QuantityEntry.Text))
            {
                int.TryParse(QuantityEntry.Text, out quantity);
                if (quantity <= 0)
                {
                    quantity = 1;
                }
            }

            var newItem = new GroceryItem
            {
                Name = NameEntry.Text.Trim(),
                Quantity = quantity,
                Category = CategoryEntry.Text?.Trim() ?? string.Empty,
                IsChecked = false
            };

            await _database.Groceries.AddItemAsync(newItem);

            NameEntry.Text = string.Empty;
            QuantityEntry.Text = string.Empty;
            CategoryEntry.Text = string.Empty;

            await LoadItemsAsync();
        }

        private async void OnDeleteItemClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is GroceryItem item)
            {
                await _database.Groceries.DeleteItemAsync(item);
                await LoadItemsAsync();
            }
        }

        private async void OnItemCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.BindingContext is GroceryItem item)
            {
                item.IsChecked = e.Value;
                await _database.Groceries.UpdateItemAsync(item);
            }
        }
    }
}