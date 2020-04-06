using Newtonsoft.Json;
using System.Collections.Generic;

namespace ComicReaderApp.Data
{
    /// <summary>
    /// Comic Favorite store. Implements a dictionary with Comic title and boolean, denoting favorite=true or false
    /// </summary>
    public class ComicFavoriteStore
    {
        #region private fields
        /// <summary>
        /// Private backing field of type dictionary for Favorite storage.
        /// </summary>
        private static Dictionary<string, bool> Items { get; set; } = new Dictionary<string, bool>();
        #endregion

        #region public accessors
        /// <summary>
        /// Public accessor to the amount of favorites in the store. ther will always be one default favorite.
        /// </summary>
        public static int Count
        {
            get { return Items.Count -1; }
        }
        #endregion

        #region public methods
        /// <summary>
        /// Setter for Favorite.
        /// </summary>
        /// <param name="key">String, name of the comic that is set to favorite</param>
        /// <param name="value">Boolean, is comis is favorite or not</param>
        public static void Set(string key, bool value)
        {
            if (Items.ContainsKey(key))
            {
                Items[key] = value;
            }
            else
            {
                Items.Add(key, value);
            }
        }

        /// <summary>
        /// Getter for Favorite
        /// </summary>
        /// <param name="key">Title of a comic</param>
        /// <returns>true if comic is favorite or false if comic is not favorite</returns>
        public static bool Get(string key)
        {
            bool  result = false;
            if (Items.ContainsKey(key))
            {
                result = Items[key];
            }
            return result;
        }
        
        /// <summary>
        /// Remover for Favorite
        /// </summary>
        /// <param name="key">Title of a comic</param>
        /// <returns></returns>
        public static void Remove(string key)
        {
            Items.Remove(key);
        }

        /// <summary>
        /// Check whether the dictionary contains a certain comic
        /// </summary>
        /// <param name="key">Title of a comic</param>
        /// <returns>true or false</returns>
        public static bool Contains(string key)
        {
            if (Items.ContainsKey(key))
            { return true; }
            return false;
        }

        /// <summary>
        /// Clears the Favorite store. Default favorite remains.
        /// </summary>
        public static void Clear()
        {
            Items.Clear();
        }

        /// <summary>
        /// Serializes Favorites to UserSettings class, Favorites field as a JSON string.
        /// </summary>
        public static void SaveFavorites() 
        {
            UserSettings.Favorites = JsonConvert.SerializeObject(Items);
        }

        /// <summary>
        /// Reads the contents of UserSettings.Favorites field into this class.
        /// </summary>
        public static void LoadFavorites()
        {
            Items = JsonConvert.DeserializeObject<Dictionary<string, bool>>(UserSettings.Favorites);
        }
        #endregion
    }
}
