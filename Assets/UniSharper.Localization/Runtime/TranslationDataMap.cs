// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;

namespace UniSharper.Localization
{
    internal class TranslationDataMap : Dictionary<string, TranslationData>
    {
        public TranslationDataMap()
            : base(StringComparer.Ordinal)
        {
        }

        public TranslationDataMap(IDictionary<string, TranslationData> source)
            : base(source, StringComparer.Ordinal)
        {
        }
    }
}