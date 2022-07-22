// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using JetBrains.Annotations;
using ReSharp.Security.Cryptography;
using System.IO;
using System.Linq;
using UniSharper;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor.Localization
{
    /// <summary>
    /// Class used to get and set the localization settings object. Implements the <see cref="UniSharperEditor.SettingsScriptableObject"/>
    /// </summary>
    /// <seealso cref="UniSharperEditor.SettingsScriptableObject"/>
    public class LocalizationAssetSettings : SettingsScriptableObject
    {
        private const string LocalizationAssetsFolderName = "Locales";

        private const string LocalizationFolderName = "Localization";

        private static readonly string LocalizationFolder = Path.Combine(EditorEnvironment.AssetsFolderName, LocalizationFolderName);

        private static readonly string SettingsAssetPath = $"{LocalizationFolder}/{nameof(LocalizationAssetSettings)}.asset";

        private static readonly string TranslationFilePathPrefKeyFormat = $"{CryptoUtility.Md5HashEncrypt(Directory.GetCurrentDirectory(), false)}.{typeof(LocalizationAssetSettings).FullName}.translationFilePath";

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
        private string localizationScriptsStorePath = PathUtility.UnifyToAltDirectorySeparatorChar(Path.Combine(EditorEnvironment.AssetsFolderName, EditorEnvironment.DefaultScriptsFolderName));

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
        private Vector2Int fontColumnIndexRange = Vector2Int.one * 2;
        
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
                return EditorPrefs.GetString(key, string.Empty);
            }
            set
            {
                if (string.IsNullOrEmpty(value) || TranslationFilePath.Equals(value))
                    return;

                var key = string.Format(TranslationFilePathPrefKeyFormat, PlayerSettings.productName);
                EditorPrefs.SetString(key, value);
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

        internal Vector2Int FontColumnIndexRange
        {
            get => fontColumnIndexRange;
            set
            {
                if (fontColumnIndexRange.Equals(value))
                    return;
                
                fontColumnIndexRange = value;
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

            if (File.Exists(SettingsAssetPath))
            {
                settings = Load();
            }
            else
            {
                settings = CreateInstance<LocalizationAssetSettings>();
                CreateLocalizationAssetsRootFolder();
                CreateLocalizationAssetsFolder(settings);
                CreateLocalizationScriptsStoreFolder(settings);
                AssetDatabase.CreateAsset(settings, SettingsAssetPath);
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
            
            AssetDatabase.CreateFolder(EditorEnvironment.AssetsFolderName, LocalizationFolderName);
        }

        internal static void CreateLocalizationScriptsStoreFolder(LocalizationAssetSettings settings)
        {
            if (Directory.Exists(settings.LocalizationScriptsStorePath))
                return;
            
            Directory.CreateDirectory(settings.LocalizationScriptsStorePath);
            AssetDatabase.ImportAsset(settings.LocalizationScriptsStorePath);
        }

        internal static LocalizationAssetSettings Load() => 
            File.Exists(SettingsAssetPath) ? AssetDatabase.LoadAssetAtPath<LocalizationAssetSettings>(SettingsAssetPath) : null;

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