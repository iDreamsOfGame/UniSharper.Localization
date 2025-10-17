// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.IO;
using ReSharp.Security.Cryptography;
using UniSharperEditor.Extensions;

namespace UniSharperEditor.Localization.FontTools
{
    internal static class FontSubsetCreatorSettings
    {
        private static readonly string EditorPrefsKeySuffix = $"{CryptoUtility.Md5HashEncrypt(Directory.GetCurrentDirectory(), null, false)}"
                                                              + $".{typeof(FontSubsetCreatorSettings).FullName}";

        private static readonly string SourceFontFilePathEditorPrefsKey = $"{EditorPrefsKeySuffix}.{nameof(SourceFontFilePath)}";

        private static readonly string FontSubsetFolderPathEditorPrefsKey = $"{EditorPrefsKeySuffix}.{nameof(FontSubsetFolderPath)}";

        private static readonly string CharacterSetTypeIntEditorPrefsKey = $"{EditorPrefsKeySuffix}.{nameof(CharacterSetTypeInt)}";

        private static readonly string CharacterSetCustomContentEditorPrefsKey = $"{EditorPrefsKeySuffix}.{nameof(CharacterSetCustomContent)}";

        private static readonly string CharacterSetFilePathEditorPrefsKey = $"{EditorPrefsKeySuffix}.{nameof(CharacterSetFilePath)}";
        
        private static readonly string CustomFontSubsetFileNameEnabledEditorPrefsKey = $"{EditorPrefsKeySuffix}.{nameof(CustomFontSubsetFileNameEnabled)}";
        
        private static readonly string FontSubsetFileNameEditorPrefsKey = $"{EditorPrefsKeySuffix}.{nameof(FontSubsetFileName)}";
        
        public static string SourceFontFilePath
        {
            get => EditorPrefsUtility.GetString(SourceFontFilePathEditorPrefsKey, string.Empty);
            set
            {
                if (value == null || SourceFontFilePath.Equals(value))
                    return;

                EditorPrefsUtility.SetString(SourceFontFilePathEditorPrefsKey, value);

                // Auto generate font subset file name.
                if (!string.IsNullOrEmpty(value) && !CustomFontSubsetFileNameEnabled)
                    FontSubsetFileName = $"{Path.GetFileNameWithoutExtension(value)}-Subset{Path.GetExtension(value)}";
            }
        }

        public static string FontSubsetFolderPath
        {
            get => EditorPrefsUtility.GetString(FontSubsetFolderPathEditorPrefsKey, string.Empty);
            set
            {
                if (string.IsNullOrEmpty(value) || FontSubsetFolderPath.Equals(value))
                    return;

                EditorPrefsUtility.SetString(FontSubsetFolderPathEditorPrefsKey, value);
            }
        }

        public static int CharacterSetTypeInt
        {
            get => EditorPrefsUtility.GetInt32(CharacterSetTypeIntEditorPrefsKey);
            set
            {
                if (CharacterSetTypeInt == value)
                    return;

                EditorPrefsUtility.SetInt32(CharacterSetTypeIntEditorPrefsKey, value);
            }
        }

        public static CharacterSetType CharacterSetType => (CharacterSetType)CharacterSetTypeInt;

        public static string CharacterSetCustomContent
        {
            get => EditorPrefsUtility.GetString(CharacterSetCustomContentEditorPrefsKey, string.Empty);
            set
            {
                if (value == null || CharacterSetCustomContent.Equals(value))
                    return;

                EditorPrefsUtility.SetString(CharacterSetCustomContentEditorPrefsKey, value);
            }
        }

        public static string CharacterSetFilePath
        {
            get => EditorPrefsUtility.GetString(CharacterSetFilePathEditorPrefsKey, string.Empty);
            set
            {
                if (value == null || CharacterSetFilePath.Equals(value))
                    return;

                EditorPrefsUtility.SetString(CharacterSetFilePathEditorPrefsKey, value);
            }
        }
        
        public static bool CustomFontSubsetFileNameEnabled
        {
            get => EditorPrefsUtility.GetBoolean(CustomFontSubsetFileNameEnabledEditorPrefsKey);
            set
            {
                var oldValue = CustomFontSubsetFileNameEnabled;
                if (oldValue.Equals(value))
                    return;
                
                EditorPrefsUtility.SetBoolean(CustomFontSubsetFileNameEnabledEditorPrefsKey, value);

                // Resets font subset file name.
                if (!oldValue && value)
                    FontSubsetFileName = string.Empty;
                
                if (oldValue && !value && !string.IsNullOrEmpty(SourceFontFilePath))
                    FontSubsetFileName = $"{Path.GetFileNameWithoutExtension(SourceFontFilePath)}-Subset{Path.GetExtension(SourceFontFilePath)}";
            }
        }

        public static string FontSubsetFileName
        {
            get => EditorPrefsUtility.GetString(FontSubsetFileNameEditorPrefsKey, string.Empty);
            set
            {
                if (value == null || FontSubsetFileName.Equals(value))
                    return;

                EditorPrefsUtility.SetString(FontSubsetFileNameEditorPrefsKey, value);
            }
        }
    }
}