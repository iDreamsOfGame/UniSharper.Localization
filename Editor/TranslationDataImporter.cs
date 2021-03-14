// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEditorUtility = UnityEditor.EditorUtility;

namespace UniSharperEditor.Localization
{
    internal class TranslationDataImporter
    {
        #region Fields

        private const float LabelWidth = 275f;

        private const int MaxIntValue = 10;

        private const string ScriptsGeneratedPrefKey = "UniSharperEditor.Localization.TranslationDataImporterscriptsGenerated";

        private readonly LocalizationAssetSettings settings;

        #endregion Fields

        #region Constructors

        internal TranslationDataImporter(LocalizationAssetSettings settings) => this.settings = settings;

        #endregion Constructors

        #region Methods

        internal void DrawEditorGui()
        {
            if (settings == null)
            {
                return;
            }

            GUILayout.BeginVertical();
            GUILayout.Label("Import Settings", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginVertical();

            // Translation File Path
            if (!string.IsNullOrEmpty(LocalizationAssetSettings.TranslationFilePath) && !File.Exists(LocalizationAssetSettings.TranslationFilePath))
            {
                LocalizationAssetSettings.TranslationFilePath = string.Empty;
            }

            LocalizationAssetSettings.TranslationFilePath = EditorGUILayoutUtility.FileField(new GUIContent("Translation File Path", "Where to locate translation excel file."), LocalizationAssetSettings.TranslationFilePath, "Select Translation Excel File", string.IsNullOrEmpty(LocalizationAssetSettings.TranslationFilePath) ? Directory.GetCurrentDirectory() : new FileInfo(LocalizationAssetSettings.TranslationFilePath).DirectoryName, new string[] { "Excel Files", "xlsx,xls" }, LabelWidth);

            // Localization Assets Path
            var localizationAssetsAbsolutePath = EditorPath.ConvertToAssetPath(EditorGUILayoutUtility.FolderField(new GUIContent("Localization Assets Path", "Where to store localization assets."), settings.LocalizationAssetsPath, "Localization Assets Path", Path.Combine(Directory.GetCurrentDirectory(), settings.LocalizationAssetsPath), string.Empty, LabelWidth));

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
            var localizationScriptsStoreAbsolutePath = EditorPath.ConvertToAssetPath(EditorGUILayoutUtility.FolderField(new GUIContent("Localization Scripts Store Path", "Where to store localization scripts."), settings.LocalizationScriptsStorePath, "Localization Scripts Store Path", Path.Combine(Directory.GetCurrentDirectory(), settings.LocalizationScriptsStorePath), string.Empty, LabelWidth));

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

            using (new EditorGUIFieldScope(LabelWidth))
            {
                settings.TranslationTextsStartingRowIndex = EditorGUILayout.IntSlider(new GUIContent("Translation Texts Starting Row Index", "The row index and after will be translation texts data."), settings.TranslationTextsStartingRowIndex, 0, MaxIntValue);
            }

            // Translation Texts Starting Column Index
            using (new EditorGUIFieldScope(LabelWidth))
            {
                settings.TranslationTextsStartingColumnIndex = EditorGUILayout.IntSlider(new GUIContent("Translation Texts Starting Column Index", "The column index and after will be translation texts data."), settings.TranslationTextsStartingColumnIndex, 0, MaxIntValue);
            }

            // If the texts starting row index less than or equals the locale row index, then fix it's value just by letting it equals locale row index + 1
            if (settings.TranslationTextsStartingRowIndex <= settings.LocaleRowIndex)
            {
                settings.TranslationTextsStartingRowIndex = settings.LocaleRowIndex + 1;
            }

            // If the texts starting column index less than or equals the key column index, then fix it's value just by letting it equals key column index + 1
            if (settings.TranslationTextsStartingColumnIndex <= settings.TranslationKeyColumnIndex)
            {
                settings.TranslationTextsStartingColumnIndex = settings.TranslationKeyColumnIndex + 1;
            }

            GUILayout.Space(20);

            if (GUILayout.Button("Build Localization Assets"))
            {
                BuildAssets();
                GUIUtility.ExitGUI();
            }

            GUILayout.EndVertical();
            GUILayout.Space(10);
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        private static void BuildAssets()
        {
            var translationDataMap = LocalizationAssetUtility.LoadTranslationFile();

            if (translationDataMap == null ||
                !LocalizationAssetUtility.BuildLocalizationAssets(translationDataMap))
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
                UnityEditorUtility.DisplayDialog("Success", "Build success!", "OK");
            }
            else
            {
                UnityEditorUtility.DisplayDialog("Error", "Failed to compile scripts!", "OK");
            }
        }

        #endregion Methods
    }
}