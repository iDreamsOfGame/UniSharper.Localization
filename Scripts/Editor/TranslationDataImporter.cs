// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System.Collections.Generic;
using System.IO;
using UniSharper.Localization;
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

        private const string ScriptsGeneratedPrefKey = "UniSharperEditor.Localization.TranslationDataImporterscriptsGenerated";

        private readonly LocalizationAssetSettings settings;

        #endregion Fields

        #region Constructors

        internal TranslationDataImporter(LocalizationAssetSettings settings)
        {
            this.settings = settings;
        }

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
            if (!string.IsNullOrEmpty(settings.TranslationFilePath) && !File.Exists(settings.TranslationFilePath))
            {
                settings.TranslationFilePath = string.Empty;
            }

            settings.TranslationFilePath = EditorGUILayoutUtility.FileField(new GUIContent("Translation File Path", "Where to locate translation excel file."), settings.TranslationFilePath, "Select Translation Excel File", string.IsNullOrEmpty(settings.TranslationFilePath) ? Directory.GetCurrentDirectory() : new FileInfo(settings.TranslationFilePath).DirectoryName, new string[] { "Excel Files", "xlsx,xls" }, LabelWidth);

            // Localization Assets Path
            string localizationAssetsAbsolutePath = EditorPath.ConvertToAssetPath(EditorGUILayoutUtility.FolderField(new GUIContent("Localization Assets Path", "Where to store localization assets."), settings.LocalizationAssetsPath, "Localization Assets Path", Path.Combine(Directory.GetCurrentDirectory(), settings.LocalizationAssetsPath), string.Empty, LabelWidth));

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
            string localizationScriptsStoreAbsolutePath = EditorPath.ConvertToAssetPath(EditorGUILayoutUtility.FolderField(new GUIContent("Localization Scripts Store Path", "Where to store localization scripts."), settings.LocalizationScriptsStorePath, "Localization Scripts Store Path", PathUtility.Combine(Directory.GetCurrentDirectory(), settings.LocalizationScriptsStorePath), string.Empty, LabelWidth));

            if (EditorPath.IsAssetPath(localizationScriptsStoreAbsolutePath))
            {
                settings.LocalizationScriptsStorePath = EditorPath.ConvertToAssetPath(localizationScriptsStoreAbsolutePath);
            }
            else
            {
                UnityEditorUtility.DisplayDialog("Invalid Path", "The 'Localization Scripts Store Path' you choose is invalid path, please select the folder in the project!", "OK");
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
            Dictionary<Locale, Dictionary<string, string>> translationDataMap = LocalizationAssetUtility.LoadTranslationFile();

            if (translationDataMap != null && LocalizationAssetUtility.BuildLocalizationAssets(translationDataMap))
            {
                bool result = LocalizationAssetUtility.GenerateScripts(translationDataMap);
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
        }

        [DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            bool scriptsGenerated = EditorPrefs.GetBool(ScriptsGeneratedPrefKey);

            if (scriptsGenerated)
            {
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
        }

        #endregion Methods
    }
}