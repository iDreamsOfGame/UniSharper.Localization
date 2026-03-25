// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Linq;
using UniSharper;
using UnityEngine;

namespace UniSharperEditor.Localization
{
    internal static class PresetCharacterSets
    {
        private const string GeneralStandardChineseCharactersLevel1TextFileName = "General Standard Chinese Characters Level1";
        
        private const string GeneralStandardChineseCharactersLevel2TextFileName = "General Standard Chinese Characters Level2";
        
        private const string GeneralStandardChineseCharactersLevel3TextFileName = "General Standard Chinese Characters Level3";

        public static readonly string AsciiCharacters = new(Enumerable.Range(0, 128).Select(c => (char)c).ToArray());

        public static readonly string ExtendedAsciiCharacters = new(Enumerable.Range(0, 256).Select(c => (char)c).ToArray());

        public static readonly string AsciiLowercaseCharacters = new(Enumerable.Range(97, 26).Select(c => (char)c).ToArray());

        public static readonly string AsciiUppercaseCharacters = new(Enumerable.Range(65, 26).Select(c => (char)c).ToArray());
        
        private static string numbersAndSymbolsCharacters;

        private static string generalStandardChineseCharactersLevel1;
        
        private static string generalStandardChineseCharactersLevel2;
        
        private static string generalStandardChineseCharactersLevel3;
        
        public static string NumbersAndSymbolsCharacters
        {
            get
            {
                if (numbersAndSymbolsCharacters != null)
                    return numbersAndSymbolsCharacters;

                numbersAndSymbolsCharacters = new string(ExtendedAsciiCharacters.Where(ch => char.IsDigit(ch) || char.IsSymbol(ch)).ToArray());
                return numbersAndSymbolsCharacters;
            }
        }

        public static string GeneralStandardChineseCharactersLevel1
        {
            get
            {
                if (generalStandardChineseCharactersLevel1 != null)
                    return generalStandardChineseCharactersLevel1;
                
                generalStandardChineseCharactersLevel1 = LoadCharactersTextFile(GeneralStandardChineseCharactersLevel1TextFileName);
                return generalStandardChineseCharactersLevel1;
            }
        }
        
        public static string GeneralStandardChineseCharactersLevel2
        {
            get
            {
                if (generalStandardChineseCharactersLevel2 != null)
                    return generalStandardChineseCharactersLevel2;
                
                generalStandardChineseCharactersLevel2 = LoadCharactersTextFile(GeneralStandardChineseCharactersLevel2TextFileName);
                return generalStandardChineseCharactersLevel2;
            }
        }
        
        public static string GeneralStandardChineseCharactersLevel3
        {
            get
            {
                if (generalStandardChineseCharactersLevel3 != null)
                    return generalStandardChineseCharactersLevel3;
                
                generalStandardChineseCharactersLevel3 = LoadCharactersTextFile(GeneralStandardChineseCharactersLevel3TextFileName);
                return generalStandardChineseCharactersLevel3;
            }
        }

        private static string LoadCharactersTextFile(string fileName)
        {
            // Search '/Assets' path.
            var textAssets = UniAssetDatabase.LoadEditorResources<TextAsset>(fileName);
            if (textAssets is { Length: > 0 })
                return textAssets[0].text;
            
            // Search package path.
            const string packagePath = PlayerEnvironment.PackagesFolderName + "/" + PackageInfo.UnityPackageName;
            textAssets = UniAssetDatabase.LoadEditorResources<TextAsset>(fileName, packagePath);
            return textAssets is { Length: > 0 } ? textAssets[0].text : string.Empty;
        }
    }
}