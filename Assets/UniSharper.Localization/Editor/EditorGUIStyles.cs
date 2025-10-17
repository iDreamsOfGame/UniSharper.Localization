using UnityEditor;
using UnityEngine;

namespace UniSharperEditor.Localization
{
    internal static class EditorGUIStyles
    {
        private static GUIStyle titleBoxGUIStyle;

        private static readonly GUILayoutOption TitleBoxHeight = GUILayout.Height(30);

        private static GUIStyle TitleBoxGUIStyle
        {
            get
            {
                if (titleBoxGUIStyle == null)
                {
                    titleBoxGUIStyle = new GUIStyle(EditorStyles.helpBox)
                    {
                        alignment = TextAnchor.MiddleLeft,
                        margin = new RectOffset { top = 8, bottom = 8 },
                        padding = new RectOffset { left = 10, right = 10 },
                        font = EditorStyles.label.font,
                        fontSize = 14,
                        richText = true
                    };

                    var textColor = GUI.skin.button.normal.textColor;
                    titleBoxGUIStyle.normal.textColor = textColor;
                    titleBoxGUIStyle.hover.textColor = textColor;
                    titleBoxGUIStyle.focused.textColor = textColor;
                    titleBoxGUIStyle.active.textColor = textColor;
                }

                return titleBoxGUIStyle;
            }
        }
        
        public static void DrawTitleLabel(string text)
        {
            GUILayout.Box(text, TitleBoxGUIStyle, TitleBoxHeight);
        }
    }
}