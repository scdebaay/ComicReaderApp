using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ComicReaderApp.Models;
using System.Collections.ObjectModel;
using ComicReaderApp.ViewModels;

namespace ComicReaderApp
{
    public partial class ComicListViewPage : ContentPage
    {
        public ComicListViewPage()
        {
            InitializeComponent();

            BindingContext = new ComicListViewModel();
        }
    }
}
