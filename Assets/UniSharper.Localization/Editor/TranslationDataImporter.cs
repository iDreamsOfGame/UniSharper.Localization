// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.IO;
using UniSharperEditor.Extensions;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

// ReSharper disable ConvertIfStatementToNullCoalescingExpression

namespace UniSharperEditor.Localization
{
    internal class TranslationDataImporter
    {
        private const float Padding = 10;

        private const float LabelWidth = 250f;

        private const int MaxIntValue = 10;

        private static readonly string ScriptsGeneratedPrefKey = $"{typeof(TranslationDataImporter).FullName}.sgpk";

        private static bool ScriptsGenerated
        {
            get => EditorPrefsUtility.GetBoolean(ScriptsGeneratedPrefKey, true);
            set => EditorPrefsUtility.SetBoolean(ScriptsGeneratedPrefKey, value);
        }

        private readonly LocalizationAssetSettings settings;

        private SerializedObject settingsSerializedObject;

        private Vector2 scrollPosition = Vector2.zero;

        private Vector2 customCharactersTextAreaScrollPosition = Vector2.zero;

        private bool showExtraCharactersOptions;

        internal TranslationDataImporter(LocalizationAssetSettings settings) => this.settings = settings;

        private SerializedObject SettingsSerializedObject
        {
            get
            {
                settingsSerializedObject ??= new SerializedObject(settings);
                return settingsSerializedObject;
            }
        }

        [DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            if (!ScriptsGenerated)
                return;

            EditorUtility.ClearProgressBar();
            ScriptsGenerated = false;

            if (!EditorUtility.scriptCompilationFailed)
            {
                EditorUtility.DisplayDialog("Success", "Build success!", "OK");
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Failed to compile scripts!", "OK");
            }

            EditorUtility.ClearProgressBar();
        }

        private static void BuildAssets()
        {
            var translationDataMap = LocalizationAssetUtility.ParseTranslationDataFile();
            if (translationDataMap == null || !LocalizationAssetUtility.BuildLocalizationAssets(translationDataMap))
                return;

            ScriptsGenerated = true;

            var result = LocalizationAssetUtility.GenerateScripts(translationDataMap);
            ScriptsGenerated = result;

            if (result)
            {
                EditorUtility.DisplayProgressBar("Hold on...", "Compiling scripts...", 0f);
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Failed to generate scripts!", "OK");
            }
        }

        internal void DrawEditorGui(EditorWindow window)
        {
            if (!settings)
                return;

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(window.position.width), GUILayout.Height(window.position.height));

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.Space(Padding);

                EditorGUILayout.BeginVertical();
                {
                    // Import Settings
                    DrawImportSettingFields();

                    EditorGUILayout.Space(5);

                    // Other Settings
                    DrawOtherSettingFields();

                    EditorGUILayout.Space(5);

                    // Characters Text File Export Settings
                    DrawCharactersTextFileExportSettingFields();

                    EditorGUILayout.Space(Padding);

                    // Build Assets Button
                    DrawBuildAssetsButton();
                    
                    EditorGUILayout.Space(Padding * 3);
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.Space(Padding);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();
        }

