using ComicReaderApp.Data;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace ComicReaderApp.Models
{
    public class ComicListItemModel : IEquatable<ComicListItemModel>, INotifyPropertyChanged
    {
        public ComicListItemModel()
        {
            Path = "";
            Title = "Comic not found";
            ThumbUrl = $"{UserSettings.ApiLocation}?file=NotFound&page=0&size=100";
        }

        public ComicListItemModel(string path, string name, int totalpages)
        {
            Path = path;
            Title = name;
            ThumbUrl = $"{UserSettings.ApiLocation}?file={Path}&page=0&size=100";
            TotalPages = totalpages;            
            if (ComicFavoriteStore.Contains(Title))
            {
                Favorite = true;
            }
        }

        public string Path { get; private set; }

        public string ThumbUrl { get; set; }

        public string Title { get; set; }

        public int TotalPages { get; set; }

        public bool Favorite { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Equals(ComicListItemModel other)
        {
            if (other == null)
                return false;

            if (this.Title == other.Title)
                return true;
            else
                return false;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [JsonIgnore]
        public ComicListItemModel SelectedItem
        {
            get { return this; }
        }

        [JsonIgnore]
        public ICommand OnSwitchFavorite
        {
            get
            {
                return new Command((ImageButtonTapped) =>
                {
                    ComicListItemModel comic = ImageButtonTapped as ComicListItemModel;
                    if (ComicFavoriteStore.Contains(comic.Title))
                    {
                        comic.Favorite = false;
                        ComicFavoriteStore.Remove(comic.Title);
                        OnPropertyChanged(nameof(Favorite));
                    }
                    else
                    {
                        comic.Favorite = true;
                        ComicFavoriteStore.Set(comic.Title, true);
                        OnPropertyChanged(nameof(Favorite));
                    }
                    ComicFavoriteStore.SaveFavorites();
                });
            }
        }
    }
}
