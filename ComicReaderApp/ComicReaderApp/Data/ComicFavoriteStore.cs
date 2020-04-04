using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using ComicReaderApp.Data;
using System.Text;

namespace ComicReaderApp.Data
{
    public class ComicFavoriteStore
    {
        private static Dictionary<string, bool> items { get; set; } = new Dictionary<string, bool>();

        public static int Count
        {
            get { return items.Count; }
        }

        public static void Set(string key, bool value)
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

        public static bool Get(string key)
        {
            bool  result = false;
            if (items.ContainsKey(key))
            {
                result = items[key];
            }
            return result;
        }

        public static void Remove(string key)
        {
            items.Remove(key);
        }

        public static bool Contains(string key)
        {
            if (items.ContainsKey(key))
            { return true; }
            return false;
        }

        public static void Clear()
        {
            items.Clear();
        }

        public static void SaveFavorites() 
        {
            UserSettings.Favorites = JsonConvert.SerializeObject(items);
        }        

        public static void LoadFavorites()
        {
            items = JsonConvert.DeserializeObject<Dictionary<string, bool>>(UserSettings.Favorites);
        }
    }
}
