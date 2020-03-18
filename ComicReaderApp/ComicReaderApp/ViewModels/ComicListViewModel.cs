using ComicReaderApp.Data;
using ComicReaderApp.Models;
using ComicReaderApp.Views;
using Stormlion.PhotoBrowser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;
using Xamarin.Forms.Xaml;

namespace ComicReaderApp.ViewModels
{
    class ComicListViewModel : INotifyPropertyChanged
    {
        readonly ComicApiCallService _comicApiCallService = new ComicApiCallService();

        #region Scrollview
        public InfiniteScrollCollection<ComicListItemModel> Items { get; }

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

        public ComicListViewModel(INavigation navigation)
        {
            _navigation = navigation;
            Items = new InfiniteScrollCollection<ComicListItemModel>
            {
                OnLoadMore = async () =>
                {
                    IsLoadingMore = true;
                    var page = (Items.Count / UserSettings.PageLimit) + 1;
                    var apiCallResult = await _comicApiCallService.GetFolderListAsync(page);
                    TotalComics = apiCallResult.AvailableFiles;
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
                            //Debug.WriteLine($"Clicked {index}");
                        },

                        BackgroundColor = Color.Black,
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