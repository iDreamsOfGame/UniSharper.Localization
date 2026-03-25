// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace UniSharperEditor.Localization
{
    [Flags]
    public enum ExtraCharacterOptions
    {
        /// <summary>
        /// Default option.
        /// </summary>
        None = 0,
        
        /// <summary>
        /// All ASCII  characters.
        /// </summary>
        Ascii = 1 << 0,
        
        /// <summary>
        /// All extended ASCII characters.
        /// </summary>
        ExtendedAscii = 1 << 1,
        
        /// <summary>
        /// All ASCII lowercase characters.
        /// </summary>
        AsciiLowercase = 1 << 2,
        
        /// <summary>
        /// All ASCII uppercase characters.
        /// </summary>
        AsciiUppercase = 1 << 3,
        
        /// <summary>
        /// All numbers and symbols.
        /// </summary>
        NumbersAndSymbols = 1 << 4,
        
        /// <summary>
        /// General Standard Chinese Characters Level 1.
        /// </summary>
        GeneralStandardChineseCharactersLevel1 = 1 << 5,
        
        /// <summary>
        /// General Standard Chinese Characters Level 2.
        /// </summary>
        GeneralStandardChineseCharactersLevel2 = 1 << 6,
        
        /// <summary>
        /// General Standard Chinese Characters Level 3.
        /// </summary>
        GeneralStandardChineseCharactersLevel3 = 1 << 7,
        
        /// <summary>
        /// Custom characters.
        /// </summary>
        CustomCharacters = 1 << 8,
        
        /// <summary>
        /// All options.
        /// </summary>
        All = ~0
    }
}