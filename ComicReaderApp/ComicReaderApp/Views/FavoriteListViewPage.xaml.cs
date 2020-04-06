using ComicReaderApp.Data;
using ComicReaderApp.ViewModels;
using ComicReaderApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Toast;
using Plugin.Toast.Abstractions;

namespace ComicReaderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoriteListViewPage : ContentPage
    {
        ToastLength toastLength = ToastLength.Short;
        //Instantiate Favorite Listview page, bind to ComicFavoriteViewModel
        public FavoriteListViewPage()
        {
            InitializeComponent();
            BindingContext = new ComicFavoriteViewModel();
        }

        private ComicFavoriteViewModel FavoriteListViewModel
        {
            get { return GetValue(BindingContextProperty) as ComicFavoriteViewModel; }
            set { SetValue(BindingContextProperty, value); }
        }

        //Private bool to detect whether the page should be refreshed OnAppearing. Defaults to false.
        private bool RefreshInitialPage = false;

        //Click handler when Settings button is clicked. Switch to settingspage. When returning to this page, the List should be refreshed.
        async void Settings_ClickedAsync(object sender, EventArgs e)
        {
            RefreshInitialPage = true;
            await Navigation.PushAsync(new SettingsContentPage());
        }

        //Click handler when Delete button is clicked. Favorites list is cleared and saved, toast is shown to confirm.
        void Delete_Clicked(object sender, EventArgs e)
        {
            FavoriteListViewModel.Items.Clear();
            FavoriteListViewModel.ClearFavorites();
            ComicFavoriteStore.Clear();
            ComicFavoriteStore.SaveFavorites();
            CrossToastPopUp.Current.ShowToastWarning("Favorites cleared", toastLength);
        }

        //OnAppearing override, checks whether RefreshInitialPage is set, of so, the item list is cleared and reloaded.
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (RefreshInitialPage)
            {
                FavoriteListViewModel.Items.Clear();
                FavoriteListViewModel.Items.LoadMoreAsync();
                RefreshInitialPage = false;
            }
        }
    }
}
