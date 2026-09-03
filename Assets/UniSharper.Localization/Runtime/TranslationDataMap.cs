// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using ReSharp.Extensions;

namespace UniSharper.Localization
{
    internal class TranslationDataMap : Dictionary<string, TranslationData>
    {
        /// <summary>
        /// Whether to use the intern string pool.
        /// </summary>
        public bool UseInternStringPool { get; set; }

        public TranslationDataMap()
            :base(StringReferenceEqualityComparer.Instance)
        {
        }

        public TranslationDataMap(IDictionary<string, TranslationData> source)
            :base(source)
        {
        }
    }
}