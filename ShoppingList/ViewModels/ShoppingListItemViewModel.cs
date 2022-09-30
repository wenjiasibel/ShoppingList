using System;
using Prism.Commands;
using Prism.Mvvm;

namespace ShoppingList.ViewModels
{
    public class ShoppingListItemViewModel : BindableBase
    {
        #region Public Properties
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Action<ShoppingListItemViewModel> OnDeleteClicked;
        #endregion

        #region Commands
        public DelegateCommand DeleteClickedCommand => new DelegateCommand(DeleteClicked);
        #endregion

        public ShoppingListItemViewModel()
        {
        }

        #region Private Implementations
        private void DeleteClicked()
        {
            OnDeleteClicked?.Invoke(this);
        }
        #endregion
    }
}
