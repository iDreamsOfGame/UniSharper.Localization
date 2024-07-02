// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using UniSharper.Localization;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEditorUtility = UnityEditor.EditorUtility;

namespace UniSharperEditor.Localization
{
    internal class LocalizationAssetsViewerWindow : LocalizationEditorWindow
    {
        private static Dictionary<Locale, Dictionary<string, TranslationData>> translationDataMap;

        private TranslationDataTreeView translationDataTreeView;

        [MenuItem("UniSharper/Localization Management/View Localization Assets...", false, 2)]
        internal static void ShowWindow()
        {
            translationDataMap = LocalizationAssetUtility.LoadLocalizationAssets();

            if (translationDataMap != null && translationDataMap.Count > 0)
            {
                GetWindow<LocalizationAssetsViewerWindow>("Localization Assets Viewer").Show();
            }
            else
            {
                UnityEditorUtility.DisplayDialog("Error", "Attempting to open Localization Assets Viewer window, but no Localization Asset exists. " +
                                                          " \n\nOpen 'UniSharper/Localization Management/Translation Data Importer' to import translation data first.", "Ok");
            }
        }

        protected override void DrawGUIWithSettings()
        {
            DrawTranslationDataTreeView();
        }

        private void DrawTranslationDataTreeView()
        {
            if (translationDataTreeView == null)
                translationDataTreeView = new TranslationDataTreeView(new TreeViewState(), translationDataMap);
            
            translationDataTreeView.OnGUI(new Rect(0, 0, position.width, position.height));
        }
    }
}