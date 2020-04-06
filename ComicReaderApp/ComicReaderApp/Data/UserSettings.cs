using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace ComicReaderApp.Data
{
    class UserSettings
    {
        #region Instance

        /// <summary>
        /// Instantiate Usersettings singleton from CrossSettings plugin current implementation.
        /// </summary>
        private static ISettings AppSettings => CrossSettings.Current;

        #endregion

        #region Methods

        /// <summary>
        /// Public method to clear all appsettings. Calls ISettings.Clear().
        /// </summary>
        public static void ClearEverything()
        {
            AppSettings.Clear();
        }

        #endregion

        #region Public Settings

        /// <summary>
        /// Default location for the ComicCloud API, set in Settings.json: apiLocation. Defines the URL where the ComicCloud API is located.
        /// </summary>
        private static readonly string ApiLocationDefault = DefaultAppSettingsManager.ApiLocation;
        
        /// <summary>
        /// Public accessor for API location setting. When not set in User settings, the default value is read from Settings.json
        /// The API URL is user configurable
        /// </summary>
        public static string ApiLocation
        {
            get => AppSettings.GetValueOrDefault(nameof(ApiLocation), ApiLocationDefault);
            set => AppSettings.AddOrUpdateValue(nameof(ApiLocation), value);
        }

        /// <summary>
        /// Default setting for page limit. Read from Settings.json: pageLimit. Denotes how many comics are fetched from the API when listing comics.
        /// </summary>
        private static readonly int PageLimitDefault = DefaultAppSettingsManager.PageLimit;

        /// <summary>
        /// Public accessor for page limit. When not set in UserSettings, the default value is read from Settins.json
        /// The page limit is user configurable
        /// </summary>
        public static int PageLimit
        {
            get => AppSettings.GetValueOrDefault(nameof(PageLimit), PageLimitDefault);
            set => AppSettings.AddOrUpdateValue(nameof(PageLimit), value);
        }

        /// <summary>
        /// Default setting for comic page size. Read from Settings.json: pageSize. Denotes the width in pixels at which comic pages are displayed in the interface.
        /// </summary>
        private static readonly int ComicSizeDefault = DefaultAppSettingsManager.DefaultComicSize;

        /// <summary>
        /// Public accessor for comic page size setting. When not set in User settings, the default value is read from Settings.json
        /// The comic page size is user configurable. This setting is only used when retrieving the comic pages from the API. Zooming is handled by the Photo browser. A higher value results in higher resolution.
        /// </summary>
        public static int ComicSize
        {
            get => AppSettings.GetValueOrDefault(nameof(ComicSize), ComicSizeDefault);
            set => AppSettings.AddOrUpdateValue(nameof(ComicSize), value);
        }

        /// <summary>
        /// Default array for Comic Access history. Defined within this class. Placeholder for empty history.
        /// </summary>
        private static readonly string HistoryDefault = "[]";

        /// <summary>
        /// Public accessor for Comic Access history.
        ///  History list of Comic objects, deserialized to JSON and stored in this class.
        /// </summary>
        public static string History
        {
            get => AppSettings.GetValueOrDefault(nameof(History), HistoryDefault);
            set => AppSettings.AddOrUpdateValue(nameof(History), value);
        }

        /// <summary>
        /// Default array for Comic favorites. Defined within this class. Placeholder for empty Favorites list.
        /// </summary>
        private static readonly string FavoritesDefault = @"{""favorite"":""false""}";

        /// <summary>
        /// Public accessor for Comic Favorite list. 
        /// The list is a key-value pair, deserialized to JSON and stored in this class.
        /// </summary>
        public static string Favorites
        {
            get => AppSettings.GetValueOrDefault(nameof(Favorites), FavoritesDefault);
            set => AppSettings.AddOrUpdateValue(nameof(Favorites), value);
        }

        /// <summary>
        /// Default array for Comic bookmarks. Defined within this class. Placeholder for emtpy bookmark list.
        /// </summary>
        private static readonly string BookMarkDefault = @"{""bookmark"":""0""}";

        /// <summary>
        /// Public accessor for Bookmark list.
        /// The list is a key-value pair, deserialized to JSON and stored in this class.
        /// </summary>
        public static string BookMarks
        {
            get => AppSettings.GetValueOrDefault(nameof(BookMarks), BookMarkDefault);
            set => AppSettings.AddOrUpdateValue(nameof(BookMarks), value);
        }
        #endregion
    }
}
