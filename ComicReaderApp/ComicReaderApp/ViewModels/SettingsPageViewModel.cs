using ComicReaderApp.Data;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using ComicReaderApp.Models;
using System.Threading.Tasks;
using System;

namespace ComicReaderApp.ViewModels
{
    /// <summary>
    /// Settings viewmodel class
    /// </summary>
    class SettingsPageViewModel : INotifyPropertyChanged
    {
        #region Readonly fields        
        /// <summary>
        /// Toastlength definition from Toast plugin.
        /// </summary>
        readonly ToastLength toastLength = ToastLength.Short;
        /// <summary>
        /// Instantiate API call service object. This handles API calls to load list of available Comic List Items.
        /// </summary>
        readonly ComicApiCallService _comicApiCallService = new ComicApiCallService();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor, Reads API URL, Pagelimit and ComicSize from UserSettings object. Uses Reflection to display app version.
        /// </summary>
        public SettingsPageViewModel()
        {
            ApiLocation = UserSettings.ApiLocation;
            PageLimit = UserSettings.PageLimit;
            ComicSize = UserSettings.ComicSize;
            AppVersion = $"{typeof(SettingsPageViewModel).Assembly.GetName()} version {typeof(SettingsPageViewModel).Assembly.GetName().Version}";
            GetApiVersion();
        }
        #endregion

        #region Public fields
        /// <summary>
        /// Public accessor for API URL from the Settings View Page.
        /// </summary>
        public string ApiLocation { get; set; }

        /// <summary>
        /// Public accessor for PageLimit from the Settings View Page.
        /// </summary>
        public int PageLimit { get; set; }

        /// <summary>
        /// Public accessor for ComicSize from the Settings View Page.
        /// </summary>
        public int ComicSize { get; set; }

        /// <summary>
        /// Public accessor for AppVersion from the Settings View Page.
        /// </summary>
        public string AppVersion { get; set; }

        /// <summary>
        /// Public accessor for ApiVersion from the Settings View Page.
        /// </summary>
        public ApiVersionModel ApiVersionModel 
        { 
            get { return _apiVersionModel; } 
            set { _apiVersionModel = value; OnPropertyChanged(nameof(ApiVersionModel)); } 
        }

        #region Private parts
        private ApiVersionModel _apiVersionModel = new ApiVersionModel();
        private async void GetApiVersion()
        {
            ApiVersionModel = await _comicApiCallService.GetApiVersionAsync();
        }
        #endregion
        #endregion

        #region Commands
        /// <summary>
        /// Command to cancel and revert the input in the Settings fields. Rereads data from UserSettings object
        /// </summary>
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

        /// <summary>
        /// Command to confirm and save the input in the Settings fields. Stores data into UserSettings object
        /// </summary>
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

        /// <summary>
        /// Command to clear all bookmarks. Uses Toast to confirm deletion.
        /// </summary>
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
        #endregion

        #region Property helpers
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
