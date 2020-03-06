using ComicReaderApp.Data;
using ComicReaderApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace ComicReaderApp.ViewModels
{
    class ComicListViewModel : INotifyPropertyChanged
    {
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

        readonly ComicApiCallService _comicApiCallService = new ComicApiCallService();

        public int TotalComics { get; private set; }

        public ComicListViewModel()
        {
            Items = new InfiniteScrollCollection<ComicListItemModel>
            {
                OnLoadMore = async () =>
                {
                    IsLoadingMore = true;
                    var page = (Items.Count / AppSettingsManager.PageLimit) + 1;
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


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string alertMessage;

        public string AlertMessage
        {
            get { return alertMessage; }
            set { alertMessage = value;
                OnPropertyChanged();
            }

        }

        public ICommand ItemTappedCommand
        {
            get
            {
                return new Command((TappedltemArgs) =>
                {
                    string ItemTitle;
                    ComicListItemModel comic = TappedltemArgs as ComicListItemModel;
                    ItemTitle = comic.Title;
                    AlertMessage = ItemTitle;
                });
            }
        }
    }
}