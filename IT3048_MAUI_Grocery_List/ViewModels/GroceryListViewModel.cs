using System.Collections.ObjectModel;
using System.Windows.Input;
using IT3048_MAUI_Grocery_List.Models;
using IT3048_MAUI_Grocery_List.Pages;
using IT3048_MAUI_Grocery_List.Services;


namespace IT3048_MAUI_Grocery_List.ViewModels
{
    public class GroceryListViewModel : BindableObject
    {
        private readonly GroceryListRepository _repo;

        private string _newItemText;
        public string NewItemText
        {
            get => _newItemText;
            set
            {
                _newItemText = value;
                OnPropertyChanged();
            }
        }

        public GroceryList GroceryList { get; set; } = new GroceryList
        {
            Name = "My Grocery List",
            Items = new ObservableCollection<GroceryItem>()
        };

        public ICommand AddItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand ShareListCommand { get; }
        public ICommand SaveListCommand { get; }
        public ICommand ViewSavedListsCommand { get; }

        public GroceryListViewModel(GroceryListRepository repo)
        {
            _repo = repo;

            AddItemCommand = new Command(AddItem);
            DeleteItemCommand = new Command<GroceryItem>(DeleteItem);
            ShareListCommand = new Command(ShareList);
            SaveListCommand = new Command(async () => await SaveList());
            ViewSavedListsCommand = new Command(async () => await NavigateToSavedLists());
        }

        private void AddItem()
        {
            if (!string.IsNullOrWhiteSpace(NewItemText))
            {
                GroceryList.Items.Add(new GroceryItem { Text = NewItemText });
                NewItemText = string.Empty;
            }
        }

        private void DeleteItem(GroceryItem item)
        {
            if (item != null)
                GroceryList.Items.Remove(item);
        }

        private async void ShareList()
        {
            var text = $"{GroceryList.Name}\n\n" +
                       string.Join("\n• ", GroceryList.Items.Select(i => i.Text));

            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Grocery List"
            });
        }

        private async Task SaveList()
        {
            var savedList = new SavedGroceryList
            {
                UserId = 1, 
                ListName = GroceryList.Name,
                CreatedAt = DateTime.Now
            };

            int listId = await _repo.CreateListAsync(savedList);

            var itemsToSave = GroceryList.Items.Select(item => new SavedGroceryItem
            {
                GroceryListId = listId,
                ItemName = item.Text,
                Price = 0,
                IsChecked = false
            });

            await _repo.SaveItemsAsync(itemsToSave);

            await Application.Current.MainPage.DisplayAlert(
                "Saved",
                "Your grocery list has been saved.",
                "OK");
        }

        private async Task NavigateToSavedLists()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SavedListsPage());
        }
    }
}
