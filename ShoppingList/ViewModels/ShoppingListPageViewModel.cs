using System;
using System.Collections.ObjectModel;
using System.IO;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ShoppingList.Models;
using ShoppingList.Views;
using Xamarin.Forms;

namespace ShoppingList.ViewModels
{
    public class ShoppingListPageViewModel : BindableBase, IInitialize
    {
        #region Public Properties
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Action<ShoppingListItemViewModel> OnDeleteClicked;

        public ObservableCollection<ShoppingListItemViewModel> ShoppingListItemViewModels { get; }

        static Database database;
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "items.db3"));
                }
                return database;
            }
        }

        public INavigationService NavigationService { get; }
        #endregion

        #region Commands
        public DelegateCommand AddItemClickedCommand => new DelegateCommand(AddItemClicked);
        #endregion


        public ShoppingListPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            ShoppingListItemViewModels = new ObservableCollection<ShoppingListItemViewModel>();
        }

        public async void Initialize(INavigationParameters parameters)
        {
            // Initialize list items
            var items = await Database.GetItemsAsync();
            foreach (Item item in items)
            {
                ShoppingListItemViewModels.Add(new ShoppingListItemViewModel(item)
                {
                    OnDeleteClicked = x => DeleteItem(x),
                    OnUpdated = x => UpdateItem(x)
                });
            }
        }

        #region Private Implementations
        private async void AddItemClicked()
        {
            Action<ShoppingListItemViewModel> callback = item => AddItem(item);
            NavigationParameters navigationParams = new NavigationParameters()
            {
                { nameof(AddNewItemPageViewModel.OnSubmitClicked), callback }
            };
            var result = await NavigationService.NavigateAsync($"{nameof(AddNewItemPage)}", navigationParams);
        }

        private async void AddItem(ShoppingListItemViewModel item)
        {
            item.OnDeleteClicked = x => ShoppingListItemViewModels.Remove(x);
            item.OnUpdated = x => UpdateItem(x);
            ShoppingListItemViewModels.Add(item);

            var newItem = new Item()
            {
                Name = item.Name,
                Quantity = item.Quantity,
                IsChecked = item.IsChecked
            };
            await Database.SaveItemAsync(newItem);
        }

        private async void DeleteItem(ShoppingListItemViewModel item)
        {
            ShoppingListItemViewModels.Remove(item);
            await Database.DeleteItemAsync(item.Model);
        }

        private async void UpdateItem(ShoppingListItemViewModel item)
        {
            await Database.UpdateItemAsync(item.Model);
        }
        #endregion
    }
}
