using ComicReaderApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ComicReaderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsContentPage : ContentPage
	{
		public SettingsContentPage()
		{
			InitializeComponent();

            BindingContext = new SettingsPageViewModel();
        }

        async void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}