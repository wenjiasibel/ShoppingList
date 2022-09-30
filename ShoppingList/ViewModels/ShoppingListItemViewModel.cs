using System;
using Prism.Commands;
using Prism.Mvvm;
using ShoppingList.Models;

namespace ShoppingList.ViewModels
{
    public class ShoppingListItemViewModel : BindableBase
    {
        #region Public Properties
        public string Name
        {
            get { return Model.Name; }
            set { Model.Name = value; }
        }

        public int Quantity
        {
            get { return Model.Quantity; }
            set {
                Model.Quantity = value;
                OnUpdated?.Invoke(this);
            }
        }

        public bool IsChecked
        {
            get { return Model.IsChecked; }
            set {
                Model.IsChecked = value;
                OnUpdated?.Invoke(this);
            }
        }

        public Item Model { get; }
        public Action<ShoppingListItemViewModel> OnDeleteClicked;
        public Action<ShoppingListItemViewModel> OnUpdated;
        #endregion

        #region Commands
        public DelegateCommand DeleteClickedCommand => new DelegateCommand(DeleteClicked);
        #endregion

        public ShoppingListItemViewModel(Item model)
        {
            Model = model;
        }

        #region Private Implementations
        private void DeleteClicked()
        {
            OnDeleteClicked?.Invoke(this);
        }
        #endregion
    }
}
