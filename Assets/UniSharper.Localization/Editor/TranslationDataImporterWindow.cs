// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEditor;
using UnityEngine;

namespace UniSharperEditor.Localization
{
    internal class TranslationDataImporterWindow : LocalizationEditorWindow
    {
        private static readonly Vector2Int Size = new Vector2Int(850, 420);
        
        private TranslationDataImporter importer;

        [MenuItem("UniSharper/Localization Management/Import Translation Data...", false, 1)]
        internal static void ShowWindow()
        {
            const string title = "Translation Data Importer";
            var position = new Vector2((Screen.width - Size.x) * 0.5f, (Screen.height - Size.y) * 0.5f);
            var rect = new Rect(position, Size);
            var window = GetWindowWithRect<TranslationDataImporterWindow>(rect, true, title);
            window.minSize = window.maxSize = Size;
            window.Show();
        }

        protected override void DrawGUIWithSettings()
        {
            importer ??= new TranslationDataImporter(Settings);
            importer.DrawEditorGui(this);
        }
    }
}