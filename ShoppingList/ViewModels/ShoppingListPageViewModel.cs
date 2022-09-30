using System;
using System.Collections.ObjectModel;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ShoppingList.Views;
using Xamarin.Forms;

namespace ShoppingList.ViewModels
{
    public class ShoppingListPageViewModel : BindableBase, IInitialize, INavigationAware
    {
        #region Public Properties
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Action<ShoppingListItemViewModel> OnDeleteClicked;

        public ObservableCollection<ShoppingListItemViewModel> ShoppingListItemViewModels { get; }

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

        public void Initialize(INavigationParameters parameters)
        {
            // Initialize list items
            ShoppingListItemViewModels.Add(new ShoppingListItemViewModel()
            {
                Name = "Apple pancake",
                Quantity = 2,
                OnDeleteClicked = x =>
                {
                    ShoppingListItemViewModels.Remove(x);
                }
        });
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            throw new NotImplementedException();
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

        private void AddItem(ShoppingListItemViewModel item)
        {
            item.OnDeleteClicked = x =>
            {
                ShoppingListItemViewModels.Remove(x);
            };
            ShoppingListItemViewModels.Add(item);
        }
        #endregion
    }
}
