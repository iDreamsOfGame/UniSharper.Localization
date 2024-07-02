// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UniSharper.Localization;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

// ReSharper disable UseNegatedPatternMatching

namespace UniSharperEditor.Localization
{
    internal class TranslationDataTreeView : TreeView
    {
        private Dictionary<string, Dictionary<Locale, TranslationData>> convertedTranslationDataMap;

        private readonly Dictionary<Locale, Dictionary<string, TranslationData>> translationDataMap;

        public TranslationDataTreeView(TreeViewState state, Dictionary<Locale, Dictionary<string, TranslationData>> translationDataMap)
            : base(state, new TranslationDataColumnHeader(CreateDefaultMultiColumnHeaderState(translationDataMap)))
        {
            this.translationDataMap = translationDataMap;
            showAlternatingRowBackgrounds = true;
            Reload();
        }

        public static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState(Dictionary<Locale, Dictionary<string, TranslationData>> translationDataMap) => 
            new MultiColumnHeaderState(GetColumns(translationDataMap));

        [UsedImplicitly]
        public Dictionary<Locale, Dictionary<string, TranslationData>> RestoreTranslationDataMap()
        {
            var result = new Dictionary<Locale, Dictionary<string, TranslationData>>();
            if (convertedTranslationDataMap == null) 
                return result;
            
            foreach (var pair in convertedTranslationDataMap)
            {
                var key = pair.Key;
                var value = pair.Value;
                foreach (var kvp in value)
                {
                    var locale = kvp.Key;
                    var translationData = kvp.Value;
                    if (!result.ContainsKey(locale))
                    {
                        result.Add(locale, new Dictionary<string, TranslationData>());
                    }

                    if (!result[locale].ContainsKey(key))
                    {
                        result[locale].Add(key, translationData);
                    }
                }
            }

            return result;
        }

        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem(-1, -1); // dummy root node
            convertedTranslationDataMap = ConvertTranslationDataMap();

            if (convertedTranslationDataMap == null || convertedTranslationDataMap.Count == 0) 
                return root;
            
            foreach (var pair in convertedTranslationDataMap)
            {
                var key = pair.Key;
                var value = pair.Value;
                var texts = value.Values.Select(translationData => translationData.Text).ToList();
                root.AddChild(new TranslationDataTreeViewItem(key, texts));
            }

            return root;
        }

        protected override void RowGUI(RowGUIArgs args)
        {
            var item = args.item as TranslationDataTreeViewItem;
            if (item == null)
            {
                base.RowGUI(args);
                return;
            }

            if (args.isRenaming) 
                return;
            
            for (var i = 0; i < args.GetNumVisibleColumns(); i++)
            {
                CellGUI(args.GetCellRect(i), item, multiColumnHeader.state.visibleColumns[i], ref args);
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

        private static MultiColumnHeaderState.Column[] GetColumns(Dictionary<Locale, Dictionary<string, TranslationData>> translationDataMap)
        {
            var columnCount = translationDataMap != null && translationDataMap.Count > 0 ? translationDataMap.Count + 1 : 2;
            var columns = new MultiColumnHeaderState.Column[columnCount];
            columns[0] = CreateColumn("Translation Key");

            if (translationDataMap == null || translationDataMap.Count == 0)
                return columns;
            
            var i = 0;

            foreach (var locale in translationDataMap.Keys)
            {
                locale.GetConstantName(out var label);
                label += $" ({locale})";
                columns[i + 1] = CreateColumn(label);
                i++;
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

        private Dictionary<string, Dictionary<Locale, TranslationData>> ConvertTranslationDataMap()
        {
            var map = new Dictionary<string, Dictionary<Locale, TranslationData>>();
            if (translationDataMap == null || translationDataMap.Count == 0)
                return map;
            
            foreach (var pair in translationDataMap)
            {
                var locale = pair.Key;
                var value = pair.Value;
                foreach (var kvp in value)
                {
                    var translationKey = kvp.Key;
                    var translationData = kvp.Value;
                    if (!map.ContainsKey(translationKey))
                    {
                        map.Add(translationKey, new Dictionary<Locale, TranslationData>());
                    }

                    map[translationKey].Add(locale, translationData);
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