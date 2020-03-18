using ComicReaderApp.ViewModels;
using ComicReaderApp.Views;
using System;
using Xamarin.Forms;

namespace ComicReaderApp
{
    public partial class ComicListViewPage : ContentPage
    {

        public ComicListViewPage()
        {
            InitializeComponent();
            BindingContext = new ComicListViewModel(Navigation);
        }

        private ComicListViewModel ComicListViewModel
        {
            get { return GetValue(BindingContextProperty) as ComicListViewModel; }
            set { SetValue(BindingContextProperty, value); }
        }

        private bool RefreshInitialPage = false;

        async void Settings_ClickedAsync(object sender, EventArgs e)
        {
            RefreshInitialPage = true;
            await Navigation.PushAsync(new SettingsContentPage());            
        }

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
