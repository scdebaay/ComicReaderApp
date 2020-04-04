using ComicReaderApp.Data;
using ComicReaderApp.Models;
using Stormlion.PhotoBrowser;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace ComicReaderApp.ViewModels
{
    class ComicFavoriteViewModel : INotifyPropertyChanged
    {
        public void ClearFavorites()
        {
            ComicFavoriteStore.Clear();
            ComicFavoriteStore.SaveFavorites();
        }

        #region Scrollview
        public InfiniteScrollCollection<ComicListItemModel> Items { get; }
        readonly ComicApiCallService _comicApiCallService = new ComicApiCallService();
        private bool _isLoadingMore;
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
        public int TotalComics { get; private set; }
        private int pageIterator { get; set; }

        public ComicFavoriteViewModel(INavigation navigation)
        {
            _navigation = navigation;
            Items = new InfiniteScrollCollection<ComicListItemModel>
            {
                OnLoadMore = async () =>
                {
                    IsLoadingMore = true;
                    var page = (pageIterator / UserSettings.PageLimit) + 1;
                    var apiCallResult = await _comicApiCallService.GetFolderListAsync(page);
                    TotalComics = ComicFavoriteStore.Count -1;
                    InfiniteScrollCollection<ComicListItemModel> items = new InfiniteScrollCollection<ComicListItemModel>();
                    pageIterator += apiCallResult.Files.Count;
                    foreach (var _comic in apiCallResult.Files)
                    {
                        if (_comic.Favorite)
                        {
                            items.Add(_comic);
                        }
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

        #region Navigation
        readonly INavigation _navigation;
        public ICommand ItemTappedCommand
        {
            get
            {
                return new Command((TappedltemArgs) =>
                {
                    ComicListItemModel comic = TappedltemArgs as ComicListItemModel;
                    ComicBookmarkStore.LoadBookmarks();
                    int bookMark = 0;
                    if (ComicBookmarkStore.Contains(comic.Title))
                    {
                        bookMark = ComicBookmarkStore.Get(comic.Title);
                    }
                    List<Photo> ComicPages = new List<Photo>();
                    for (int page = 0; page <= comic.TotalPages; page++)
                    {
                        Photo photopage = new Photo
                        {
                            URL = $"{UserSettings.ApiLocation}?file={comic.Path}&page={page}&size={UserSettings.ComicSize}",
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
    }
}