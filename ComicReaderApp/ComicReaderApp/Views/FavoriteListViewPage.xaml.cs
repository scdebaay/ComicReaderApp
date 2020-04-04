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
        public FavoriteListViewPage()
        {
            InitializeComponent();
            BindingContext = new ComicFavoriteViewModel(Navigation);
        }

        private ComicFavoriteViewModel FavoriteListViewModel
        {
            get { return GetValue(BindingContextProperty) as ComicFavoriteViewModel; }
            set { SetValue(BindingContextProperty, value); }
        }

        private bool RefreshInitialPage = false;

        async void Settings_ClickedAsync(object sender, EventArgs e)
        {
            RefreshInitialPage = true;
            await Navigation.PushAsync(new SettingsContentPage());
        }

        void Delete_Clicked(object sender, EventArgs e)
        {
            FavoriteListViewModel.Items.Clear();
            FavoriteListViewModel.ClearFavorites();
            ComicFavoriteStore.Clear();
            ComicFavoriteStore.SaveFavorites();
            CrossToastPopUp.Current.ShowToastWarning("Favorites cleared", toastLength);
        }

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
