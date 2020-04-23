// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEditor;

namespace UniSharperEditor.Localization
{
    internal class TranslationDataImporterWindow : LocalizationEditorWindow
    {
        #region Fields

        private TranslationDataImporter importer;

        #endregion Fields

        #region Methods

        [MenuItem("UniSharper/Localization Management/Translation Data Importer", false, 1)]
        internal static void ShowWindow()
        {
            GetWindow<TranslationDataImporterWindow>("Translation Data Importer", true).Show();
        }

        protected override void DrawGUIWithSettings()
        {
            if (importer == null)
            {
                importer = new TranslationDataImporter(Settings);
            }

            importer.DrawEditorGui();
        }

        #endregion Methods
    }
}