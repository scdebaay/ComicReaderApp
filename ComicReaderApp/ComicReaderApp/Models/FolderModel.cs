using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ComicReaderApp.Models
{
    public class FolderModel
    {
        List<ComicListItemModel> files = new List<ComicListItemModel>();
        public FolderModel(JObject folder)
        {
            Name = (string)folder["folder"]["@name"];
            AvailableFiles = int.Parse((string)folder["folder"]["@files"]);
            TotalPages = int.Parse((string)folder["folder"]["@totalPages"]);
            CurrentPage = int.Parse((string)folder["folder"]["@currentPage"]);
            JArray fileArray = (JArray)folder["folder"]["file"];

            foreach (var comicFile in fileArray.Children())
            {
                ComicListItemModel comic = new ComicListItemModel((string)comicFile["@path"], (string)comicFile["@name"]);
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
