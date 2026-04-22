using System.Collections.ObjectModel;
using System.Windows.Input;
using IT3048_MAUI_Grocery_List.Models;

namespace IT3048_MAUI_Grocery_List.ViewModels
{
    public class GroceryListViewModel : BindableObject
    {
        private string _newItemText;

        public event Action? ViewSavedListsRequested;

        public List GroceryList { get; set; } = new();

        public string NewItemText
        {
            get => _newItemText;
            set
            {
                _newItemText = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand ShareListCommand { get; }
        public ICommand SaveListCommand { get; }
        public ICommand ViewSavedListsCommand { get; }

        public GroceryListViewModel()
        {
            AddItemCommand = new Command(AddItem);
            DeleteItemCommand = new Command<GroceryItem>(DeleteItem);
            ShareListCommand = new Command(ShareList);
            SaveListCommand = new Command(SaveList);
            ViewSavedListsCommand = new Command(() => ViewSavedListsRequested?.Invoke());
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

        private async void SaveList()
        {
            await Application.Current.MainPage.DisplayAlert(
                "Saved",
                "Your grocery list has been saved.",
                "OK");
        }
    }
}
