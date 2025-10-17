// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Linq;

namespace UniSharperEditor.Localization.FontTools
{
    internal static class PresetCharacterSets
    {
        public static readonly char[] AllAsciiCharacters = Enumerable.Range(0, 128).Select(c => (char)c).ToArray();

        public static readonly char[] AllExtendedAsciiCharacters = Enumerable.Range(0, 256).Select(c => (char)c).ToArray();

        public static readonly char[] AllAsciiLowercaseCharacters = Enumerable.Range(97, 26).Select(c => (char)c).ToArray();

        public static readonly char[] AllAsciiUppercaseCharacters = Enumerable.Range(65, 26).Select(c => (char)c).ToArray();
        
        private static char[] allNumbersAndSymbolsCharacters;
        
        public static char[] AllNumbersAndSymbolsCharacters
        {
            get
            {
                if (allNumbersAndSymbolsCharacters != null)
                    return allNumbersAndSymbolsCharacters;

                allNumbersAndSymbolsCharacters = AllExtendedAsciiCharacters.Where(ch => char.IsDigit(ch) || char.IsSymbol(ch)).ToArray();
                return allNumbersAndSymbolsCharacters;
            }
        }
    }
}