using System;
using Prism.Ioc;
using ShoppingList.ViewModels;
using ShoppingList.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingList
{
    public partial class App
    {
        public App() : base(null)
        {
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            MainPage = new MainPage();

            NavigationService.NavigateAsync($"{nameof(ShoppingListPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Instance Registration
            containerRegistry.RegisterInstance(containerRegistry);

            // Navigation Registration
            containerRegistry.RegisterForNavigation<NavigationPage>();

            // Page Registration
            containerRegistry.RegisterForNavigation<ShoppingListPage, ShoppingListPageViewModel>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
