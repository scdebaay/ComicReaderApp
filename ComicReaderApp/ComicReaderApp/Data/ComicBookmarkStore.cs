using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using ComicReaderApp.Data;
using System.Text;

namespace ComicReaderApp.Data
{
    /// <summary>
    /// Comic Bookmark store. Implements a dictionary with Comic title and int, denoting the comic Title and the page to which a bookmark was set.
    /// </summary>
    public class ComicBookmarkStore
    {
        #region private fields
        /// <summary>
        /// Private backing field of type dictionary for Bookmark storage.
        /// </summary>
        private static Dictionary<string, int> items { get; set; } = new Dictionary<string, int>();
        #endregion

        #region public methods
        /// <summary>
        /// Setter for Bookmark.
        /// </summary>
        /// <param name="key">String, name of the comic of which a page is boomarked</param>
        /// <param name="value">Int, page fo the comic, the bookmark is inserted at</param>
        public static void Set(string key, int value)
        {
            if (items.ContainsKey(key))
            {
                items[key] = value;
            }
            else
            {
                items.Add(key, value);
            }
        }
        
        /// <summary>
        /// Getter for Bookmark
        /// </summary>
        /// <param name="key">Title of a comic</param>
        /// <returns>int of the page that was boomarked for the requested comic</returns>
        public static int Get(string key)
        {
            int  result = 0;
            if (items.ContainsKey(key))
            {
                result = items[key];
            }
            return result;
        }
        
        /// <summary>
        /// Check whether the dictionary contains a certain comic
        /// </summary>
        /// <param name="key">Title of a comic</param>
        /// <returns>true or false</returns>
        public static bool Contains(string key)
        {
            if (items.ContainsKey(key))
            { return true; }
            return false;
        }

        /// <summary>
        /// Clears the Bookamrk store. Default Bookmark remains.
        /// </summary>
        public static void Clear()
        {
            items.Clear();
        }

        /// <summary>
        /// Serializes Bookmark to UserSettings class, Bookmarks field as a JSON string.
        /// </summary>
        public static void SaveBookmarks() 
        {
            UserSettings.BookMarks = JsonConvert.SerializeObject(items);
        }

        /// <summary>
        /// Reads the contents of UserSettings.Bookmarks field into this class.
        /// </summary>
        public static void LoadBookmarks()
        {
            items = JsonConvert.DeserializeObject<Dictionary<string, int>>(UserSettings.BookMarks);
        }
        #endregion
    }
}
