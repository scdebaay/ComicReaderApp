using ComicReaderApp.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicReaderApp.Models
{
    /// <summary>
    /// Custom collection class representing a list of comics that has been visisted by a user.
    /// </summary>
    /// <typeparam name="ComicListItemModel">The list should only contain ComicListItem objects</typeparam>
    public class ComicHistoryModel<ComicListItemModel> : IList<Models.ComicListItemModel>
    {
        #region private instance
        /// <summary>
        /// Private backing list for Comic History List. Initiates to new List of ComicListItems
        /// </summary>
        private List<Models.ComicListItemModel> Items { get; set; } = new List<Models.ComicListItemModel>();
        #endregion

        #region public accessors
        /// <summary>
        /// public accessor to specific Comic in the list Items by index.
        /// Gets and sets items at index.
        /// </summary>
        /// <param name="index">int, index to locate comic at</param>
        /// <returns></returns>
        public Models.ComicListItemModel this[int index]
        {
            get { return Items[index]; }
            set { Items[index] = value; }
        }

        /// <summary>
        /// Count of items in this list object.
        /// </summary>
        public int Count { get { return Items.Count; } }
        
        /// <summary>
        /// Readonly status of this object, defaults to false
        /// </summary>
        public bool IsReadOnly { get { return false; } }
        #endregion

        #region IList implementation        
        public void Add(Models.ComicListItemModel item)
        {
            Items.Add(item);
        }

        public void Clear()
        {
            Items.Clear();
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
        #endregion

        #region Custom accessors
        /// <summary>
        /// Public accessor to JSON representation of this object.
        /// </summary>
        public string JSONResult
        {
            get { return JsonConvert.SerializeObject(this); }
        }
        #endregion

        #region Public functions
        /// <summary>
        /// Public function to load JSON string representing the Users history into this object
        /// </summary>
        public void LoadHistory()
        {
            JArray historyArray = JsonConvert.DeserializeObject<JArray>(UserSettings.History);
            if (historyArray.HasValues)
            {
                Items.Clear();
                foreach (var histcomic in historyArray.Children())
                {
                    Models.ComicListItemModel comic = new Models.ComicListItemModel((string)histcomic["Path"], ((string)histcomic["Title"]), histcomic["TotalPages"].ToObject<int>());
                    Items.Add(comic);
                }
            }


        }
        
        /// <summary>
        /// Public function to check whether this object contains ComicListItem represented by item
        /// </summary>
        /// <param name="item">ComicListItem to check the list for.</param>
        /// <returns></returns>
        public bool Contains(Models.ComicListItemModel item)
        {
            if (Items.Contains(item))
            { return true; }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Public function to asynchronously retrieve the items in the History list for the current user.
        /// </summary>
        /// <param name="index">Index item to start retrieval from</param>
        /// <param name="count">Amount of items to retrieve.</param>
        /// <returns></returns>
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
        #endregion
    }
}
