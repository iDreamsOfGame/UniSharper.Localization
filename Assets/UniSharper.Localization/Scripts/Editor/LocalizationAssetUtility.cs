// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
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
        #region Methods

        internal static bool BuildLocalizationAssets(Dictionary<Locale, Dictionary<string, string>> translationDataMap)
        {
            if (translationDataMap.Count > 0)
            {
                LocalizationAssetSettings settings = LocalizationAssetSettings.Load();
                LocalizationAssetSettings.CreateLocalizationAssetsFolder(settings);

                foreach (KeyValuePair<Locale, Dictionary<string, string>> locale in translationDataMap)
                {
                    string assetPath = PathUtility.UnifyToAltDirectorySeparatorChar(Path.Combine(settings.LocalizationAssetsPath, $"{locale.Key}.bytes"));
                    string assetAbsolutePath = EditorPath.ConvertToAbsolutePath(assetPath);

                    using (FileStream stream = File.Open(assetAbsolutePath, FileMode.Create))
                    {
                        BinaryFormatter writer = new BinaryFormatter();
                        writer.Serialize(stream, locale.Value);
                    }

                    AssetDatabase.ImportAsset(assetPath);
                }

                return true;
            }

            return false;
        }

        internal static bool GenerateScripts(Dictionary<Locale, Dictionary<string, string>> translationDataMap)
        {
            try
            {
                LocalizationAssetSettings settings = LocalizationAssetSettings.Load();
                LocalizationAssetSettings.CreateLocalizationScriptsStoreFolder(settings);

                // Generate Locales.cs
                string scriptLocalesStorePath = EditorPath.ConvertToAbsolutePath(settings.LocalizationScriptsStorePath, "Locales.cs");
                string scriptLocalesAssetPath = EditorPath.ConvertToAssetPath(scriptLocalesStorePath);
                string scriptTextContent = AssetDatabaseUtility.LoadEditorResources<TextAsset>("NewLocalesScriptTemplate")[0].text;
                scriptTextContent = scriptTextContent.Replace(ScriptTemplate.Placeholders.Namespace, settings.LocalizationScriptNamespace);
                scriptTextContent = scriptTextContent.Replace(ScriptTemplate.Placeholders.Fields, GenerateFieldsForScriptLocales(translationDataMap));
                File.WriteAllText(scriptLocalesStorePath, scriptTextContent, new UTF8Encoding(true));

                // Generate TranslationKey.cs
                string scriptTranslationKeyStorePath = EditorPath.ConvertToAbsolutePath(settings.LocalizationScriptsStorePath, "TranslationKey.cs");
                string scriptTranslationKeyAssetPath = EditorPath.ConvertToAssetPath(scriptLocalesStorePath);
                scriptTextContent = AssetDatabaseUtility.LoadEditorResources<TextAsset>("NewTranlationKeyScriptTemplate")[0].text;
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
            LocalizationAssetSettings settings = LocalizationAssetSettings.Load();

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

            Dictionary<Locale, Dictionary<string, string>> translationDataMap = null;
            string dirPath = EditorPath.ConvertToAbsolutePath(settings.LocalizationAssetsPath);
            string[] files = Directory.GetFiles(dirPath, "*.bytes");

            if (files.Length <= 0)
                return null;

            translationDataMap = new Dictionary<Locale, Dictionary<string, string>>();

            foreach (string file in files)
            {
                string localeString = Path.GetFileNameWithoutExtension(file);
                Locale locale = new Locale(localeString);
                using (FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter reader = new BinaryFormatter();
                    Dictionary<string, string> translationTexts = reader.Deserialize(stream) as Dictionary<string, string>;
                    translationDataMap.AddUnique(locale, translationTexts);
                }
            }

            return translationDataMap;
        }

        internal static Dictionary<Locale, Dictionary<string, string>> LoadTranslationFile()
        {
            LocalizationAssetSettings settings = LocalizationAssetSettings.Load();

            if (settings == null)
            {
                Debug.LogError("No Localization Settings file exists!");
                return null;
            }

            if (string.IsNullOrEmpty(settings.TranslationFilePath))
            {
                Debug.LogError("Translation File Path' should be non-empty!");
                return null;
            }

            Dictionary<Locale, Dictionary<string, string>> dataMap = null;
            string path = settings.TranslationFilePath;
            string fileExtension = Path.GetExtension(path).ToLower();
            using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = fileExtension.Equals(".xlsx") ? ExcelReaderFactory.CreateOpenXmlReader(stream) : ExcelReaderFactory.CreateBinaryReader(stream))
                {
                    DataSet dataSet = reader.AsDataSet();

                    if (dataSet.Tables.Count > 0)
                    {
                        DataTable table = dataSet.Tables[0];
                        DataColumnCollection columns = table.Columns;
                        DataRowCollection rows = table.Rows;

                        if (columns.Count > 1 && rows.Count > 1)
                        {
                            dataMap = new Dictionary<Locale, Dictionary<string, string>>();
                            Dictionary<Locale, int> localeColumnIndexMap = new Dictionary<Locale, int>();

                            // Record locales.
                            for (int i = 1; i < columns.Count; i++)
                            {
                                string localeString = rows[0][i].ToString().Trim();
                                if (string.IsNullOrEmpty(localeString))
                                    continue;
                                Locale locale = new Locale(localeString);
                                localeColumnIndexMap.AddUnique(locale, i);
                                dataMap.AddUnique(locale, new Dictionary<string, string>());
                            }

                            // Records translation data.
                            for (int i = 1; i < rows.Count; i++)
                            {
                                foreach (KeyValuePair<Locale, int> kvp in localeColumnIndexMap)
                                {
                                    Locale locale = kvp.Key;
                                    int columnIndex = kvp.Value;
                                    string translationKey = rows[i][0].ToString().Trim();
                                    string translationText = rows[i][columnIndex].ToString();

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
                            Debug.LogError("Invalid translation data format!");
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
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Dictionary<string, string> item in translationDataMap.Values)
            {
                int i = 0;
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
            StringBuilder stringBuilder = new StringBuilder();

            int i = 0;
            foreach (Locale locale in translationDataMap.Keys)
            {
                bool hasDefinedConstantName = locale.GetConstantName(out var fieldName);

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