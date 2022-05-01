// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using UniSharper.Localization;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace UniSharperEditor.Localization
{
    internal class TranslationDataTreeView : TreeView
    {
        private Dictionary<string, Dictionary<Locale, string>> convertedTranslationDataMap;

        private readonly Dictionary<Locale, Dictionary<string, string>> translationDataMap;

        public TranslationDataTreeView(TreeViewState state, Dictionary<Locale, Dictionary<string, string>> translationDataMap)
            : base(state, new TranslationDataColumnHeader(CreateDefaultMultiColumnHeaderState(translationDataMap)))
        {
            this.translationDataMap = translationDataMap;
            showAlternatingRowBackgrounds = true;
            Reload();
        }

        public static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState(Dictionary<Locale, Dictionary<string, string>> translationDataMap) => new MultiColumnHeaderState(GetColumns(translationDataMap));

        public Dictionary<Locale, Dictionary<string, string>> RestoreTranslationDataMap()
        {
            var result = new Dictionary<Locale, Dictionary<string, string>>();

            if (convertedTranslationDataMap != null)
            {
                foreach (var kvp in convertedTranslationDataMap)
                {
                    var key = kvp.Key;

                    foreach (var textMap in kvp.Value)
                    {
                        var locale = textMap.Key;
                        var text = textMap.Value;

                        if (!result.ContainsKey(locale))
                        {
                            result.Add(locale, new Dictionary<string, string>());
                        }

                        if (!result[locale].ContainsKey(key))
                        {
                            result[locale].Add(key, text);
                        }
                    }
                }
            }

            return result;
        }

        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem(-1, -1); // dummy root node
            convertedTranslationDataMap = ConvertTranslationDataMap();

            if (convertedTranslationDataMap != null && convertedTranslationDataMap.Count > 0)
            {
                foreach (var kvp in convertedTranslationDataMap)
                {
                    var texts = new List<string>(kvp.Value.Values);
                    root.AddChild(new TranslationDataTreeViewItem(kvp.Key, texts));
                }
            }

            return root;
        }

        protected override void RowGUI(RowGUIArgs args)
        {
            if (!(args.item is TranslationDataTreeViewItem item))
            {
                base.RowGUI(args);
                return;
            }

            if (!args.isRenaming)
            {
                for (var i = 0; i < args.GetNumVisibleColumns(); i++)
                {
                    CellGUI(args.GetCellRect(i), item, multiColumnHeader.state.visibleColumns[i], ref args);
                }
            }
        }

        private static MultiColumnHeaderState.Column CreateColumn(string label, bool canSort = false, bool allowToggleVisibility = false) => new MultiColumnHeaderState.Column()
        {
            headerContent = new GUIContent(label, label),
            allowToggleVisibility = allowToggleVisibility,
            canSort = canSort,
            minWidth = 50,
            width = 150,
            maxWidth = 1000,
            headerTextAlignment = TextAlignment.Left,
            autoResize = false
        };

        private static MultiColumnHeaderState.Column[] GetColumns(Dictionary<Locale, Dictionary<string, string>> translationDataMap)
        {
            var columnCount = (translationDataMap != null && translationDataMap.Count > 0) ? translationDataMap.Count + 1 : 2;
            var columns = new MultiColumnHeaderState.Column[columnCount];
            columns[0] = CreateColumn("Translation Key");

            if (translationDataMap != null && translationDataMap.Count > 0)
            {
                var i = 0;

                foreach (var locale in translationDataMap.Keys)
                {
                    locale.GetConstantName(out var label);
                    label += $" ({locale})";
                    columns[i + 1] = CreateColumn(label);
                    i++;
                }
            }

            return columns;
        }

        private void CellGUI(Rect cellRect, TranslationDataTreeViewItem viewItem, int columnIndex, ref RowGUIArgs args)
        {
            var labelStyle = GUI.skin.GetStyle("Label");

            if (Event.current.type == EventType.Repaint)
            {
                labelStyle.Draw(cellRect, columnIndex == 0 
                    ? viewItem.displayName
                    : viewItem.TranslationTexts[columnIndex - 1], false, false, args.selected, args.focused);
            }
        }

        private Dictionary<string, Dictionary<Locale, string>> ConvertTranslationDataMap()
        {
            var map = new Dictionary<string, Dictionary<Locale, string>>();

            if (translationDataMap != null && translationDataMap.Count > 0)
            {
                foreach (var kvp in translationDataMap)
                {
                    var locale = kvp.Key;
                    foreach (var texts in kvp.Value)
                    {
                        if (!map.ContainsKey(texts.Key))
                        {
                            map.Add(texts.Key, new Dictionary<Locale, string>());
                        }

                        map[texts.Key].Add(locale, texts.Value);
                    }
                }
            }

            return map;
        }
    }

    internal class TranslationDataTreeViewItem : TreeViewItem
    {
        public TranslationDataTreeViewItem(string translationTextKey, IList<string> translationTexts)
        : base(translationTextKey.GetHashCode(), 0, translationTextKey) => TranslationTexts = translationTexts;

        public IList<string> TranslationTexts { get; }
    }
}