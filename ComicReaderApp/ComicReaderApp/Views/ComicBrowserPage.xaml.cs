using ComicReaderApp.Models;
using ComicReaderApp.ViewModels;
using System;
using System.Collections.Generic;
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
		}
	}
}