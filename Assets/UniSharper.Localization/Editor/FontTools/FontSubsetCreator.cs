// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HarfBuzzSharp;
using SkiaSharp.FontSubset;
using UnityEditor;
using UnityEngine;
using Font = HarfBuzzSharp.Font;

namespace UniSharperEditor.Localization.FontTools
{
    internal class FontSubsetCreator
    {
        private const float Padding = 10;

        private const float LabelWidth = 175f;

        private static readonly string[] CharacterSetDisplayOptions = { "ASCII", "Extended ASCII", "ASCII Lowercase", "ASCII Uppercase", "Numbers + Symbols", "Custom Characters", "Characters from File" };

        private bool isFirstDrawGui = true;

        public void DrawEditorGui()
        {
            if (isFirstDrawGui)
            {
                // Verify settings.
                if (!string.IsNullOrEmpty(FontSubsetCreatorSettings.SourceFontFilePath) && !File.Exists(FontSubsetCreatorSettings.SourceFontFilePath))
                    FontSubsetCreatorSettings.SourceFontFilePath = string.Empty;

                if (!string.IsNullOrEmpty(FontSubsetCreatorSettings.FontSubsetFolderPath) && !Directory.Exists(FontSubsetCreatorSettings.FontSubsetFolderPath))
                    FontSubsetCreatorSettings.FontSubsetFolderPath = string.Empty;

                if (!string.IsNullOrEmpty(FontSubsetCreatorSettings.CharacterSetFilePath) && !File.Exists(FontSubsetCreatorSettings.CharacterSetFilePath))
                    FontSubsetCreatorSettings.CharacterSetFilePath = string.Empty;

                isFirstDrawGui = false;
            }

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.Space(Padding);

                EditorGUILayout.BeginVertical();
                {
                    // Settings
                    DrawSettingsFields();

                    EditorGUILayout.Space(10);

                    // Create button
                    DrawCreateButton();
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.Space(Padding);
            }
            EditorGUILayout.EndHorizontal();
        }

        private static void DrawSettingsFields()
        {
            const string title = "Settings";
            EditorGUIStyles.DrawTitleLabel(title);

            FontSubsetCreatorSettings.SourceFontFilePath = UniEditorGUILayout.FileField(new GUIContent("Source Font File Path", "Where to locate source font file."),
                FontSubsetCreatorSettings.SourceFontFilePath,
                "Select Source Font File",
                string.Empty,
                new[] { "Font Files", "ttf,otf" },
                LabelWidth);

            FontSubsetCreatorSettings.FontSubsetFolderPath = UniEditorGUILayout.FolderField(new GUIContent("Font Subset Folder Path", "Where to store font subset file."),
                FontSubsetCreatorSettings.FontSubsetFolderPath,
                "Font Subset Folder Path",
                string.Empty,
                string.Empty,
                LabelWidth);

            using (new UniEditorGUILayout.FieldScope(LabelWidth))
            {
                var characterSetTypeBeforeChange = (CharacterSetType)FontSubsetCreatorSettings.CharacterSetTypeInt;
                FontSubsetCreatorSettings.CharacterSetTypeInt = EditorGUILayout.Popup("Character Set", FontSubsetCreatorSettings.CharacterSetTypeInt, CharacterSetDisplayOptions);

                // Resets character set content when character set type changed.
                if (characterSetTypeBeforeChange is not CharacterSetType.CustomCharacters && FontSubsetCreatorSettings.CharacterSetType is CharacterSetType.CustomCharacters)
                    FontSubsetCreatorSettings.CharacterSetCustomContent = string.Empty;

                // Resets character set file path when character set type changed.
                if (characterSetTypeBeforeChange is not CharacterSetType.CharactersFromFile && FontSubsetCreatorSettings.CharacterSetType is CharacterSetType.CharactersFromFile)
                    FontSubsetCreatorSettings.CharacterSetFilePath = string.Empty;
            }

            switch (FontSubsetCreatorSettings.CharacterSetTypeInt)
            {
                case (int)CharacterSetType.CustomCharacters:
                {
                    using (new UniEditorGUILayout.FieldScope(LabelWidth))
                    {
                        FontSubsetCreatorSettings.CharacterSetCustomContent = EditorGUILayout.TextArea(FontSubsetCreatorSettings.CharacterSetCustomContent, GUILayout.Height(90));
                    }

                    break;
                }
                case (int)CharacterSetType.CharactersFromFile:
                    FontSubsetCreatorSettings.CharacterSetFilePath = UniEditorGUILayout.FileField(new GUIContent("Character Set File Path", "Where to locate the file of character set for font subset."),
                        FontSubsetCreatorSettings.CharacterSetFilePath,
                        "Select Character Set File",
                        string.Empty,
                        new[] { "Text File", "txt" },
                        LabelWidth);
                    break;
            }
            
            var label = new GUIContent("Custom Font Subset File Name", "Custom font subset file name or not?");
            FontSubsetCreatorSettings.CustomFontSubsetFileNameEnabled = EditorGUILayout.BeginToggleGroup(label, FontSubsetCreatorSettings.CustomFontSubsetFileNameEnabled);
            using (new UniEditorGUILayout.FieldScope(LabelWidth))
            {
                label = new GUIContent("Font Subset File Name", "The file name for font subset file to be created.");
                FontSubsetCreatorSettings.FontSubsetFileName = EditorGUILayout.TextField(label, FontSubsetCreatorSettings.FontSubsetFileName);
            }
            EditorGUILayout.EndToggleGroup();
        }

