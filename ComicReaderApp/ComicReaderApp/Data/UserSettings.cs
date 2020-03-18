﻿using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Threading;

namespace ComicReaderApp.Data
{
    class UserSettings
    {
        #region Instance

        private static ISettings AppSettings => CrossSettings.Current;

        #endregion

        #region Methods
        public static void ClearEverything()
        {
            AppSettings.Clear();
        }
        #endregion

        #region Public Settings
        private static readonly string ApiLocationDefault = DefaultAppSettingsManager.ApiLocation; //Default value for your property if the key-value pair has not been created yet

        public static string ApiLocation
        {
            get => AppSettings.GetValueOrDefault(nameof(ApiLocation), ApiLocationDefault);
            set => AppSettings.AddOrUpdateValue(nameof(ApiLocation), value);
        }

        private static readonly int PageLimitDefault = DefaultAppSettingsManager.PageLimit; //Default value for your property if the key-value pair has not been created yet

        public static int PageLimit
        {
            get => AppSettings.GetValueOrDefault(nameof(PageLimit), PageLimitDefault);
            set => AppSettings.AddOrUpdateValue(nameof(PageLimit), value);
        }

        private static readonly int ComicSizeDefault = DefaultAppSettingsManager.DefaultComicSize; //Default value for your property if the key-value pair has not been created yet

        public static int ComicSize
        {
            get => AppSettings.GetValueOrDefault(nameof(ComicSize), ComicSizeDefault);
            set => AppSettings.AddOrUpdateValue(nameof(ComicSize), value);
        }
        #endregion
    }
}