        private void DrawImportSettingFields()
        {
            const string title = "Import Settings";
            EditorGUIStyles.DrawTitleLabel(title);

            // Translation File Path
            if (!string.IsNullOrEmpty(LocalizationAssetSettings.TranslationFilePath) && !File.Exists(LocalizationAssetSettings.TranslationFilePath))
                LocalizationAssetSettings.TranslationFilePath = string.Empty;

            var directory = string.IsNullOrEmpty(LocalizationAssetSettings.TranslationFilePath)
                ? Directory.GetCurrentDirectory()
                : new FileInfo(LocalizationAssetSettings.TranslationFilePath).DirectoryName;
            LocalizationAssetSettings.TranslationFilePath = UniEditorGUILayout.FileField(new GUIContent("Translation File Path", "Where to locate translation excel file."),
                LocalizationAssetSettings.TranslationFilePath,
                "Select Translation Excel File",
                directory,
                new[] { "Excel Files", "xlsx,xls" },
                LabelWidth);

            // Localization Assets Path
            var localizationAssetsAbsolutePath = EditorPath.GetAssetPath(UniEditorGUILayout.FolderField(new GUIContent("Localization Assets Path", "Where to store localization assets."),
                settings.LocalizationAssetsPath,
                "Localization Assets Path",
                EditorPath.GetFullPath(settings.LocalizationAssetsPath),
                string.Empty,
                LabelWidth));

            if (EditorPath.TryGetAssetPath(localizationAssetsAbsolutePath, out var tempLocalizationAssetsPath))
            {
                settings.LocalizationAssetsPath = tempLocalizationAssetsPath;
            }
            else
            {
                Debug.LogError("The 'Localization Assets Path' you choose is invalid path, please select the folder in the project!");
            }

            // Localization Script Namespace
            using (new UniEditorGUILayout.FieldScope(LabelWidth))
            {
                settings.LocalizationScriptNamespace = EditorGUILayout.TextField(new GUIContent("Localization Script Namespace", "The namespace of scripts 'Locales.cs' and 'TranslationKey.cs'."), settings.LocalizationScriptNamespace);
            }

            // Localization Scripts Store Path
            var localizationScriptsStoreAbsolutePath = EditorPath.GetAssetPath(UniEditorGUILayout.FolderField(new GUIContent("Localization Scripts Store Path", "Where to store localization scripts."),
                settings.LocalizationScriptsStorePath,
                "Localization Scripts Store Path",
                Path.Combine(settings.LocalizationScriptsStorePath),
                string.Empty,
                LabelWidth));

            if (EditorPath.TryGetAssetPath(localizationScriptsStoreAbsolutePath, out var tempLocalizationScriptsStorePath))
            {
                settings.LocalizationScriptsStorePath = tempLocalizationScriptsStorePath;
            }
            else
            {
                Debug.LogError("The 'Localization Scripts Store Path' you choose is invalid path, please select the folder in the project!");
            }

            // Locale Row Index
            using (new UniEditorGUILayout.FieldScope(LabelWidth))
            {
                settings.LocaleRowIndex = EditorGUILayout.IntSlider(new GUIContent("Locale Row Index", "The row index of locale definition."), settings.LocaleRowIndex, 0, MaxIntValue);
            }

            // Translation Key Column Index
            using (new UniEditorGUILayout.FieldScope(LabelWidth))
            {
                settings.TranslationKeyColumnIndex = EditorGUILayout.IntSlider(new GUIContent("Translation Key Column Index", "The column index of translation key definition."), settings.TranslationKeyColumnIndex, 0, MaxIntValue);
            }

            // Translation Text Row Start Index
            using (new UniEditorGUILayout.FieldScope(LabelWidth))
            {
                settings.TranslationTextRowStartIndex =
                    EditorGUILayout.IntSlider(new GUIContent("Translation Text Row Start Index", "The row index and after will be translation texts data."), settings.TranslationTextRowStartIndex, 0, MaxIntValue);
            }

            // Translation Text Column Index Range
            using (new UniEditorGUILayout.FieldScope(LabelWidth))
            {
                settings.TranslationTextColumnIndexRange = EditorGUILayout.Vector2IntField(new GUIContent("Translation Text Column Index Range", "The column start index and till end index will be translation texts data."),
                    settings.TranslationTextColumnIndexRange);
            }

            // Style Column Index Range
            using (new UniEditorGUILayout.FieldScope(LabelWidth))
            {
                settings.StyleColumnIndexRange = EditorGUILayout.Vector2IntField(new GUIContent("Style Column Index Range", "The style start index and till end index will be style information of text."),
                    settings.StyleColumnIndexRange);
            }

            // Limit the value of TranslationTextRowStartIndex
            if (settings.TranslationTextRowStartIndex <= settings.LocaleRowIndex)
                settings.TranslationTextRowStartIndex = settings.LocaleRowIndex + 1;

            // Limit the value of TranslationTextColumnIndexRange
            if (settings.TranslationTextColumnIndexRange.x <= settings.TranslationKeyColumnIndex)
                settings.TranslationTextColumnIndexRange = new Vector2Int(settings.TranslationKeyColumnIndex + 1, settings.TranslationTextColumnIndexRange.y);

            if (settings.TranslationTextColumnIndexRange.y < settings.TranslationTextColumnIndexRange.x)
                settings.TranslationTextColumnIndexRange = new Vector2Int(settings.TranslationTextColumnIndexRange.x, settings.TranslationTextColumnIndexRange.x);

            // Limit the value of StyleColumnIndexRange
            if (settings.StyleColumnIndexRange.x <= settings.TranslationTextColumnIndexRange.y)
                settings.StyleColumnIndexRange = new Vector2Int(settings.TranslationTextColumnIndexRange.y + 1, settings.StyleColumnIndexRange.y);

            if (settings.StyleColumnIndexRange.y < settings.StyleColumnIndexRange.x)
                settings.StyleColumnIndexRange = new Vector2Int(settings.StyleColumnIndexRange.x, settings.StyleColumnIndexRange.x);

            SettingsSerializedObject.ApplyModifiedProperties();
        }

