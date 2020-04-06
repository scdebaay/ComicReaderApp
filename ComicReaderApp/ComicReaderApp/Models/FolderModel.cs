using ComicReaderApp.Data;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ComicReaderApp.Models
{
    /// <summary>
    /// Intermediate class to deserialize Comic Folder list into list of ComicListItems and Folder properties
    /// </summary>
    public class FolderModel
    {
        #region private fields
        /// <summary>
        /// Readonly backing field for ComicListItemModels.
        /// </summary>
        readonly List<ComicListItemModel> files = new List<ComicListItemModel>();
        #endregion

        #region instance

        /// <summary>
        /// Default constructor, creates a list of one default ComicListItem and stores this in files field.
        /// </summary>
        public FolderModel()
        {
            Name = "Folder not defined.";
            AvailableFiles = 1;
            TotalPages = 1;
            CurrentPage = 1;
            ComicListItemModel emtpyComic = new ComicListItemModel("", "File not found", 0);
            files.Add(emtpyComic);
        }
        
        /// <summary>
        /// Constructor used to parse API result into e Folder object with folder details and a list of ComicListItems
        /// </summary>
        /// <param name="folder">JObject containing folder and array of ComicListItems to be parsed into files field</param>
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
                    ComicListItemModel comic = new ComicListItemModel((string)comicFile["@path"], ((string)comicFile["@name"]).Substring(0, ((string)comicFile["@name"]).Length - 4), comicFile["@totalpages"].ToObject<int>());
                    if (ComicFavoriteStore.Contains(comic.Title))
                    {
                        comic.Favorite = true;
                    }
                    files.Add(comic);
                }
            }
            else
            {
                ComicListItemModel comic = new ComicListItemModel();
                files.Add(comic);
            }

        }
        #endregion

        #region public accessors

        /// <summary>
        /// Public accessor for folder Name. Denotes Name property of the folder object.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Public accessor for AvailableFiles. Denotes the total amount of files available from the API
        /// </summary>
        public int AvailableFiles { get; private set; }

        /// <summary>
        /// Public accessor for TotalPages. Denotes the amount of pages the AvailableFiles are divided in using the pageLimit parameter in the API call.
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// Public accessor for CurrentPage. Denotes the page retrieved from the API, determined by the page parameter in the API call.
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// Public accessor for File list. Denotes the currently retrieved ComicFiles from the API.
        /// </summary>
        public List<ComicListItemModel> Files { get {
                return files;
            } private set { } }
        #endregion
    }
}
