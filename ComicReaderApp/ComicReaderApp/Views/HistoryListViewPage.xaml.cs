using ComicReaderApp.ViewModels;
using ComicReaderApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ComicReaderApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryListViewPage : ContentPage
    {

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
