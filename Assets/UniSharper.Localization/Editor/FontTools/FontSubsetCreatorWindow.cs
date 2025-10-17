using UnityEditor;
using UnityEngine;

namespace UniSharperEditor.Localization.FontTools
{
    internal class FontSubsetCreatorWindow : EditorWindow
    {
        private static readonly Vector2Int Size = new(850, 160);

        private FontSubsetCreator creator;
        
        [MenuItem("UniSharper/Localization Management/Font Tools/Create Font Subset...", false, 3)]
        internal static void ShowWindow()
        {
            const string title = "Font Subset Creator";
            var position = new Vector2((Screen.width - Size.x) * 0.5f, (Screen.height - Size.y) * 0.5f);
            var rect = new Rect(position, Size);
            var window = GetWindowWithRect<FontSubsetCreatorWindow>(rect, true, title);
            window.minSize = window.maxSize = Size;
            window.Show();
        }

        private void OnGUI()
        {
            creator ??= new FontSubsetCreator();
            creator.DrawEditorGui(this);
            GUIUtility.ExitGUI();
        }
    }
}