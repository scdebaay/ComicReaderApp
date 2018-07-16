using ComicReaderApp.Models;
using ComicReaderApp.Behaviors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Extended;
using System.Windows.Input;
using System.Diagnostics;
using Xamarin.Forms;

namespace ComicReaderApp.ViewModels
{
    class ComicListViewModel : INotifyPropertyChanged
    {
        public InfiniteScrollCollection<ComicListItem> Items { get; }
        
        bool _isLoadingMore;
        bool IsLoadingMore
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

        public ComicListViewModel()
        {
            Items = new InfiniteScrollCollection<ComicListItem>
            {
                OnLoadMore = async () =>
                {
                    IsLoadingMore = true;

                    var items = GetItems(false);
                    //Call your Web API next items page.
                    await Task.Delay(1200);

                    IsLoadingMore = false;
                    return items;
                }
            };
            Items.LoadMoreAsync();
            //ItemTappedCommand = new Command();
        }

        InfiniteScrollCollection<ComicListItem> GetItems(bool clearList)
        {
            InfiniteScrollCollection<ComicListItem> items;
            if (clearList || Items == null)
            {
                items = new InfiniteScrollCollection<ComicListItem>();
            }
            else
            {
                items = new InfiniteScrollCollection<ComicListItem>(Items);
            }

            for (int i = 0; i < 20; i++)
            {
                items.Add(new ComicListItem { ThumbUrl = Data.ComicList.Titles[i].ThumbUrl, Title = Data.ComicList.Titles[i].Title });
            }

            return items;
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
                    ComicListItem comic = TappedltemArgs as ComicListItem;
                    ItemTitle = comic.Title;
                    AlertMessage = ItemTitle;
                });
            }
        }
    }
}