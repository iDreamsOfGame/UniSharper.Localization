// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
// ReSharper disable InvertIf

namespace UniSharper.Localization
{
    /// <summary>
    /// Translation data for locale.
    /// </summary>
    [Serializable]
    public class TranslationData : ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the TranslationData class.
        /// </summary>
        /// <param name="text">The translation text. </param>
        /// <param name="font">The font of translation text. </param>
        /// <param name="style">Styling information for text. </param>
        public TranslationData(string text, Dictionary<string, string> style = null)
        {
            Text = text;
            Style = style;
        }

        /// <summary>
        /// Initializes a new instance of the TranslationData class.
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data. </param>
        /// <param name="context">The destination (see StreamingContext) for this serialization. </param>
        public TranslationData(SerializationInfo info, StreamingContext context)
        {
            Text = info.GetString("t");
            Style = info.GetValue("s", typeof(Dictionary<string, string>)) as Dictionary<string, string>;
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

        /// <summary>
        /// Populates a SerializationInfo with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data. </param>
        /// <param name="context">The destination (see StreamingContext) for this serialization. </param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("t", Text);
            info.AddValue("s", Style);
        }
    }
}