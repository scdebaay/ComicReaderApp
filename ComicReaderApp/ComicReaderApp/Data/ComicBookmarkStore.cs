using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using ComicReaderApp.Data;
using System.Text;

namespace ComicReaderApp.Data
{
    public class ComicBookmarkStore
    {
        private static Dictionary<string, int> items { get; set; } = new Dictionary<string, int>();

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

        public static int Get(string key)
        {
            int  result = 0;
            if (items.ContainsKey(key))
            {
                result = items[key];
            }
            return result;
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

        public static void SaveBookmarks() 
        {
            UserSettings.BookMarks = JsonConvert.SerializeObject(items);
        }        

        public static void LoadBookmarks()
        {
            items = JsonConvert.DeserializeObject<Dictionary<string, int>>(UserSettings.BookMarks);
        }
    }
}
