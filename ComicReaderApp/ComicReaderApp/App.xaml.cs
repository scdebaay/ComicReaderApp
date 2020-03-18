using ComicReaderApp.Data;
using Xamarin.Forms;

namespace ComicReaderApp
{
    public partial class App : Application
	{
        public App ()
		{
			InitializeComponent();
            //UserSettings.ClearEverything();
            MainPage = new NavigationPage(new ComicListViewPage());
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = (Color)GetResourceValue("HeaderBackGroundColour");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = (Color)GetResourceValue("TitleFontColour");
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        private object GetResourceValue(string keyName)
        {
            // Search all dictionaries
            if (Application.Current.Resources.TryGetValue(keyName, out var retVal)) { }
            return retVal;
        }
    }
}
