// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UniSharper.Localization;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor.Localization
{
    internal static class LocalizationAssetUtility
    {
        #region Fields

        private const string UnityPackageName = "io.github.idreamsofgame.unisharper.localization";

        #endregion Fields

        #region Methods

        internal static bool BuildLocalizationAssets(Dictionary<Locale, Dictionary<string, string>> translationDataMap)
        {
            if (translationDataMap.Count <= 0)
                return false;

            var settings = LocalizationAssetSettings.Load();
            LocalizationAssetSettings.CreateLocalizationAssetsFolder(settings);

            foreach (var locale in translationDataMap)
            {
                var assetPath = PathUtility.UnifyToAltDirectorySeparatorChar(Path.Combine(settings.LocalizationAssetsPath, $"{locale.Key}.bytes"));
                var assetAbsolutePath = EditorPath.ConvertToAbsolutePath(assetPath);

                using (var stream = File.Open(assetAbsolutePath, FileMode.Create))
                {
                    var writer = new BinaryFormatter();
                    writer.Serialize(stream, locale.Value);
                }

                AssetDatabase.ImportAsset(assetPath);
            }

            return true;
        }

        internal static bool GenerateScripts(Dictionary<Locale, Dictionary<string, string>> translationDataMap)
        {
            try
            {
                var settings = LocalizationAssetSettings.Load();
                LocalizationAssetSettings.CreateLocalizationScriptsStoreFolder(settings);

                // Generate Locales.cs
                var scriptLocalesStorePath = EditorPath.ConvertToAbsolutePath(settings.LocalizationScriptsStorePath, "Locales.cs");
                var scriptLocalesAssetPath = EditorPath.ConvertToAssetPath(scriptLocalesStorePath);
                var scriptTextContent = ScriptTemplate.LoadScriptTemplateFile("NewLocalesScriptTemplate.txt", UnityPackageName);
                scriptTextContent = scriptTextContent.Replace(ScriptTemplate.Placeholders.Namespace, settings.LocalizationScriptNamespace);
                scriptTextContent = scriptTextContent.Replace(ScriptTemplate.Placeholders.Fields, GenerateFieldsForScriptLocales(translationDataMap));
                File.WriteAllText(scriptLocalesStorePath, scriptTextContent, new UTF8Encoding(true));

                // Generate TranslationKey.cs
                var scriptTranslationKeyStorePath = EditorPath.ConvertToAbsolutePath(settings.LocalizationScriptsStorePath, "TranslationKey.cs");
                var scriptTranslationKeyAssetPath = EditorPath.ConvertToAssetPath(scriptLocalesStorePath);
                scriptTextContent = ScriptTemplate.LoadScriptTemplateFile("NewTranlationKeyScriptTemplate.txt", UnityPackageName);
                scriptTextContent = scriptTextContent.Replace(ScriptTemplate.Placeholders.Namespace, settings.LocalizationScriptNamespace);
                scriptTextContent = scriptTextContent.Replace(ScriptTemplate.Placeholders.Constants, GenerateConstantsForScriptTranslationKey(translationDataMap));
                File.WriteAllText(scriptTranslationKeyStorePath, scriptTextContent, new UTF8Encoding(true));
                AssetDatabase.ImportAsset(scriptLocalesAssetPath);
                AssetDatabase.ImportAsset(scriptTranslationKeyAssetPath);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                return false;
            }
        }

        internal static Dictionary<Locale, Dictionary<string, string>> LoadLocalizationAssets()
        {
            var settings = LocalizationAssetSettings.Load();

            if (settings == null)
            {
                Debug.LogError("No Localization Settings file exists!");
                return null;
            }

            if (!Directory.Exists(settings.LocalizationAssetsPath))
            {
                Debug.LogError("The folder path to store Localization assets is not exists!");
                return null;
            }

            var dirPath = EditorPath.ConvertToAbsolutePath(settings.LocalizationAssetsPath);
            var files = Directory.GetFiles(dirPath, "*.bytes");

            if (files.Length <= 0)
                return null;

            var translationDataMap = new Dictionary<Locale, Dictionary<string, string>>();

            foreach (var file in files)
            {
                var localeString = Path.GetFileNameWithoutExtension(file);
                var locale = new Locale(localeString);
                using (var stream = File.Open(file, FileMode.Open, FileAccess.Read))
                {
                    var reader = new BinaryFormatter();
                    var translationTexts = reader.Deserialize(stream) as Dictionary<string, string>;
                    translationDataMap.AddUnique(locale, translationTexts);
                }
            }

            return translationDataMap;
        }

        internal static Dictionary<Locale, Dictionary<string, string>> LoadTranslationFile()
        {
            var settings = LocalizationAssetSettings.Load();

            if (settings == null)
            {
                Debug.LogError("No Localization Settings file exists!");
                return null;
            }

            if (string.IsNullOrEmpty(LocalizationAssetSettings.TranslationFilePath))
            {
                Debug.LogError("Translation File Path' should be non-empty!");
                return null;
            }

            Dictionary<Locale, Dictionary<string, string>> dataMap = null;
            var path = LocalizationAssetSettings.TranslationFilePath;
            var fileExtension = Path.GetExtension(path).ToLower();
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = fileExtension.Equals(".xlsx") ?
                    ExcelReaderFactory.CreateOpenXmlReader(stream) : ExcelReaderFactory.CreateBinaryReader(stream))
                {
                    var dataSet = reader.AsDataSet();

                    if (dataSet.Tables.Count > 0)
                    {
                        var table = dataSet.Tables[0];
                        var columns = table.Columns;
                        var rows = table.Rows;

                        if (columns.Count > 1 && rows.Count > 1)
                        {
                            dataMap = new Dictionary<Locale, Dictionary<string, string>>();
                            var localeColumnIndexMap = new Dictionary<Locale, int>();

                            // Record locales.
                            for (var i = settings.TranslationTextsStartingColumnIndex; i < columns.Count; i++)
                            {
                                var localeString = rows[settings.LocaleRowIndex][i].ToString().Trim();
                                if (string.IsNullOrEmpty(localeString))
                                    continue;
                                var locale = new Locale(localeString);
                                localeColumnIndexMap.AddUnique(locale, i);
                                dataMap.AddUnique(locale, new Dictionary<string, string>());
                            }

                            // Records translation data.
                            for (var i = settings.TranslationTextsStartingRowIndex; i < rows.Count; i++)
                            {
                                foreach (var kvp in localeColumnIndexMap)
                                {
                                    var locale = kvp.Key;
                                    var columnIndex = kvp.Value;
                                    var translationKey = rows[i][settings.TranslationKeyColumnIndex].ToString().Trim();
                                    var translationText = rows[i][columnIndex].ToString();

                                    if (string.IsNullOrEmpty(translationKey))
                                        continue;

                                    if (string.IsNullOrEmpty(translationText))
                                    {
                                        translationText = LocalizationManager.DefaultText;
                                    }

                                    if (dataMap.ContainsKey(locale) && !dataMap[locale].ContainsKey(translationKey))
                                    {
                                        dataMap[locale].Add(translationKey, translationText);
                                    }
                                    else
                                    {
                                        Debug.LogErrorFormat("Found repeat translation key in cell 'A{0}'!", (i + 1).ToString());
                                        return null;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("Invalid translation file format!");
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid translation file!");
                    }
                }
            }

            return dataMap;
        }

        private static string GenerateConstantsForScriptTranslationKey(Dictionary<Locale, Dictionary<string, string>> translationDataMap)
        {
            var stringBuilder = new StringBuilder();

            foreach (var item in translationDataMap.Values)
            {
                var i = 0;
                foreach (var translationKey in item.Keys)
                {
                    stringBuilder.AppendFormat("\t\tpublic const string {0} = \"{1}\";",
                        translationKey.ToTitleCase(), translationKey);

                    if (i < item.Keys.Count - 1)
                    {
                        stringBuilder.AppendWindowsNewLine()
                            .AppendWindowsNewLine();
                    }
                    i++;
                }

                break;
            }

            return stringBuilder.ToString();
        }

        private static string GenerateFieldsForScriptLocales(Dictionary<Locale, Dictionary<string, string>> translationDataMap)
        {
            var stringBuilder = new StringBuilder();

            var i = 0;
            foreach (var locale in translationDataMap.Keys)
            {
                var hasDefinedConstantName = locale.GetConstantName(out var fieldName);

                if (hasDefinedConstantName)
                {
                    stringBuilder.AppendFormat("\t\tpublic static readonly Locale {0} = Locale.{1};",
                        fieldName, fieldName);
                }
                else
                {
                    stringBuilder.AppendFormat("\t\tpublic static readonly Locale {0} = new Locale(\"{1}\");",
                        fieldName, locale);
                }

                if (i < translationDataMap.Keys.Count - 1)
                {
                    stringBuilder.AppendWindowsNewLine()
                        .AppendWindowsNewLine();
                }

                i++;
            }

            return stringBuilder.ToString();
        }

        #endregion Methods
    }
}