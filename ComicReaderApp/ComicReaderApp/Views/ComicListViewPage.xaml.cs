using ComicReaderApp.ViewModels;
using ComicReaderApp.Views;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ComicReaderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComicListViewPage : ContentPage
    {
        //Instantiate Main Comic Listview page, bind to ComicListViewModel
        public ComicListViewPage()
        {            
            InitializeComponent();            
            BindingContext = new ComicListViewModel();
            
        }
        private ComicListViewModel ComicListViewModel
        {
            get { return GetValue(BindingContextProperty) as ComicListViewModel; }
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

        //Click handler when Favorites button is clicked. Switch to Favoritespage. When returning to this page, the List should not be refreshed.
        async void Favorites_ClickedAsync(object sender, EventArgs e)
        {
            RefreshInitialPage = false;
            await Navigation.PushAsync(new FavoriteListViewPage());
        }

        //Click handler when History button is clicked. Switch to Historypage. When returning to this page, the List should not be refreshed.
        async void History_ClickedAsync(object sender, EventArgs e)
        {
            RefreshInitialPage = false;
            await Navigation.PushAsync(new HistoryListViewPage());
        }

        async void Search_ClickedAsync(object sender, EventArgs e)
        {
            ComicListViewModel.Items.Clear();
            await ComicListViewModel.Items.LoadMoreAsync();
        }

        //OnAppearing override, checks whether RefreshInitialPage is set, of so, the item list is cleared and reloaded.
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (RefreshInitialPage) {
                ComicListViewModel.Items.Clear();
                ComicListViewModel.Items.LoadMoreAsync();
                RefreshInitialPage = false;
            }
        }
    }
}
