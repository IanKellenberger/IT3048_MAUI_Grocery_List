using System.Collections.ObjectModel;
using System.ComponentModel;
using IT3048_MAUI_Grocery_List.Models;
using IT3048_MAUI_Grocery_List.Services;

namespace IT3048_MAUI_Grocery_List.Pages;

public partial class GroceryListPage : ContentPage
{
    private readonly string _username;
    private readonly AppDatabaseService _database;
    private readonly ObservableCollection<GroceryItemRow> _items = new();

    public GroceryListPage(string username, AppDatabaseService database)
    {
        InitializeComponent();
        _username = username;
        _database = database;

        UsernameLabel.Text = "Logged in as " + _username;
        ItemsCollectionView.ItemsSource = _items;

        AddBlankItem();
    }

    private void AddBlankItem()
    {
        var row = new GroceryItemRow();
        row.PropertyChanged += GroceryItem_PropertyChanged;
        _items.Add(row);
        RecalculateTotal();
    }

    private void GroceryItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        RecalculateTotal();
    }

    private void RecalculateTotal()
    {
        decimal total = 0;

        foreach (var item in _items)
        {
            if (decimal.TryParse(item.PriceText, out decimal price))
            {
                total += price;
            }
        }

        TotalLabel.Text = "Total: $" + total.ToString("F2");
    }

    private void OnAddItemClicked(object sender, EventArgs e)
    {
        AddBlankItem();
    }

    private void OnDeleteItemClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is GroceryItemRow row)
        {
            row.PropertyChanged -= GroceryItem_PropertyChanged;
            _items.Remove(row);
            RecalculateTotal();
        }
    }

    private void OnNewListClicked(object sender, EventArgs e)
    {
        ListNameEntry.Text = string.Empty;
        MessageLabel.Text = string.Empty;

        foreach (var item in _items)
        {
            item.PropertyChanged -= GroceryItem_PropertyChanged;
        }

        _items.Clear();
        AddBlankItem();
    }

    private async void OnSaveListClicked(object sender, EventArgs e)
    {
        MessageLabel.Text = string.Empty;

        string listName = ListNameEntry.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(listName))
        {
            MessageLabel.Text = "Please enter a list name.";
            return;
        }

        var filledItems = _items
            .Where(i => !string.IsNullOrWhiteSpace(i.Name))
            .ToList();

        if (filledItems.Count == 0)
        {
            MessageLabel.Text = "Please add at least one item.";
            return;
        }

        var user = await _database.Users.GetByUsernameAsync(_username);

        if (user == null)
        {
            MessageLabel.Text = "Could not find logged in user.";
            return;
        }

        var savedList = new SavedGroceryList
        {
            UserId = user.Id,
            ListName = listName,
            CreatedAt = DateTime.Now
        };

        int listId = await _database.GroceryLists.CreateListAsync(savedList);

        var dbItems = new List<SavedGroceryItem>();

        foreach (var item in filledItems)
        {
            decimal.TryParse(item.PriceText, out decimal parsedPrice);

            dbItems.Add(new SavedGroceryItem
            {
                GroceryListId = listId,
                ItemName = item.Name.Trim(),
                Price = parsedPrice,
                IsChecked = item.IsChecked
            });
        }

        await _database.GroceryLists.SaveItemsAsync(dbItems);

        await DisplayAlert("Success", "Grocery list saved.", "OK");
        OnNewListClicked(sender, e);
    }

    private async void OnHouseholdsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HouseholdPage(_username, _database));
    }

    private async void OnProfileClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage(_username, _database));
    }

    private async void OnViewSavedListsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SavedListsPage());
    }

    public class GroceryItemRow : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private string _priceText = string.Empty;
        private bool _isChecked;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string PriceText
        {
            get => _priceText;
            set
            {
                if (_priceText != value)
                {
                    _priceText = value;
                    OnPropertyChanged(nameof(PriceText));
                }
            }
        }

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged(nameof(IsChecked));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
