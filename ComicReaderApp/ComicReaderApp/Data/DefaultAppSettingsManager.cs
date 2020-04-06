using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace ComicReaderApp.Data
{
    public class DefaultAppSettingsManager
    {
        #region private fields
        private static DefaultAppSettingsManager _instance;
        readonly JObject _settings;

        private const string Namespace = "ComicReaderApp.Data";
        private const string FileName = "Settings.json";
        #endregion

        /// <summary>
        /// DefaultAppSettingsManager constructor. Instantiates manager to _instance field. Tries to read Settings.json.
        /// Stores resulting JObject in readonly _settings field
        /// </summary>
        private DefaultAppSettingsManager()
        {
            try
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(DefaultAppSettingsManager)).Assembly;
                var stream = assembly.GetManifestResourceStream($"{Namespace}.{FileName}");
                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    _settings = JObject.Parse(json);
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Unable to load settings file");
            }
        }

        #region instance
        /// <summary>
        /// Public accessor to instantiate manager object. When requested, instantiates class and returns the _instance field.
        /// </summary>
        public static DefaultAppSettingsManager Settings
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DefaultAppSettingsManager();
                }

                return _instance;
            }
        }
        #endregion

        #region public settings
        /// <summary>
        /// Public accessor for generic, non-defined Settings.
        /// </summary>
        /// <param name="name">Name of the setting to access. Tries to find setting of "name" in the Settings class. If not found, returns an empty string</param>
        /// <returns></returns>
        public string this[string name]
        {
            get
            {
                try
                {
                    var path = name.Split(':');

                    JToken node = _settings[path[0]];
                    for (int index = 1; index < path.Length; index++)
                    {
                        node = node[path[index]];
                    }

                    return node.ToString();
                }
                catch (Exception)
                {
                    Debug.WriteLine($"Unable to retrieve setting '{name}'");
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Public accessor for API location setting. User changes to this setting are stored in the UserSetting class.
        /// </summary>
        public static string ApiLocation
        {
            get { return Settings["apiLocation"]; }
        }

        /// <summary>
        /// Public accessor for page limit setting. User changes to this setting are stored in the UserSetting class. Accessed setting converted to int
        /// </summary>
        public static int PageLimit
        {
            get { return int.Parse(Settings["pageLimit"]); }
        }

        /// <summary>
        /// Public accessor for default comic size setting. User changes to this setting are stored in the UserSetting class. Accessed setting converted to int
        /// </summary>
        public static int DefaultComicSize
        {
            get { return int.Parse(Settings["defaultComicSize"]); }
        }
        #endregion
    }
}