using ComicReaderApp.Data;
using ComicReaderApp.Models;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using Stormlion.PhotoBrowser;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace ComicReaderApp.ViewModels
{
    /// <summary>
    /// Comic List viewmodel. VM for Initial page, Comic List. Takes care of infinite scroll and Comic browsing, using Photo Browser.
    /// </summary>
    class ComicListViewModel : INotifyPropertyChanged
    {
        #region Readonly fields
        /// <summary>
        /// Toast length initialization. Toast being used to confirm bookmark set.
        /// </summary>
        readonly ToastLength toastLength = ToastLength.Short;
        
        /// <summary>
        /// Instantiate API call service object. This handles API calls to load list of available Comic List Items.
        /// </summary>
        readonly ComicApiCallService _comicApiCallService = new ComicApiCallService();
        #endregion
        
        /// <summary>
        /// Public accessor for InfiniteScrollCollection, Items. Used by Comic Listview page to display available Comic List items.
        /// </summary>
        public InfiniteScrollCollection<ComicListItemModel> Items { get; }

        #region Private fields
        /// <summary>
        /// Private bool represents List lodaing status.
        /// </summary>
        private bool _isLoadingMore;
        
        /// <summary>
        /// Private int, represents the number of available Comic List items from the API
        /// </summary>
        private int _totalComics { get; set; }
        private string _searchText;
        #endregion

        #region Public fields
        /// <summary>
        /// Public accessor for _isLoadingMore boolean field. Raises a property changed event when set.
        /// </summary>
        public bool IsLoadingMore
        {
            get
            {
                return _isLoadingMore;
            }
            set
            {
                _isLoadingMore = value;
                OnPropertyChanged(nameof(IsLoadingMore));
            }
        }        

        public string SearchText
        {
            get { return _searchText; }
            set 
            { 
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for Comic List ViewModel. Initiates InfiniteScrollCollection of Comic List items, calls API Call service to populate list.
        /// Detects when list is scrolled to the end and then calls for more items.
        /// </summary>
        public ComicListViewModel()
        {
            Items = new InfiniteScrollCollection<ComicListItemModel>
            {
                OnLoadMore = async () =>
                {
                    IsLoadingMore = true;
                    var page = (Items.Count / UserSettings.PageLimit);
                    FolderModel apiCallResult = new FolderModel();
                    if (string.IsNullOrEmpty(SearchText))                        
                    {
                        apiCallResult = await _comicApiCallService.GetFolderListAsync(page);
                    }
                    else 
                    {
                        apiCallResult = await _comicApiCallService.GetFolderListAsync(SearchText, page);
                    }
                    _totalComics = apiCallResult.AvailableFiles;
                    InfiniteScrollCollection<ComicListItemModel> items = new InfiniteScrollCollection<ComicListItemModel>();
                    foreach (var _comic in apiCallResult.Files)
                    {
                        items.Add(_comic);
                    }

                    IsLoadingMore = false;
                    return items;
                },
                OnCanLoadMore = () =>
                {
                    return Items.Count < _totalComics;
                }
            };
            Items.LoadMoreAsync();
        }
        #endregion

        #region Property Helpers
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private object GetResourceValue(string keyName)
        {
            // Search all dictionaries
            if (Application.Current.Resources.TryGetValue(keyName, out var retVal)) { }
            return retVal;
        }
        #endregion

        #region Start comic reading
        /// <summary>
        /// Public Command to handle Comic List item tapped (TappedItemArgs)
        /// Checks ComicBookmarkStore to see whether a page in the tapped comic was bookmarked. If true, starts browsing at that page.
        /// Checks ComicBookHistoryStore to see whether the comic was opened before. If not, then adds comic to history list.
        /// From the Comic browser, when the action button is pressed, a bookmark is saved at the current page.
        /// </summary>
        public ICommand ItemTappedCommand
        {
            get
            {
                return new Command((TappedltemArgs) =>
                {
                    ComicListItemModel comic = TappedltemArgs as ComicListItemModel;
                    #region Bookmarks setup
                    int bookMark = 0;
                    if (ComicBookmarkStore.Contains(comic.Title))
                    {
                        bookMark = ComicBookmarkStore.Get(comic.Title);
                    }
                    #endregion
                    #region History
                    ComicHistoryModel<ComicListItemModel> history = new ComicHistoryModel<ComicListItemModel>();
                    if (UserSettings.History == "[]")
                    {
                        history.Add(comic);
                    }
                    else
                    {
                        history.LoadHistory();
                        if (!history.Contains(comic))
                        {
                            history.Add(comic);
                        }
                    }
                    UserSettings.History = history.JSONResult;
                    #endregion
                    #region ComicBrowser
                    List<Photo> ComicPages = new List<Photo>();
                    for (int page = 0; page <= comic.TotalPages; page++)
                    {
                        Photo photopage = new Photo
                        {
                            URL = $"{UserSettings.ApiLocation}comic{comic.Path}/{page}?size={UserSettings.ComicSize}",
                            Title = $"{comic.Title}-Page {page}"
                        };
                        ComicPages.Add(photopage);
                    }
                    new PhotoBrowser
                    {
                        Photos = ComicPages,
                        ActionButtonPressed = (index) =>
                        {
                            ComicBookmarkStore.Set(comic.Title, index);
                            ComicBookmarkStore.SaveBookmarks();
                            CrossToastPopUp.Current.ShowToastSuccess("Bookmark saved", toastLength);
                        },
                        StartIndex = bookMark,
                        BackgroundColor = (Color)GetResourceValue("HeaderBackGroundColour"),
                        DidDisplayPhoto = (index) =>
                        {
                            //Debug.WriteLine($"Selection changed: {index}");
                        },
                        Android_ContainerPaddingPx = 20,
                        iOS_ZoomPhotosToFill = false
                    }.Show();
                    #endregion
                });
            }
        }
        #endregion
    }
}