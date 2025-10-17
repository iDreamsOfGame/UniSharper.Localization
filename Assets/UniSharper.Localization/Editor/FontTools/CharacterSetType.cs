// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace UniSharperEditor.Localization.FontTools
{
    internal enum CharacterSetType
    {
        /// <summary>
        /// All ASCII  characters.
        /// </summary>
        Ascii = 0,
        
        /// <summary>
        /// All extended ASCII characters.
        /// </summary>
        ExtendedAscii,
        
        /// <summary>
        /// All ASCII lowercase characters.
        /// </summary>
        AsciiLowercase,
        
        /// <summary>
        /// All ASCII uppercase characters.
        /// </summary>
        AsciiUppercase,
        
        /// <summary>
        /// All numbers and symbols.
        /// </summary>
        NumbersAndSymbols,
        
        /// <summary>
        /// Custom characters.
        /// </summary>
        CustomCharacters,
        
        /// <summary>
        /// Characters from a file.
        /// </summary>
        CharactersFromFile
    }
}