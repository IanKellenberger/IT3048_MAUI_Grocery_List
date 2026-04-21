using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IT3048_MAUI_Grocery_List.Models;
using IT3048_MAUI_Grocery_List.Services;

namespace IT3048_MAUI_Grocery_List.ViewModels
{
    public class SavedListsViewModel : INotifyPropertyChanged
    {
        private readonly GroceryListRepository _repo;

        public ObservableCollection<SavedGroceryList> SavedLists { get; } =
            new ObservableCollection<SavedGroceryList>();

        public ObservableCollection<SavedGroceryItem> SelectedListItems { get; } =
            new ObservableCollection<SavedGroceryItem>();

        private SavedGroceryList _selectedList;
        public SavedGroceryList SelectedList
        {
            get => _selectedList;
            set
            {
                if (_selectedList != value)
                {
                    _selectedList = value;
                    OnPropertyChanged();
                    LoadItemsForSelectedList();
                }
            }
        }

        public SavedListsViewModel(GroceryListRepository repo)
        {
            _repo = repo;
            LoadSavedLists();
        }

        private async void LoadSavedLists()
        {
            SavedLists.Clear();

            var lists = await _repo.GetAllListsAsync();

            foreach (var list in lists)
            {
                SavedLists.Add(list);
            }
        }

        private async void LoadItemsForSelectedList()
        {
            SelectedListItems.Clear();

            if (SelectedList == null)
                return;

            var items = await _repo.GetItemsForListAsync(SelectedList.Id);

            foreach (var item in items)
            {
                SelectedListItems.Add(item);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
