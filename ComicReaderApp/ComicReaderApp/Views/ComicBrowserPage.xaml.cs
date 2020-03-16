using ComicReaderApp.Data;
using ComicReaderApp.Models;
using ComicReaderApp.ViewModels;
using Stormlion.PhotoBrowser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ComicReaderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComicBrowserPage : ContentPage
    {
        public ComicBrowserPage(ComicListItemModel comic)
        {
            InitializeComponent();
            BindingContext = new ComicBrowserViewModel(comic);

            List<Photo> ComicPages = new List<Photo>();
            for (int page = 0; page <= comic.TotalPages; page++)
            {
                Photo photopage = new Photo();
                photopage.URL = $"{UserSettings.ApiLocation}?file={comic.Path}&page={page}&size={UserSettings.ComicSize}";
                photopage.Title = $"{comic.Title}-Page {page}";
                ComicPages.Add(photopage);
            }
            new PhotoBrowser
            {
                Photos = ComicPages,
                ActionButtonPressed = (index) =>
                {
                    Debug.WriteLine($"Clicked {index}");
                },

                BackgroundColor = Color.Black,
                DidDisplayPhoto = (index) =>
                {
                    Debug.WriteLine($"Selection changed: {index}");
                },

                Android_ContainerPaddingPx = 20,
                iOS_ZoomPhotosToFill = false
            }.Show();
        }
    }
}