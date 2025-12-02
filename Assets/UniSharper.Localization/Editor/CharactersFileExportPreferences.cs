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
        private bool isAsciiCharactersRequired;
        
        [SerializeField]
        private bool isExtendedAsciiCharactersRequired;
        
        [SerializeField]
        private bool isAsciiLowercaseCharactersRequired;
        
        [SerializeField]
        private bool isAsciiUppercaseCharactersRequired;
        
        [SerializeField]
        private bool isNumbersAndSymbolsCharactersRequired;

        [SerializeField]
        private bool isCustomCharactersRequired;
        
        [SerializeField]
        private string customCharacters;

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

        internal bool IsAsciiCharactersRequired
        {
            get => isAsciiCharactersRequired;
            set
            {
                if (isAsciiCharactersRequired.Equals(value))
                    return;

                isAsciiCharactersRequired = value;
                IsDirty = true;
            }
        }
        
        internal bool IsExtendedAsciiCharactersRequired
        {
            get => isExtendedAsciiCharactersRequired;
            set
            {
                if (isExtendedAsciiCharactersRequired.Equals(value))
                    return;

                isExtendedAsciiCharactersRequired = value;
                IsDirty = true;
            }
        }

        internal bool IsAsciiLowercaseCharactersRequired
        {
            get => isAsciiLowercaseCharactersRequired;
            set
            {
                if (isAsciiLowercaseCharactersRequired.Equals(value))
                    return;
                
                isAsciiLowercaseCharactersRequired = value;
                IsDirty = true;
            }
        }
        
        internal bool IsAsciiUppercaseCharactersRequired
        {
            get => isAsciiUppercaseCharactersRequired;
            set
            {
                if (isAsciiUppercaseCharactersRequired.Equals(value))
                    return;
                
                isAsciiUppercaseCharactersRequired = value;
                IsDirty = true;
            }
        }
        
        internal bool IsNumbersAndSymbolsCharactersRequired
        {
            get => isNumbersAndSymbolsCharactersRequired;
            set
            {
                if (isNumbersAndSymbolsCharactersRequired.Equals(value))
                    return;
                
                isNumbersAndSymbolsCharactersRequired = value;
                IsDirty = true;
            }
        }
        
        internal bool IsCustomCharactersRequired
        {
            get => isCustomCharactersRequired;
            set
            {
                if (isCustomCharactersRequired.Equals(value))
                    return;
                
                isCustomCharactersRequired = value;
                IsDirty = true;
            }
        }
        
        internal string CustomCharacters
        {
            get => customCharacters;
            set
            {
                if (customCharacters.Equals(value))
                    return;
                
                customCharacters = value;
                IsDirty = true;
            }
        }
    }
}