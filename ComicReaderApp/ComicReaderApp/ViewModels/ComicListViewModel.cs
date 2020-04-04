﻿using ComicReaderApp.Data;
using ComicReaderApp.Models;
using Stormlion.PhotoBrowser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;
using Plugin.Toast;
using Plugin.Toast.Abstractions;

namespace ComicReaderApp.ViewModels
{
    class ComicListViewModel : INotifyPropertyChanged
    {
        ToastLength toastLength = ToastLength.Short;
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
                        if (!history.Items.Contains(comic))
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