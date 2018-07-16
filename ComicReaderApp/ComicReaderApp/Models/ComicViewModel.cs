using ComicReaderApp.Data;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ComicReaderApp
{
    class ComicViewModel : INotifyPropertyChanged

    {
        bool isLoading;
        public ComicViewModel()
        {
            LoadItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void LoadItems()
        {
            isLoading = true;
            await AsyncLoadingComics();
            isLoading = false;
            return;
        }

        private Task AsyncLoadingComics()
        {
            foreach (var ComicListItem in Data.ComicList.Titles)
            {
                //ComicsDisplayed.Add(ComicListItem);
            }
            return null;
        }

        private async void OnLoadNewItems(object sender, ItemVisibilityEventArgs e)
        {
            //ComicListItem Caller = e.Item as ComicListItem;
            //if (ComicsDisplayed.Last().Title != Caller.Title)
            { return; }

            //hit bottom!
            //if (ComicsDisplayed.Last().Title == Caller.Title)
            {
                //await LoadItems();
            }
        }
    }
}