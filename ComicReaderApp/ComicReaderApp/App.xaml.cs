using ComicReaderApp.Data;
using Xamarin.Forms;

namespace ComicReaderApp
{
    public partial class App : Application
	{
        public App ()
		{
			InitializeComponent();
			//Used to clear all usersettings, for debugging pruposes only. Reset to comment when deploying the app.
            //UserSettings.ClearEverything();
			//Initialize Favorites and Bookmarks. Read them from their respective storage. ToDo, refactor History into persistent storage and load from here
			ComicFavoriteStore.LoadFavorites();
			ComicBookmarkStore.LoadBookmarks();
			//Instantiate new Mainpage of type Navigationpage of type ComicListViewPage and Set header background and text color.
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