        private void DrawOtherSettingFields()
        {
            const string title = "Other Settings";
            EditorGUIStyles.DrawTitleLabel(title);

            // Target Locales
            using (new UniEditorGUILayout.FieldScope(LabelWidth))
            {
                var targetLocalesProperty = SettingsSerializedObject.FindProperty("targetLocales");
                EditorGUILayout.PropertyField(targetLocalesProperty, new GUIContent("Target Locales", "The list of Locales that only need to be built."));
            }

            EditorGUILayout.Space(2);

            // Excluded Locales
            using (new UniEditorGUILayout.FieldScope(LabelWidth))
            {
                var excludedLocalesProperty = SettingsSerializedObject.FindProperty("excludedLocales");
                EditorGUILayout.PropertyField(excludedLocalesProperty, new GUIContent("Excluded Locales", "The list of Locales that should be excluded in the build."));
            }
        }

        private void DrawCharactersTextFileExportSettingFields()
        {
            const string title = "Characters Text File Export Settings";
            EditorGUIStyles.DrawTitleLabel(title);

            const float exportToggleLabelWith = 130;

            // Export Characters Text File Toggle
            var label = new GUIContent("Enabled", "Should export characters text file with all characters in translation text content?");
            settings.CharactersFileExportPreferences.Enabled = EditorGUILayout.BeginToggleGroup(label, settings.CharactersFileExportPreferences.Enabled);
            {
                // Characters Text File Export Path
                label = new GUIContent("Export Folder Path", "The folder path to store file Characters.txt.");
                var charactersTextFileExportFolderPath = UniEditorGUILayout.FolderField(label,
                    settings.CharactersFileExportPreferences.ExportFolderPath,
                    label.text,
                    EditorPath.GetFullPath(settings.CharactersFileExportPreferences.ExportFolderPath),
                    string.Empty,
                    LabelWidth);
                settings.CharactersFileExportPreferences.ExportFolderPath = !string.IsNullOrEmpty(charactersTextFileExportFolderPath)
                    ? EditorPath.GetAssetPath(charactersTextFileExportFolderPath)
                    : string.Empty;

                label = new GUIContent("Extra Characters Options", "Options to add extra characters to export.");
                showExtraCharactersOptions = EditorGUILayout.BeginFoldoutHeaderGroup(showExtraCharactersOptions, label);
                if (showExtraCharactersOptions)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        // Add ASCII Characters.
                        using (new UniEditorGUILayout.FieldScope(exportToggleLabelWith))
                        {
                            label = new GUIContent("Add ASCII Chars", "Add extra ASCII characters to characters text file.");
                            settings.CharactersFileExportPreferences.IsAsciiCharactersRequired = EditorGUILayout.ToggleLeft(label, settings.CharactersFileExportPreferences.IsAsciiCharactersRequired);
                        }

                        // Add Extended ASCII Characters.
                        using (new UniEditorGUILayout.FieldScope(exportToggleLabelWith))
                        {
                            label = new GUIContent("Add Extended ASCII Chars", "Add extra Extended ASCII characters to characters text file.");
                            settings.CharactersFileExportPreferences.IsExtendedAsciiCharactersRequired = EditorGUILayout.ToggleLeft(label, settings.CharactersFileExportPreferences.IsExtendedAsciiCharactersRequired);
                        }

                        // Add ASCII Lowercase Characters.
                        using (new UniEditorGUILayout.FieldScope(exportToggleLabelWith))
                        {
                            label = new GUIContent("Add ASCII Lowercase Chars", "Add extra ASCII lowercase characters to characters text file.");
                            settings.CharactersFileExportPreferences.IsAsciiLowercaseCharactersRequired = EditorGUILayout.ToggleLeft(label, settings.CharactersFileExportPreferences.IsAsciiLowercaseCharactersRequired);
                        }

                        // Add ASCII Uppercase Characters.
                        using (new UniEditorGUILayout.FieldScope(exportToggleLabelWith))
                        {
                            label = new GUIContent("Add ASCII Uppercase Chars", "Add extra ASCII uppercase characters to characters text file.");
                            settings.CharactersFileExportPreferences.IsAsciiUppercaseCharactersRequired = EditorGUILayout.ToggleLeft(label, settings.CharactersFileExportPreferences.IsAsciiUppercaseCharactersRequired);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    {
                        // Add Numbers and Symbols Characters.
                        using (new UniEditorGUILayout.FieldScope(exportToggleLabelWith))
                        {
                            label = new GUIContent("Add Numbers & Symbols Chars", "Add extra numbers and symbols characters to characters text file.");
                            settings.CharactersFileExportPreferences.IsNumbersAndSymbolsCharactersRequired = EditorGUILayout.ToggleLeft(label, settings.CharactersFileExportPreferences.IsNumbersAndSymbolsCharactersRequired);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.EndFoldoutHeaderGroup();
                
                // Add Custom Characters.
                using (new UniEditorGUILayout.FieldScope(exportToggleLabelWith))
                {
                    label = new GUIContent("Add Custom Chars", "Add custom characters to characters text file.");
                    settings.CharactersFileExportPreferences.IsCustomCharactersRequired = EditorGUILayout.ToggleLeft(label, settings.CharactersFileExportPreferences.IsCustomCharactersRequired);
                }
                
                if (EditorGUILayout.BeginFadeGroup(settings.CharactersFileExportPreferences.IsCustomCharactersRequired ? 1 : 0))
                {
                    customCharactersTextAreaScrollPosition = EditorGUILayout.BeginScrollView(customCharactersTextAreaScrollPosition, GUILayout.Height(90));
                    {
                        settings.CharactersFileExportPreferences.CustomCharacters = EditorGUILayout.TextArea(settings.CharactersFileExportPreferences.CustomCharacters, GUILayout.ExpandHeight(true));
                    }
                    EditorGUILayout.EndScrollView();
                }
                EditorGUILayout.EndFadeGroup();
            }

            EditorGUILayout.EndToggleGroup();

            // Save preferences data if data is dirty.
            settings.SaveOnCharactersTextFileExportPreferencesDirty();
        }

        private void DrawBuildAssetsButton()
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Build Localization Assets", GUILayout.Width(LabelWidth), GUILayout.Height(25)))
                    BuildAssets();
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}