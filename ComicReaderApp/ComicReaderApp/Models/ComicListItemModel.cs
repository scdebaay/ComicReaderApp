using ComicReaderApp.Data;

namespace ComicReaderApp.Models
{
    public class ComicListItemModel
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
        }

        public string Path { get; private set; }

        public string ThumbUrl { get; set; }

        public string Title { get; set; }

        public int TotalPages { get; set; }
    }
}
