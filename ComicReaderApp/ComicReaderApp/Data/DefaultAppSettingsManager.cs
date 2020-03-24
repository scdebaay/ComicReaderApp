using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace ComicReaderApp.Data
{
    public class DefaultAppSettingsManager
    {
        private static DefaultAppSettingsManager _instance;
        readonly JObject _settings;

        private const string Namespace = "ComicReaderApp.Data";
        private const string FileName = "Settings.json";

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

        public static string ApiLocation
        {
            get { return Settings["apiLocation"]; }
        }

        public static int PageLimit
        {
            get { return int.Parse(Settings["pageLimit"]); }
        }

        public static int DefaultComicSize
        {
            get { return int.Parse(Settings["defaultComicSize"]); }
        }
    }
}