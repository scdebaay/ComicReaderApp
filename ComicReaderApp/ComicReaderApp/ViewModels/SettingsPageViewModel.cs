using ComicReaderApp.Data;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Toast;
using Plugin.Toast.Abstractions;

namespace ComicReaderApp.ViewModels
{
    class SettingsPageViewModel : INotifyPropertyChanged
    {
        ToastLength toastLength = ToastLength.Short;
        public SettingsPageViewModel()
        {
            ApiLocation = UserSettings.ApiLocation;
            PageLimit = UserSettings.PageLimit;
            ComicSize = UserSettings.ComicSize;
            AppVersion = $"{typeof(SettingsPageViewModel).Assembly.GetName()} version {typeof(SettingsPageViewModel).Assembly.GetName().Version}";
        }

        private string _apiLocation;
        public string ApiLocation {
            get { return _apiLocation; }
            set {
                _apiLocation = value;
                OnPropertyChanged(nameof(ApiLocation));
            }
        }
        private int _pageLimit;
        public int PageLimit {
            get { return _pageLimit; }
            set
            {
                _pageLimit = value;
                OnPropertyChanged(nameof(PageLimit));
            }
        }
        private int _comicSize;
        public int ComicSize {
            get { return _comicSize; }
            set
            {
                _comicSize = value;
                OnPropertyChanged(nameof(ComicSize));
            }
        }
        private string _appVersion;
        public string AppVersion {
            get { return _appVersion; }
            set
            {
                _appVersion = value;
                OnPropertyChanged(nameof(AppVersion));
            }
        }

        public ICommand Cancel
        {
            get
            {
                return new Command(() =>
                {
                    ApiLocation = UserSettings.ApiLocation;
                    PageLimit = UserSettings.PageLimit;
                    ComicSize = UserSettings.ComicSize;                    
                }
                );
            }
        }

        public ICommand Save
        {
            get
            {
                return new Command(() =>
                {
                    UserSettings.ApiLocation = ApiLocation;
                    UserSettings.PageLimit = PageLimit;
                    UserSettings.ComicSize = ComicSize;
                    CrossToastPopUp.Current.ShowToastSuccess("Settings saved", toastLength);
                }
                );
            }
        }

        public ICommand ClearBookmarks
        { 
            get
            {
                return new Command(() =>
                {
                    ComicBookmarkStore.LoadBookmarks();
                    ComicBookmarkStore.Clear();
                    ComicBookmarkStore.SaveBookmarks();
                    CrossToastPopUp.Current.ShowToastWarning("Bookmarks cleared", toastLength);
                }
                );
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
