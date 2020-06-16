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
    class ComicHistoryViewModel : INotifyPropertyChanged
    {
        #region Readonly fields
        /// <summary>
        /// Toast length initialization. Toast being used to confirm bookmark set.
        /// </summary>
        readonly ToastLength toastLength = ToastLength.Short;

        /// <summary>
        /// Instantiate ComicHistory object. This serves to load list of previously visited Comic List Items.
        /// </summary>
        private ComicHistoryModel<ComicListItemModel> history = new ComicHistoryModel<ComicListItemModel>();
        #endregion

        #region Public Functions
        /// <summary>
        /// Public Function to be bound to Clear History button in the History Listviewpage
        /// </summary>
        public void ClearHistory()
        {
            history.Clear();
            UserSettings.History = history.JSONResult;
        }
        #endregion

        /// <summary>
        /// Public accessor for InfiniteScrollCollection, Items. Used by History Listview page to display available Comic List items.
        /// </summary>
        public InfiniteScrollCollection<ComicListItemModel> Items { get; }

        #region Private fields
        /// <summary>
        /// Private bool represents List loading status.
        /// </summary>
        private bool _isLoadingMore;

        /// <summary>
        /// Private int, represents the number of available Comic List items from the users history
        /// </summary>
        private int TotalComics { get; set; }
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
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for History List ViewModel. Initiates InfiniteScrollCollection of Comic List items, calls Comic History object to populate list.
        /// Detects when list is scrolled to the end and then calls for more items.
        /// </summary>
        public ComicHistoryViewModel()
        {
            Items = new InfiniteScrollCollection<ComicListItemModel>
            {
                OnLoadMore = async () =>
                {   
                    history.LoadHistory();
                    IsLoadingMore = true;
                    var page = (Items.Count / UserSettings.PageLimit);
                    var startItem = (page * UserSettings.PageLimit);
                    var histLoadResult = await history.GetRangeAsync(startItem, UserSettings.PageLimit);
                    TotalComics = history.Count;
                    InfiniteScrollCollection<ComicListItemModel> items = new InfiniteScrollCollection<ComicListItemModel>();
                    foreach (var _comic in histLoadResult)
                    {
                        items.Add(_comic);
                    }
                    IsLoadingMore = false;
                    return items;
                },
                OnCanLoadMore = () =>
                {
                    return Items.Count < TotalComics;
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

        #region Start comic reading from history
        /// <summary>
        /// Public Command to handle History List item tapped (TappedItemArgs)
        /// Checks ComicBookmarkStore to see whether a page in the tapped comic was bookmarked. If true, starts browsing at that page.        
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
                });
            }
        }
        #endregion
        public ICommand RemoveComicCommand
        {
            get
            {
                return new Command((RemovedComic) =>
                {
                    ComicListItemModel comic = RemovedComic as ComicListItemModel;
                    ComicHistoryModel<ComicListItemModel> history = new ComicHistoryModel<ComicListItemModel>();
                    history.LoadHistory();
                    if (history.Contains(comic))
                    {
                        history.Remove(comic);
                        UserSettings.History = history.JSONResult;
                    }
                    Items.Clear();
                    Items.LoadMoreAsync();                    
                });
            }
        }
    }
}