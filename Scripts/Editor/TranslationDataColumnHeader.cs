// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEditor.IMGUI.Controls;

namespace UniSharperEditor.Localization
{
    internal class TranslationDataColumnHeader : MultiColumnHeader
    {
        #region Constructors

        public TranslationDataColumnHeader(MultiColumnHeaderState state)
            : base(state)
        {
            ResizeToFit();
        }

        #endregion Constructors
    }
}