using ComicReaderApp.Data;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace ComicReaderApp.Models
{
    /// <summary>
    /// ComicListItemModel representing Comics in the folder list.
    /// </summary>
    public class ComicListItemModel : IEquatable<ComicListItemModel>, INotifyPropertyChanged
    {
        #region constructor        
        /// <summary>
        /// Default constructor, instantiates emtpy ComicListItem, functional, does not contain Comic Data.
        /// </summary>
        public ComicListItemModel()
        {
            Path = "";
            Title = "Comic not found";
            ThumbUrl = $"{UserSettings.ApiLocation}image/NotFound.png?size=100";
        }
        
        /// <summary>
        /// Constructor that instantiates valid ComicListItems to be used in the app.
        /// </summary>
        /// <param name="path">Relative path used to access the comic pages from the API, the file= parameter</param>
        /// <param name="name">Title of the comic</param>
        /// <param name="totalpages">Amount of pages in the comic</param>
        public ComicListItemModel(string path, string name, int totalpages)
        {
            Path = path;
            Title = name;
            ThumbUrl = $"{UserSettings.ApiLocation}comic{Path}/0?size=100";
            TotalPages = totalpages;            
            if (ComicFavoriteStore.Contains(Title))
            {
                Favorite = true;
            }
        }
        #endregion

        #region public accessors
        
        /// <summary>
        /// Public accessor to Path field. Denotes the relative path where the comic can be accessed. This is used in the API call as the file= parameter.
        /// </summary>
        public string Path { get; private set; }
        
        /// <summary>
        /// Public accessor to the ThumUrl field. Denotes the URL where the thumbnail can be accessed for display in the Comic List.
        /// </summary>
        public string ThumbUrl { get; private set; }

        /// <summary>
        /// Public accessor to Title field. Denotes the filename of the Comic, minus the extension.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Public accessor to TotalPages field. Denotes the number of pages in the Comic. Used to determine when to stop browsing.
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// Public accessor to Favorite field. Denotes whether this Comic is to be featured on the Favorites page.
        /// </summary>
        public bool Favorite { get; set; } = false;
        #endregion

        #region property helpers
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        
        #endregion

        #region reflection

        /// <summary>
        /// Implementation of the Equals function to be able to compare two comics. Equality is determined based on Comic Title.
        /// Used for the Contains function in the ComicHistoryModel
        /// </summary>
        /// <param name="other">ComicListItem to compare this instance with</param>
        /// <returns></returns>
        public bool Equals(ComicListItemModel other)
        {
            if (other == null)
                return false;

            if (this.Title == other.Title)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Public accessor to SelectedItem field. Used to bind to from the List view and set or unset the Favorite status. Passes the current object to the OnSwitchFavorite Command.
        /// Is JSON ignored because of recursuve loop when serializing and deserializing.
        /// </summary>
        [JsonIgnore]
        public ComicListItemModel SelectedItem
        {
            get { return this; }
        }
        #endregion
        
        #region commands        
        /// <summary>
        /// Public command to switch Favorite status for current comic. Uses OnPropertyChanged to update the favorite icon.
        /// Takes in the currently selected comic using the SelectedItem field.
        /// Writes out Favorite to the Favorite store and saves the store status.
        /// </summary>
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
        #endregion
    }
}
