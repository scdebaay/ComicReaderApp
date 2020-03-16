using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ComicReaderApp.Models
{
    public class FolderModel
    {
        List<ComicListItemModel> files = new List<ComicListItemModel>();

        public FolderModel()
        {
            Name = "Folder not defined.";
            AvailableFiles = 1;
            TotalPages = 1;
            CurrentPage = 1;
            ComicListItemModel emtpyComic = new ComicListItemModel("", "File not found");
            files.Add(emtpyComic);
        }

        public FolderModel(JObject folder)
        {
            Name = (string)folder["folder"]["@name"];

            AvailableFiles = string.IsNullOrEmpty((string)folder["folder"]["@files"]) ? 1 : int.TryParse((string)folder["folder"]["@files"],out int testAFiles) ? AvailableFiles = testAFiles : AvailableFiles = 1;

            TotalPages = string.IsNullOrEmpty((string)folder["folder"]["@totalPages"]) ? 1 : int.TryParse((string)folder["folder"]["@totalPages"], out int testTPages) ? TotalPages = testTPages : TotalPages = 1;

            CurrentPage = string.IsNullOrEmpty((string)folder["folder"]["@currentPage"]) ? 1 : int.TryParse((string)folder["folder"]["@currentPage"], out int testCPage) ? CurrentPage = testCPage : CurrentPage = 1;

            JArray fileArray = (JArray)folder["folder"]["file"];

            if (fileArray.HasValues)
            {
                foreach (var comicFile in fileArray.Children())
                {
                    ComicListItemModel comic = new ComicListItemModel((string)comicFile["@path"], ((string)comicFile["@name"]).Substring(0, ((string)comicFile["@name"]).Length - 4));
                    files.Add(comic);
                }
            }
            else
            {
                ComicListItemModel comic = new ComicListItemModel();
                files.Add(comic);
            }

        }

        public string Name { get; private set; }
        public int AvailableFiles { get; private set; }
        public int TotalPages { get; private set; }
        public int CurrentPage { get; private set; }
        public List<ComicListItemModel> Files { get {
                return files;
            } private set { } }
    }
}
