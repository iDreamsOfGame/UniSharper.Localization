// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using UnityEditor;
using UnityEngine;

namespace UniSharperEditor.Localization
{
    internal abstract class LocalizationEditorWindow : EditorWindow
    {
        private LocalizationAssetSettings settings;

        protected LocalizationAssetSettings Settings
        {
            get
            {
                if (settings == null)
                {
                    settings = LocalizationAssetSettings.Load();
                }

                return settings;
            }
        }

        protected virtual void DrawGUIWithoutSettings()
        {
            GUILayout.Space(50);
            if (GUILayout.Button("Create Localization Settings"))
            {
                settings = LocalizationAssetSettings.Create();
            }
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.Space(50);
            GUI.skin.label.wordWrap = true;
            GUILayout.Label("Click the \"Create\" button above to start using Localization.  Once you begin, the Localization system will save some assets to your project to keep up with its data");
            GUILayout.Space(50);
            GUILayout.EndHorizontal();
        }

        protected virtual void DrawGUIWithSettings()
        {
        }

        private void OnGUI()
        {
            if (Settings == null)
            {
                DrawGUIWithoutSettings();
            }
            else
            {
                DrawGUIWithSettings();
            }
            
            GUIUtility.ExitGUI();
        }
    }
}