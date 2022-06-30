// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Runtime.Serialization;

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
        public TranslationData(string text, string font = null, string[] style = null)
        {
            Text = text;
            Font = font;
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
            Font = info.GetString("f");

            var style = Array.Empty<string>();
            Style = info.GetValue("s", style.GetType()) as string[];
        }
        
        /// <summary>
        /// The translation text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The font of translation text.
        /// </summary>
        public string Font { get; set; }

        /// <summary>
        /// Styling information for text.
        /// </summary>
        public string[] Style { get; set; }

        /// <summary>
        /// Populates a SerializationInfo with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data. </param>
        /// <param name="context">The destination (see StreamingContext) for this serialization. </param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("t", Text);
            info.AddValue("f", Font);
            info.AddValue("s", Style);
        }
    }
}