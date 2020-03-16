using ComicReaderApp.Data;
using ComicReaderApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ComicReaderApp.ViewModels
{
    public class ComicBrowserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _thumbUrl;
        public string ThumbUrl {
            get
            {
                return _thumbUrl;
            }
            set
            {
                _thumbUrl = value;
                OnPropertyChanged(nameof(ThumbUrl));
            }
        }
        private string _comicTitle;
        public string ComicTitle {
            get
            {
                return _comicTitle;
            }
            set
            {
                _comicTitle = value;
                OnPropertyChanged(nameof(ComicTitle));
            }
        }

        public ComicBrowserViewModel(ComicListItemModel comic)
        {
            ThumbUrl = $"{UserSettings.ApiLocation}?file={comic.Path}&page=0&size={UserSettings.ComicSize}";
            ComicTitle = comic.Title;
        }
    }
}
