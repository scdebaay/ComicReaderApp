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
    public partial class HistoryListViewPage : ContentPage
    {
        ToastLength toastLength = ToastLength.Short;
        public HistoryListViewPage()
        {            
            InitializeComponent();            
            BindingContext = new ComicHistoryViewModel(Navigation);            
        }

        private ComicHistoryViewModel HistoryListViewModel
        {
            get { return GetValue(BindingContextProperty) as ComicHistoryViewModel; }
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
            HistoryListViewModel.Items.Clear();
            HistoryListViewModel.ClearHistory();
            CrossToastPopUp.Current.ShowToastWarning("History cleared", toastLength);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (RefreshInitialPage) {
                HistoryListViewModel.Items.Clear();
                HistoryListViewModel.Items.LoadMoreAsync();
                RefreshInitialPage = false;
            }
        }
    }
}
