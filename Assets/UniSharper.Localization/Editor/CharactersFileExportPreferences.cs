// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.IO;
using UnityEngine;

namespace UniSharperEditor.Localization
{
    /// <summary>
    /// Class used to get and set the preferences to export characters text file.
    /// </summary>
    [Serializable]
    public class CharactersFileExportPreferences
    {
        private const string CharactersTextFileName = "Characters.txt";
        
        [SerializeField]
        private bool enabled;

        [SerializeField]
        private string exportFolderPath = string.Empty;
        
        [SerializeField]
        private ExtraCharacterOptions extraCharacterOptions;
        
        [SerializeField]
        private string customCharacters = string.Empty;

        internal bool IsDirty { get; set; }

        internal bool Enabled
        {
            get => enabled;
            set
            {
                if (enabled.Equals(value))
                    return;

                enabled = value;
                IsDirty = true;
            }
        }
        
        internal string ExportFolderPath
        {
            get => exportFolderPath;
            set
            {
                if (string.IsNullOrEmpty(value) || exportFolderPath.Equals(value))
                    return;

                exportFolderPath = value;
                IsDirty = true;
            }
        }

        internal string ExportPath => Path.Combine(ExportFolderPath, CharactersTextFileName);

        internal ExtraCharacterOptions ExtraCharacterOptions
        {
            get => extraCharacterOptions;
            set
            {
                if (extraCharacterOptions == value)
                    return;

                extraCharacterOptions = value;
                IsDirty = true;
            }
        }

        internal bool ShouldIncludeAsciiCharacters => ExtraCharacterOptions.HasFlag(ExtraCharacterOptions.Ascii);

        internal bool ShouldIncludeExtendedAsciiCharacters => ExtraCharacterOptions.HasFlag(ExtraCharacterOptions.ExtendedAscii);

        internal bool ShouldIncludeAsciiLowercaseCharacters => ExtraCharacterOptions.HasFlag(ExtraCharacterOptions.AsciiLowercase);

        internal bool ShouldIncludeAsciiUppercaseCharacters => ExtraCharacterOptions.HasFlag(ExtraCharacterOptions.AsciiUppercase);

        internal bool ShouldIncludeNumbersAndSymbolsCharacters => ExtraCharacterOptions.HasFlag(ExtraCharacterOptions.NumbersAndSymbols);

        internal bool ShouldIncludeGeneralStandardChineseCharactersLevel1 => 
            ExtraCharacterOptions.HasFlag(ExtraCharacterOptions.GeneralStandardChineseCharactersLevel1);

        internal bool ShouldIncludeGeneralStandardChineseCharactersLevel2 =>
            ExtraCharacterOptions.HasFlag(ExtraCharacterOptions.GeneralStandardChineseCharactersLevel2);

        internal bool ShouldIncludeGeneralStandardChineseCharactersLevel3 =>
            ExtraCharacterOptions.HasFlag(ExtraCharacterOptions.GeneralStandardChineseCharactersLevel3);
        
        internal bool ShouldIncludeCustomCharacters => ExtraCharacterOptions.HasFlag(ExtraCharacterOptions.CustomCharacters);
        
        internal string CustomCharacters
        {
            get => customCharacters;
            set
            {
                if (!string.IsNullOrEmpty(customCharacters) && customCharacters == value)
                    return;
                
                customCharacters = value;
                IsDirty = true;
            }
        }
    }
}