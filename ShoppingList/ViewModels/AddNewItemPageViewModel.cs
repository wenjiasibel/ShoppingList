using System;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace ShoppingList.ViewModels
{
    public class AddNewItemPageViewModel : BindableBase, IInitialize
    {
        #region Public Properties
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Action<ShoppingListItemViewModel> OnSubmitClicked;

        public INavigationService NavigationService { get; }
        #endregion

        #region Commands
        public DelegateCommand SubmitClickedCommand => new DelegateCommand(SubmitClicked);
        #endregion

        public AddNewItemPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public void Initialize(INavigationParameters parameters)
        {
            parameters.TryGetValue(nameof(OnSubmitClicked), out OnSubmitClicked);
        }

        #region Private Implementations
        private void SubmitClicked()
        {
            OnSubmitClicked?.Invoke(new ShoppingListItemViewModel()
            {
                Name = Name,
                Quantity = Quantity
            });
            NavigationService.GoBackAsync();
        }
        #endregion
    }
}
