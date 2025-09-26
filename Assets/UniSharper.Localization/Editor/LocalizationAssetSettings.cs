// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using ReSharp.Extensions;
using ReSharp.Security.Cryptography;
using UniSharper;
using UniSharperEditor.Extensions;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor.Localization
{
    /// <summary>
    /// Class used to get and set the localization settings object. Implements the <see cref="SettingsScriptableObject{T}"/>
    /// </summary>
    /// <seealso cref="SettingsScriptableObject{T}"/>
    public class LocalizationAssetSettings : SettingsScriptableObject<LocalizationAssetSettings>
    {
        private const string LocalizationAssetsFolderName = "Locales";

        private const string LocalizationFolderName = "Localization";

        private static readonly string LocalizationFolder = PlayerPath.GetAssetPath(LocalizationFolderName);

        private static readonly string DefaultSettingsAssetPath = $"{LocalizationFolder}/{nameof(LocalizationAssetSettings)}.asset";

        private static readonly string TranslationFilePathPrefKeyFormat = $"{CryptoUtility.Md5HashEncrypt(Directory.GetCurrentDirectory(), null, false)}.{typeof(LocalizationAssetSettings).FullName}.translationFilePath";

        [ReadOnlyField]
        [SerializeField]
        private int localeRowIndex;

        [ReadOnlyField]
        [SerializeField]
        private string localizationAssetsPath = PathUtility.UnifyToAltDirectorySeparatorChar(Path.Combine(LocalizationFolder, LocalizationAssetsFolderName));

        [ReadOnlyField]
        [SerializeField]
        private string localizationScriptNamespace;

        [ReadOnlyField]
        [SerializeField]
        private string localizationScriptsStorePath = PathUtility.UnifyToAltDirectorySeparatorChar(PlayerPath.GetAssetPath(EditorEnvironment.DefaultScriptsFolderName));

        [ReadOnlyField]
        [SerializeField]
        private int translationKeyColumnIndex;

        [ReadOnlyField]
        [SerializeField]
        private int translationTextRowStartIndex = 1;

        [ReadOnlyField]
        [SerializeField]
        private Vector2Int translationTextColumnIndexRange = Vector2Int.one;

        [ReadOnlyField]
        [SerializeField]
        private Vector2Int styleColumnIndexRange = Vector2Int.one * 3;

        [SerializeField]
        private string[] targetLocales = Array.Empty<string>();

        [SerializeField]
        private string[] excludedLocales = Array.Empty<string>();

        internal static string TranslationFilePath
        {
            get
            {
                var key = string.Format(TranslationFilePathPrefKeyFormat, PlayerSettings.productName);
                return EditorPrefsUtility.GetString(key, string.Empty);
            }
            set
            {
                if (string.IsNullOrEmpty(value) || TranslationFilePath.Equals(value))
                    return;

                var key = string.Format(TranslationFilePathPrefKeyFormat, PlayerSettings.productName);
                EditorPrefsUtility.SetString(key, value);
            }
        }

        internal string LocalizationAssetsPath
        {
            get => localizationAssetsPath;
            set
            {
                if (string.IsNullOrEmpty(value) || localizationAssetsPath.Equals(value))
                    return;

                localizationAssetsPath = value;
                Save();
            }
        }

        internal string LocalizationScriptNamespace
        {
            get => localizationScriptNamespace;
            set
            {
                if (string.IsNullOrEmpty(value) || localizationScriptNamespace.Equals(value))
                    return;

                localizationScriptNamespace = value;
                Save();
            }
        }

        internal string LocalizationScriptsStorePath
        {
            get => localizationScriptsStorePath;
            set
            {
                if (string.IsNullOrEmpty(value) || localizationScriptsStorePath.Equals(value))
                    return;

                localizationScriptsStorePath = value;
                Save();
            }
        }

        internal int LocaleRowIndex
        {
            get => localeRowIndex;
            set
            {
                if (localeRowIndex.Equals(value))
                    return;

                localeRowIndex = value;
                Save();
            }
        }

        internal int TranslationKeyColumnIndex
        {
            get => translationKeyColumnIndex;
            set
            {
                if (translationKeyColumnIndex.Equals(value))
                    return;

                translationKeyColumnIndex = value;
                Save();
            }
        }

        internal int TranslationTextRowStartIndex
        {
            get => translationTextRowStartIndex;
            set
            {
                if (translationTextRowStartIndex.Equals(value))
                    return;

                translationTextRowStartIndex = value;
                Save();
            }
        }

        internal Vector2Int TranslationTextColumnIndexRange
        {
            get => translationTextColumnIndexRange;
            set
            {
                if (translationTextColumnIndexRange.Equals(value))
                    return;

                translationTextColumnIndexRange = value;
                Save();
            }
        }

        internal Vector2Int StyleColumnIndexRange
        {
            get => styleColumnIndexRange;
            set
            {
                if (styleColumnIndexRange.Equals(value))
                    return;

                styleColumnIndexRange = value;
                Save();
            }
        }

        internal static LocalizationAssetSettings Create()
        {
            LocalizationAssetSettings settings;

            if (File.Exists(DefaultSettingsAssetPath))
            {
                settings = Load();
            }
            else
            {
                settings = CreateInstance<LocalizationAssetSettings>();
                CreateLocalizationAssetsRootFolder();
                CreateLocalizationAssetsFolder(settings);
                CreateLocalizationScriptsStoreFolder(settings);
                AssetDatabase.CreateAsset(settings, DefaultSettingsAssetPath);
            }

            return settings;
        }

        internal static void CreateLocalizationAssetsFolder(LocalizationAssetSettings settings)
        {
            if (Directory.Exists(settings.LocalizationAssetsPath))
                return;

            Directory.CreateDirectory(settings.LocalizationAssetsPath);
            AssetDatabase.ImportAsset(settings.LocalizationAssetsPath);
        }

        internal static void CreateLocalizationAssetsRootFolder()
        {
            if (Directory.Exists(LocalizationFolder))
                return;

            AssetDatabase.CreateFolder(PlayerEnvironment.AssetsFolderName, LocalizationFolderName);
        }

        internal static void CreateLocalizationScriptsStoreFolder(LocalizationAssetSettings settings)
        {
            if (Directory.Exists(settings.LocalizationScriptsStorePath))
                return;

            Directory.CreateDirectory(settings.LocalizationScriptsStorePath);
            AssetDatabase.ImportAsset(settings.LocalizationScriptsStorePath);
        }

        internal static LocalizationAssetSettings Load()
        {
            if (File.Exists(DefaultSettingsAssetPath))
                return AssetDatabase.LoadAssetAtPath<LocalizationAssetSettings>(DefaultSettingsAssetPath);

            var guids = AssetDatabase.FindAssets($"t: {nameof(LocalizationAssetSettings)}");
            if (guids.Length > 0)
            {
                var guid = guids[0];
                var path = AssetDatabase.GUIDToAssetPath(guid);
                return AssetDatabase.LoadAssetAtPath<LocalizationAssetSettings>(path);
            }

            // Find nothing, create new and load it.
            var settings = CreateInstance<LocalizationAssetSettings>();
            AssetDatabase.CreateAsset(settings, DefaultSettingsAssetPath);
            return AssetDatabase.LoadAssetAtPath<LocalizationAssetSettings>(DefaultSettingsAssetPath);
        }

        internal bool CanBuildLocaleAssets(string localeString)
        {
            if (targetLocales is { Length: > 0 })
                return targetLocales.Contains(localeString);

            if (excludedLocales is { Length: > 0 })
                return !excludedLocales.Contains(localeString);

            return true;
        }

        [UsedImplicitly]
        private void OnEnable()
        {
            if (!string.IsNullOrEmpty(localizationScriptNamespace))
                return;

            localizationScriptNamespace = $"{(string.IsNullOrEmpty(PlayerSettings.productName) ? "Project" : PlayerSettings.productName)}.Localization";
        }
    }
}