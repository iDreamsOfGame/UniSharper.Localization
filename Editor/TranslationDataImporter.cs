// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEditorUtility = UnityEditor.EditorUtility;

namespace UniSharperEditor.Localization
{
    internal class TranslationDataImporter
    {
        private const float LabelWidth = 275f;

        private const int MaxIntValue = 10;

        private const string ScriptsGeneratedPrefKey = "UniSharperEditor.Localization.TranslationDataImporterscriptsGenerated";

        private readonly LocalizationAssetSettings settings;

        private SerializedObject settingsSerializedObject;

        internal TranslationDataImporter(LocalizationAssetSettings settings) => this.settings = settings;

        private SerializedObject SettingsSerializedObject => settingsSerializedObject ??= new SerializedObject(settings);

        internal void DrawEditorGui()
        {
            if (settings == null)
                return;

            GUILayout.BeginVertical();
            GUILayout.Label("Import Settings", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginVertical();

            // Translation File Path
            if (!string.IsNullOrEmpty(LocalizationAssetSettings.TranslationFilePath) && !File.Exists(LocalizationAssetSettings.TranslationFilePath))
                LocalizationAssetSettings.TranslationFilePath = string.Empty;

            var directory = string.IsNullOrEmpty(LocalizationAssetSettings.TranslationFilePath) 
                ? Directory.GetCurrentDirectory()
                : new FileInfo(LocalizationAssetSettings.TranslationFilePath).DirectoryName;
            LocalizationAssetSettings.TranslationFilePath = EditorGUILayoutUtility.FileField(new GUIContent("Translation File Path", "Where to locate translation excel file."),
                LocalizationAssetSettings.TranslationFilePath, "Select Translation Excel File", directory, new[] { "Excel Files", "xlsx,xls" }, LabelWidth);

            // Localization Assets Path
            var localizationAssetsAbsolutePath = EditorPath.ConvertToAssetPath(EditorGUILayoutUtility.FolderField(new GUIContent("Localization Assets Path", "Where to store localization assets."), settings.LocalizationAssetsPath,
                "Localization Assets Path", Path.Combine(Directory.GetCurrentDirectory(), settings.LocalizationAssetsPath), string.Empty, LabelWidth));

            if (EditorPath.IsAssetPath(localizationAssetsAbsolutePath))
            {
                settings.LocalizationAssetsPath = EditorPath.ConvertToAssetPath(localizationAssetsAbsolutePath);
            }
            else
            {
                UnityEditorUtility.DisplayDialog("Invalid Path", "The 'Localization Assets Path' you choose is invalid path, please select the folder in the project!", "OK");
            }

            // Localization Script Namespace
            using (new EditorGUIFieldScope(LabelWidth))
            {
                settings.LocalizationScriptNamespace = EditorGUILayout.TextField(new GUIContent("Localization Script Namespace", "The namespace of scripts 'Locales.cs' and 'TranslationKey.cs'."), settings.LocalizationScriptNamespace);
            }

            // Localization Scripts Store Path
            var localizationScriptsStoreAbsolutePath = EditorPath.ConvertToAssetPath(EditorGUILayoutUtility.FolderField(new GUIContent("Localization Scripts Store Path", "Where to store localization scripts."), settings.LocalizationScriptsStorePath,
                "Localization Scripts Store Path", Path.Combine(Directory.GetCurrentDirectory(), settings.LocalizationScriptsStorePath), string.Empty, LabelWidth));

            if (EditorPath.IsAssetPath(localizationScriptsStoreAbsolutePath))
            {
                settings.LocalizationScriptsStorePath = EditorPath.ConvertToAssetPath(localizationScriptsStoreAbsolutePath);
            }
            else
            {
                UnityEditorUtility.DisplayDialog("Invalid Path", "The 'Localization Scripts Store Path' you choose is invalid path, please select the folder in the project!", "OK");
            }

            // Locale Row Index
            using (new EditorGUIFieldScope(LabelWidth))
            {
                settings.LocaleRowIndex = EditorGUILayout.IntSlider(new GUIContent("Locale Row Index", "The row index of locale definition."), settings.LocaleRowIndex, 0, MaxIntValue);
            }

            // Translation Key Column Index
            using (new EditorGUIFieldScope(LabelWidth))
            {
                settings.TranslationKeyColumnIndex = EditorGUILayout.IntSlider(new GUIContent("Translation Key Column Index", "The column index of translation key definition."), settings.TranslationKeyColumnIndex, 0, MaxIntValue);
            }

            // Translation Text Row Start Index
            using (new EditorGUIFieldScope(LabelWidth))
            {
                settings.TranslationTextRowStartIndex =
                    EditorGUILayout.IntSlider(new GUIContent("Translation Text Row Start Index", "The row index and after will be translation texts data."), settings.TranslationTextRowStartIndex, 0, MaxIntValue);
            }

            // Translation Text Column Index Range
            using (new EditorGUIFieldScope(LabelWidth))
            {
                settings.TranslationTextColumnIndexRange = EditorGUILayout.Vector2IntField(new GUIContent("Translation Text Column Index Range", "The column start index and till end index will be translation texts data."),
                    settings.TranslationTextColumnIndexRange);
            }
            
            // Font Column Index Range
            using (new EditorGUIFieldScope(LabelWidth))
            {
                settings.FontColumnIndexRange = EditorGUILayout.Vector2IntField(new GUIContent("Font Column Index Range", "The column start index and till end index will be font information of text."),
                    settings.FontColumnIndexRange);
            }
            
            // Style Column Index Range
            using (new EditorGUIFieldScope(LabelWidth))
            {
                settings.StyleColumnIndexRange = EditorGUILayout.Vector2IntField(new GUIContent("Style Column Index Range", "The style start index and till end index will be style information of text."),
                    settings.StyleColumnIndexRange);
            }

            // Target Locales
            using (new EditorGUIFieldScope(LabelWidth))
            {
                var targetLocalesProperty = SettingsSerializedObject.FindProperty("targetLocales");
                EditorGUILayout.PropertyField(targetLocalesProperty, new GUIContent("Target Locales", "The list of Locales that only need to be built."));
            }
            
            // Excluded Locales
            using (new EditorGUIFieldScope(LabelWidth))
            {
                var excludedLocalesProperty = SettingsSerializedObject.FindProperty("excludedLocales");
                EditorGUILayout.PropertyField(excludedLocalesProperty, new GUIContent("Excluded Locales", "The list of Locales that should be excluded in the build."));
            }

            // Limit the value of TranslationTextRowStartIndex
            if (settings.TranslationTextRowStartIndex <= settings.LocaleRowIndex)
                settings.TranslationTextRowStartIndex = settings.LocaleRowIndex + 1;
            
            // Limit the value of TranslationTextColumnIndexRange
            if (settings.TranslationTextColumnIndexRange.x <= settings.TranslationKeyColumnIndex)
                settings.TranslationTextColumnIndexRange = new Vector2Int(settings.TranslationKeyColumnIndex + 1, settings.TranslationTextColumnIndexRange.y);

            if (settings.TranslationTextColumnIndexRange.y < settings.TranslationTextColumnIndexRange.x)
                settings.TranslationTextColumnIndexRange = new Vector2Int(settings.TranslationTextColumnIndexRange.x, settings.TranslationTextColumnIndexRange.x);
            
            // Limit the value of FontColumnIndexRange
            if (settings.FontColumnIndexRange.x <= settings.TranslationTextColumnIndexRange.y)
                settings.FontColumnIndexRange = new Vector2Int(settings.TranslationTextColumnIndexRange.y + 1, settings.FontColumnIndexRange.y);

            if (settings.FontColumnIndexRange.y < settings.FontColumnIndexRange.x)
                settings.FontColumnIndexRange = new Vector2Int(settings.FontColumnIndexRange.x, settings.FontColumnIndexRange.x);
            
            // Limit the value of StyleColumnIndexRange
            if (settings.StyleColumnIndexRange.x <= settings.FontColumnIndexRange.y)
                settings.StyleColumnIndexRange = new Vector2Int(settings.FontColumnIndexRange.y + 1, settings.StyleColumnIndexRange.y);

            if (settings.StyleColumnIndexRange.y < settings.StyleColumnIndexRange.x)
                settings.StyleColumnIndexRange = new Vector2Int(settings.StyleColumnIndexRange.x, settings.StyleColumnIndexRange.x);

            SettingsSerializedObject.ApplyModifiedProperties();

            GUILayout.Space(20);

            try
            {
                if (GUILayout.Button("Build Localization Assets"))
                    BuildAssets();
            }
            catch (Exception)
            {
                // ignored
            }

            GUILayout.EndVertical();
            GUILayout.Space(10);
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        private static void BuildAssets()
        {
            var translationDataMap = LocalizationAssetUtility.ParseTranslationDataFile();
            if (translationDataMap == null || !LocalizationAssetUtility.BuildLocalizationAssets(translationDataMap))
                return;

            var result = LocalizationAssetUtility.GenerateScripts(translationDataMap);
            EditorPrefs.SetBool(ScriptsGeneratedPrefKey, result);

            if (result)
            {
                UnityEditorUtility.DisplayProgressBar("Hold on...", "Compiling scripts...", 0f);
            }
            else
            {
                UnityEditorUtility.DisplayDialog("Error", "Failed to generate scripts!", "OK");
            }
        }

        [DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            var scriptsGenerated = EditorPrefs.GetBool(ScriptsGeneratedPrefKey);
            if (!scriptsGenerated)
                return;

            UnityEditorUtility.ClearProgressBar();
            EditorPrefs.SetBool(ScriptsGeneratedPrefKey, false);

            if (!UnityEditorUtility.scriptCompilationFailed)
            {
                if (UnityEditorUtility.DisplayDialog("Success", "Build success!", "OK"))
                    UnityEditorUtility.ClearProgressBar();
            }
            else
            {
                if (UnityEditorUtility.DisplayDialog("Error", "Failed to compile scripts!", "OK"))
                    UnityEditorUtility.ClearProgressBar();
            }
        }
    }
}