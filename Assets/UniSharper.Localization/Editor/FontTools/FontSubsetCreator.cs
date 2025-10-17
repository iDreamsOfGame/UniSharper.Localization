using System;
using System.Collections.Generic;
using System.IO;
using HarfBuzzSharp;
using ReSharp.Security.Cryptography;
using SkiaSharp.FontSubset;
using UniSharperEditor.Extensions;
using UnityEditor;
using UnityEngine;
using Font = HarfBuzzSharp.Font;

namespace UniSharperEditor.Localization.FontTools
{
    internal class FontSubsetCreator
    {
        private const float Padding = 10;

        private const float LabelWidth = 175f;

        private static readonly string EditorPrefsKeySuffix = $"{CryptoUtility.Md5HashEncrypt(Directory.GetCurrentDirectory(), null, false)}.{PlayerSettings.productName}.{typeof(FontSubsetCreator).FullName}";

        private static readonly string SourceFontFilePathEditorPrefsKey = $"{EditorPrefsKeySuffix}.{nameof(SourceFontFilePath)}";
        
        private static readonly string FontSubsetFolderPathEditorPrefsKey = $"{EditorPrefsKeySuffix}.{nameof(FontSubsetFolderPath)}";
        
        private static readonly string CharactersSetTextFilePathEditorPrefsKey = $"{EditorPrefsKeySuffix}.{nameof(CharactersSetTextFilePath)}";
        
        private static string SourceFontFilePath
        {
            get => EditorPrefsUtility.GetString(SourceFontFilePathEditorPrefsKey, string.Empty);
            set
            {
                if (string.IsNullOrEmpty(value) || SourceFontFilePath.Equals(value))
                    return;
                
                EditorPrefsUtility.SetString(SourceFontFilePathEditorPrefsKey, value);
            }
        }

        private static string FontSubsetFolderPath
        {
            get => EditorPrefsUtility.GetString(FontSubsetFolderPathEditorPrefsKey, string.Empty);
            set
            {
                if (string.IsNullOrEmpty(value) || FontSubsetFolderPath.Equals(value))
                    return;
                
                EditorPrefsUtility.SetString(FontSubsetFolderPathEditorPrefsKey, value);
            }
        }

        private static string CharactersSetTextFilePath
        {
            get => EditorPrefsUtility.GetString(CharactersSetTextFilePathEditorPrefsKey, string.Empty);
            set
            {
                if (string.IsNullOrEmpty(value) || CharactersSetTextFilePath.Equals(value))
                    return;
                
                EditorPrefsUtility.SetString(CharactersSetTextFilePathEditorPrefsKey, value);
            }
        }
        
        public void DrawEditorGui(EditorWindow window)
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.Space(Padding);

                EditorGUILayout.BeginVertical();
                {
                    // Settings
                    DrawSettingsFields();

                    EditorGUILayout.Space(10);

                    // Generate button
                    DrawGenerateButton();
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.Space(Padding);
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawSettingsFields()
        {
            const string title = "Settings";
            EditorGUIStyles.DrawTitleLabel(title);
            
            SourceFontFilePath = UniEditorGUILayout.FileField(new GUIContent("Source Font File Path", "Where to locate source font file."),
                SourceFontFilePath,
                "Select Source Font File",
                string.Empty,
                new[] { "Font Files", "ttf,otf" },
                LabelWidth);
            
            FontSubsetFolderPath = UniEditorGUILayout.FolderField(new GUIContent("Font Subset Folder Path", "Where to store font subset file."),
                FontSubsetFolderPath,
                "Font Subset Folder Path",
                string.Empty,
                string.Empty,
                LabelWidth);
            
            CharactersSetTextFilePath = UniEditorGUILayout.FileField(new GUIContent("Characters Set Text File Path", "Where to locate the text file of characters set in font subset."),
                CharactersSetTextFilePath,
                "Select Characters Set Text File",
                string.Empty,
                new[] { "Text File", "txt" },
                LabelWidth);
        }

        private void DrawGenerateButton()
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Generate Font Subset File", GUILayout.Width(LabelWidth), GUILayout.Height(25)))
                    GenerateFontSubsetFile();
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
        }

        private async void GenerateFontSubsetFile()
        {
            try
            {
                if (string.IsNullOrEmpty(SourceFontFilePath))
                {
                    EditorUtility.DisplayDialog("Error", "Please select source font file.", "OK");
                    return;
                }
                
                if (string.IsNullOrEmpty(FontSubsetFolderPath))
                {
                    EditorUtility.DisplayDialog("Error", "Please select font subset folder path.", "OK");
                    return;
                }
                
                if (string.IsNullOrEmpty(CharactersSetTextFilePath))
                {
                    EditorUtility.DisplayDialog("Error", "Please select characters set text file.", "OK");
                    return;
                }
                
                // Initialize font subset builder.
                var blob = Blob.FromFile(SourceFontFilePath);
                var font = new Font(new Face(blob, 0));
                var builder = new FontSubsetBuilder();
                builder.SetFont(font);

                var chars = await File.ReadAllTextAsync(CharactersSetTextFilePath);
                var glyphs = new HashSet<uint>();
                if (chars.Length == 0)
                {
                    EditorUtility.DisplayDialog("Error", "No content in characters set text file!", "OK");
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

                // Generate font subset file.
                var sourceFontFileName = Path.GetFileNameWithoutExtension(SourceFontFilePath);
                var sourceFontFileExtension = Path.GetExtension(SourceFontFilePath);
                var fontSubsetFilePath = Path.Combine(FontSubsetFolderPath, $"{sourceFontFileName}-Subset{sourceFontFileExtension}");
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
    }
}