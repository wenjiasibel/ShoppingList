using System;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace ShoppingList.ViewModels
{
    public class AddNewItemPageViewModel : BindableBase, INavigationAware
    {
        #region Public Properties
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Action<ShoppingListItemViewModel> OnSubmitClicked;
        #endregion

        #region Commands
        public DelegateCommand SubmitClickedCommand => new DelegateCommand(SubmitClicked);
        #endregion

        public AddNewItemPageViewModel()
        {
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            parameters.TryGetValue(nameof(OnSubmitClicked), out OnSubmitClicked);
        }

        #region Private Implementations
        private void SubmitClicked()
        {
            OnSubmitClicked?.Invoke(new ShoppingListItemViewModel()
            {
                Name = this.Name,
                Quantity = this.Quantity
            });
        }
        #endregion
    }
}
