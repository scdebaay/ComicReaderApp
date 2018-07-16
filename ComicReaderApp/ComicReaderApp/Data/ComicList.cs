using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using ComicReaderApp.Models;
using Xamarin.Forms;

namespace ComicReaderApp.Data
{
    class ComicList
    {
        #region ComicList
        public static ObservableCollection<ComicListItem> Titles = new ObservableCollection<ComicListItem>
        {
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-01.cbr&page=0&size=100", Title = "XIII-01" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-02.cbr&page=0&size=100", Title = "XIII-02" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-03.cbr&page=0&size=100", Title = "XIII-03" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-04.cbr&page=0&size=100", Title = "XIII-04" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-05.cbr&page=0&size=100", Title = "XIII-05" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-06.cbr&page=0&size=100", Title = "XIII-06" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-07.cbr&page=0&size=100", Title = "XIII-07" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-08.cbr&page=0&size=100", Title = "XIII-08" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-09.cbr&page=0&size=100", Title = "XIII-09" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-10.cbr&page=0&size=100", Title = "XIII-10" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-11.cbr&page=0&size=100", Title = "XIII-11" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-12.cbr&page=0&size=100", Title = "XIII-12" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-13.cbr&page=0&size=100", Title = "XIII-13" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-14.cbr&page=0&size=100", Title = "XIII-14" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-15.cbr&page=0&size=100", Title = "XIII-15" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-16.cbr&page=0&size=100", Title = "XIII-16" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-17.cbr&page=0&size=100", Title = "XIII-17" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-18.cbr&page=0&size=100", Title = "XIII-18" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-19.cbr&page=0&size=100", Title = "XIII-19" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\Vrije%20Vlucht\\Fanchon%2001.cbz&page=0&size=100", Title = "Fanchon 01" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-01.cbr&page=0&size=100", Title = "XIII-01" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-02.cbr&page=0&size=100", Title = "XIII-02" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-03.cbr&page=0&size=100", Title = "XIII-03" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-04.cbr&page=0&size=100", Title = "XIII-04" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-05.cbr&page=0&size=100", Title = "XIII-05" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-06.cbr&page=0&size=100", Title = "XIII-06" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-07.cbr&page=0&size=100", Title = "XIII-07" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-08.cbr&page=0&size=100", Title = "XIII-08" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-09.cbr&page=0&size=100", Title = "XIII-09" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-10.cbr&page=0&size=100", Title = "XIII-10" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-11.cbr&page=0&size=100", Title = "XIII-11" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-12.cbr&page=0&size=100", Title = "XIII-12" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-13.cbr&page=0&size=100", Title = "XIII-13" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-14.cbr&page=0&size=100", Title = "XIII-14" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-15.cbr&page=0&size=100", Title = "XIII-15" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-16.cbr&page=0&size=100", Title = "XIII-16" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-17.cbr&page=0&size=100", Title = "XIII-17" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-18.cbr&page=0&size=100", Title = "XIII-18" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\XIII\\XIII-19.cbr&page=0&size=100", Title = "XIII-19" },
            new ComicListItem { ThumbUrl = "https://www.de-baay.nl/ComicCloud/?file=\\Vrije%20Vlucht\\Fanchon%2001.cbz&page=0&size=100", Title = "Fanchon 01" },
        };
        #endregion
    }
}
