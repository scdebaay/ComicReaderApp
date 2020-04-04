﻿using ComicReaderApp.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicReaderApp.Models
{
    public class ComicHistoryModel<ComicListItemModel> : IList<Models.ComicListItemModel>
    {        
        public Models.ComicListItemModel this[int index]
        {
            get { return Items[index]; }
            set { Items[index] = value; }
        }

        public List<Models.ComicListItemModel> Items { get; set; } = new List<Models.ComicListItemModel>();

        public int Count { get { return Items.Count; } }
        public bool IsReadOnly { get { return false; } }

        public void Add(Models.ComicListItemModel item)
        {
            Items.Add(item);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public string JSONResult
        {
            get { return JsonConvert.SerializeObject(this); }
        }

        public void LoadHistory()
        {
            JArray historyArray = JsonConvert.DeserializeObject<JArray>(UserSettings.History);
            if (historyArray.HasValues)
            {
                foreach (var histcomic in historyArray.Children())
                {
                    Models.ComicListItemModel comic = new Models.ComicListItemModel((string)histcomic["Path"], ((string)histcomic["Title"]), histcomic["TotalPages"].ToObject<int>());
                    Items.Add(comic);
                }
            }


        }

        public bool Contains(Models.ComicListItemModel item)
        {
            if (Items.Contains(item))
            { return true; }
            else
            {
                return false;
            }
        }

        public void CopyTo(Models.ComicListItemModel[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Models.ComicListItemModel> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public int IndexOf(Models.ComicListItemModel item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, Models.ComicListItemModel item)
        {
            Items.Insert(index, item);
        }

        public bool Remove(Models.ComicListItemModel item)
        {
            return Items.Remove(item);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public async Task<List<Models.ComicListItemModel>> GetRangeAsync(int index, int count)
        {
            List<Models.ComicListItemModel> result = new List<Models.ComicListItemModel>();
            if (this.Count < count)
            {
                count = this.Count;
            }
            for (int i = index; i < (index+count); i++)
            {
                await Task.Run(() => result.Add(this.Items[i]));
            }
            return result;
        }
    }
}
