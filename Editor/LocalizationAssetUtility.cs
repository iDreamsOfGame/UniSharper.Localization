﻿// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UniSharper;
using UniSharper.Localization;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor.Localization
{
    internal static class LocalizationAssetUtility
    {
        private const string UnityPackageName = "io.github.idreamsofgame.unisharper.localization";

        internal static bool BuildLocalizationAssets(Dictionary<string, Dictionary<string, TranslationData>> translationDataMap)
        {
            if (translationDataMap.Count <= 0)
                return false;

            var settings = LocalizationAssetSettings.Load();
            LocalizationAssetSettings.CreateLocalizationAssetsFolder(settings);

            foreach (var (locale, dataMap) in translationDataMap)
            {
                var assetPath = PathUtility.UnifyToAltDirectorySeparatorChar(Path.Combine(settings.LocalizationAssetsPath, $"{locale}.bytes"));
                var assetAbsolutePath = EditorPath.ConvertToAbsolutePath(assetPath);

                using (var stream = File.Open(assetAbsolutePath, FileMode.Create))
                {
                    var writer = new BinaryFormatter();
                    writer.Serialize(stream, dataMap);
                }

                AssetDatabase.ImportAsset(assetPath);
            }

            return true;
        }

        internal static bool GenerateScripts(Dictionary<string, Dictionary<string, TranslationData>> source)
        {
            var translationDataMap = new Dictionary<Locale, Dictionary<string, TranslationData>>();
            foreach (var (localeString, dataMap) in source)
            {
                translationDataMap.Add(new Locale(localeString), dataMap);
            }
            
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
                scriptTextContent = ScriptTemplate.LoadScriptTemplateFile("NewTranslationKeyScriptTemplate.txt", UnityPackageName);
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
                using var stream = File.Open(file, FileMode.Open, FileAccess.Read);
                var reader = new BinaryFormatter();
                var translationTexts = reader.Deserialize(stream) as Dictionary<string, string>;
                translationDataMap.AddUnique(locale, translationTexts);
            }

            return translationDataMap;
        }

        internal static Dictionary<string, Dictionary<string, TranslationData>> ParseTranslationDataFile()
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

            var translationDataMap = new Dictionary<string, Dictionary<string, TranslationData>>();
            var localeTextColumnIndexMap = new Dictionary<string, int>();
            var localeFontColumnIndexMap = new Dictionary<string, int>();
            var localeStyleColumnIndexMap = new Dictionary<string, int>();
            var path = LocalizationAssetSettings.TranslationFilePath;
            var fileExtension = Path.GetExtension(path).ToLower();
            using var stream = File.Open(path, FileMode.Open, FileAccess.Read);
            using var reader = fileExtension.Equals(".xlsx") ? ExcelReaderFactory.CreateOpenXmlReader(stream) : ExcelReaderFactory.CreateBinaryReader(stream);
            var dataSet = reader.AsDataSet();
            
            if (dataSet.Tables.Count > 0)
            {
                for (var index = 0; index < dataSet.Tables.Count; index++)
                {
                    var table = dataSet.Tables[index];
                    var columns = table.Columns;
                    var rows = table.Rows;

                    if (columns.Count > 1 && rows.Count > 1)
                    {
                        if (index == 0)
                        {
                            // Locate locale translation text column.
                            for (var i = settings.TranslationTextColumnIndexRange.x; i <= settings.TranslationTextColumnIndexRange.y; i++)
                            {
                                var localeString = rows[settings.LocaleRowIndex][i].ToString().Trim();
                                if (string.IsNullOrEmpty(localeString))
                                    continue;
                                
                                localeTextColumnIndexMap.AddUnique(localeString, i, false);
                                translationDataMap.AddUnique(localeString, new Dictionary<string, TranslationData>(), false);
                            }
                            
                            // Locate locale font column.
                            for (var i = settings.FontColumnIndexRange.x; i <= settings.FontColumnIndexRange.y; i++)
                            {
                                var cellValue = rows[settings.LocaleRowIndex][i].ToString().Trim();
                                if (string.IsNullOrEmpty(cellValue))
                                    continue;

                                var values = cellValue.Split('.');
                                if (values.Length < 2)
                                    continue;

                                var localeString = values[0];
                                localeFontColumnIndexMap.AddUnique(localeString, i, false);
                            }
                            
                            // Locate locale style column.
                            for (var i = settings.StyleColumnIndexRange.x; i < settings.StyleColumnIndexRange.y; i++)
                            {
                                var cellValue = rows[settings.LocaleRowIndex][i].ToString().Trim();
                                if (string.IsNullOrEmpty(cellValue))
                                    continue;
                                
                                var values = cellValue.Split('.');
                                if (values.Length < 2)
                                    continue;
                                
                                var localeString = values[0];
                                localeStyleColumnIndexMap.AddUnique(localeString, i, false);
                            }
                        }
                        
                        for (var i = settings.TranslationTextRowStartIndex; i < rows.Count; i++)
                        {
                            var translationKey = rows[i][settings.TranslationKeyColumnIndex].ToString().Trim();
                            if (string.IsNullOrEmpty(translationKey))
                                continue;
                            
                            // Parse translation text.
                            foreach (var (localeString, columnIndex) in localeTextColumnIndexMap)
                            {
                                if (columnIndex >= columns.Count)
                                    continue;
                                
                                var translationText = rows[i][columnIndex].ToString();
                                if (string.IsNullOrEmpty(translationText))
                                    translationText = LocalizationManager.DefaultText;
                                
                                if (translationDataMap.ContainsKey(localeString) && !translationDataMap[localeString].ContainsKey(translationKey))
                                {
                                    translationDataMap[localeString].Add(translationKey, new TranslationData(translationText));
                                }
                                else
                                {
                                    Debug.LogErrorFormat("Found repeat translation key in cell 'A{0}'!", (i + 1).ToString());
                                    return null;
                                }
                            }
                            
                            // Parse font.
                            foreach (var (localeString, columnIndex) in localeFontColumnIndexMap)
                            {
                                if (columnIndex >= columns.Count)
                                    continue;
                                
                                var font = rows[i][columnIndex].ToString().Trim();
                                if (string.IsNullOrEmpty(font))
                                    continue;
                                
                                if (translationDataMap.TryGetValue(localeString, out var dataMap) && dataMap.TryGetValue(translationKey, out var translationData))
                                    translationData.Font = font;
                            }
                            
                            // Parse style.
                            foreach (var (localeString, columnIndex) in localeStyleColumnIndexMap)
                            {
                                if (columnIndex >= columns.Count)
                                    continue;
                                
                                var cellValue = rows[i][columnIndex].ToString().Trim();
                                if (string.IsNullOrEmpty(cellValue))
                                    continue;

                                var style = cellValue.Split('|');
                                if(style == null)
                                    continue;
                                
                                if (translationDataMap.TryGetValue(localeString, out var dataMap) && dataMap.TryGetValue(translationKey, out var translationData))
                                    translationData.Style = style;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("Invalid translation file format!");
                    }
                }
            }
            else
            {
                Debug.LogError("Invalid translation file!");
            }

            return translationDataMap;
        }

        private static string GenerateConstantsForScriptTranslationKey(Dictionary<Locale, Dictionary<string, TranslationData>> translationDataMap)
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
                        stringBuilder.Append(PlayerEnvironment.WindowsNewLine)
                            .Append(PlayerEnvironment.WindowsNewLine);
                    }

                    i++;
                }

                break;
            }

            return stringBuilder.ToString();
        }

        private static string GenerateFieldsForScriptLocales(Dictionary<Locale, Dictionary<string, TranslationData>> translationDataMap)
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
                    stringBuilder.Append(PlayerEnvironment.WindowsNewLine)
                        .Append(PlayerEnvironment.WindowsNewLine);
                }

                i++;
            }

            return stringBuilder.ToString();
        }
    }
}