        private static void DrawCreateButton()
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Create Font Subset File", GUILayout.Width(LabelWidth), GUILayout.Height(25)))
                    CreateFontSubsetFile();
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
        }

        private static async void CreateFontSubsetFile()
        {
            try
            {
                if (string.IsNullOrEmpty(FontSubsetCreatorSettings.SourceFontFilePath))
                {
                    EditorUtility.DisplayDialog("Error", "Please select source font file.", "OK");
                    return;
                }

                if (string.IsNullOrEmpty(FontSubsetCreatorSettings.FontSubsetFolderPath))
                {
                    EditorUtility.DisplayDialog("Error", "Please select font subset folder path.", "OK");
                    return;
                }

                if (FontSubsetCreatorSettings.CharacterSetType is CharacterSetType.CustomCharacters && string.IsNullOrEmpty(FontSubsetCreatorSettings.CharacterSetCustomContent))
                {
                    EditorUtility.DisplayDialog("Error", "No custom content for character set.", "OK");
                    return;
                }

                if (FontSubsetCreatorSettings.CharacterSetType is CharacterSetType.CharactersFromFile && string.IsNullOrEmpty(FontSubsetCreatorSettings.CharacterSetFilePath))
                {
                    EditorUtility.DisplayDialog("Error", "Please select characters set text file.", "OK");
                    return;
                }

                // Initialize font subset builder.
                var blob = Blob.FromFile(FontSubsetCreatorSettings.SourceFontFilePath);
                var font = new Font(new Face(blob, 0));
                var builder = new FontSubsetBuilder();
                builder.SetFont(font);

                var chars = await GetCharacterSet();
                var glyphs = new HashSet<uint>();
                if (chars.Length == 0)
                {
                    EditorUtility.DisplayDialog("Error", "No character set!", "OK");
                    return;
                }

                foreach (var ch in chars)
                {
                    var unicode = (uint)ch;
                    if (font.TryGetGlyph(unicode, out var glyph))
                    {
                        glyphs.Add(glyph);
                    }
                    else
                    {
                        Debug.LogWarning($"Glyph for character '{ch}' not found in source font file!");
                    }
                }

                // Adds glyphs.
                if (glyphs.Count == 0)
                {
                    EditorUtility.DisplayDialog("Error", "No glyph to add in font subset file!", "OK");
                    return;
                }

                builder.AddGlyphs(glyphs);

                // Verifies font subset file extension.
                var extension = Path.GetExtension(FontSubsetCreatorSettings.SourceFontFilePath);
                var fontSubsetFileName = FontSubsetCreatorSettings.FontSubsetFileName;
                if (!fontSubsetFileName.EndsWith(extension))
                    fontSubsetFileName += extension;
                    
                // Creates font subset file.
                var fontSubsetFilePath = Path.Combine(FontSubsetCreatorSettings.FontSubsetFolderPath, fontSubsetFileName);
                EditorUtility.DisplayProgressBar("Hold on...", "Generating font subset file...", 0.5f);
                await File.WriteAllBytesAsync(fontSubsetFilePath, builder.Build());
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog("Success", "Font subset file has been generated successfully!", "OK");
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        private static async Task<char[]> GetCharacterSet()
        {
            switch (FontSubsetCreatorSettings.CharacterSetType)
            {
                default:
                case CharacterSetType.Ascii:
                    return PresetCharacterSets.AllAsciiCharacters;

                case CharacterSetType.ExtendedAscii:
                    return PresetCharacterSets.AllExtendedAsciiCharacters;

                case CharacterSetType.AsciiLowercase:
                    return PresetCharacterSets.AllAsciiLowercaseCharacters;

                case CharacterSetType.AsciiUppercase:
                    return PresetCharacterSets.AllAsciiUppercaseCharacters;

                case CharacterSetType.NumbersAndSymbols:
                    return PresetCharacterSets.AllNumbersAndSymbolsCharacters;

                case CharacterSetType.CustomCharacters:
                    return !string.IsNullOrEmpty(FontSubsetCreatorSettings.CharacterSetCustomContent)
                        ? FontSubsetCreatorSettings.CharacterSetCustomContent.ToCharArray()
                        : Array.Empty<char>();

                case CharacterSetType.CharactersFromFile:
                {
                    var content = await File.ReadAllTextAsync(FontSubsetCreatorSettings.CharacterSetFilePath);
                    return !string.IsNullOrEmpty(content) ? content.ToCharArray() : Array.Empty<char>();
                }
            }
        }
    }
}