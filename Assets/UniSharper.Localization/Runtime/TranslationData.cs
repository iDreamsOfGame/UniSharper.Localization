// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using MemoryPack;
using UnityEngine.Scripting;

// ReSharper disable InvertIf

namespace UniSharper.Localization
{
    /// <summary>
    /// Translation data for locale.
    /// </summary>
    [MemoryPackable]
    public partial class TranslationData
    {
        /// <summary>
        /// The default translation text.
        /// </summary>
        public const string DefaultText = "No String";
        
        /// <summary>
        /// Initializes a new instance of the TranslationData class.
        /// </summary>
        [Preserve]
        public TranslationData()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the TranslationData class.
        /// </summary>
        /// <param name="text">The translation text. </param>
        /// <param name="font">The font of translation text. </param>
        /// <param name="style">Styling information for text. </param>
        [MemoryPackConstructor]
        internal TranslationData(string text, Dictionary<string, string> style = null)
        {
            Text = text;
            Style = style;
        }
        
        /// <summary>
        /// The translation text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Styling information for text.
        /// </summary>
        public Dictionary<string, string> Style { get; set; }

        /// <summary>
        /// Get the parameter value of style.
        /// </summary>
        /// <param name="key">The name of parameter of style. </param>
        /// <returns>The parameter value of style. </returns>
        public string GetStyleParameter(string key)
        {
            if (Style == null || string.IsNullOrEmpty(key))
                return string.Empty;

            return Style.TryGetValue(key, out var value) ? value : string.Empty;
        }

        /// <summary>
        /// Tries to get the parameter value of style.
        /// </summary>
        /// <param name="key">The name of parameter of style. </param>
        /// <param name="value">The parameter value of style. </param>
        /// <returns><c>true</c> if the style parameters contains an element with the specified key; otherwise, <c>false</c>. </returns>
        public bool TryGetStyleParameter(string key, out string value)
        {
            if (Style == null || string.IsNullOrEmpty(key))
            {
                value = string.Empty;
                return false;
            }

            return Style.TryGetValue(key, out value);
        }
    }
}