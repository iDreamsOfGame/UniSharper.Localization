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
        #region Fields

        private Dictionary<string, Dictionary<Locale, string>> convertedTranslationDataMap;
        private Dictionary<Locale, Dictionary<string, string>> translationDataMap;

        #endregion Fields

        #region Constructors

        public TranslationDataTreeView(TreeViewState state, Dictionary<Locale, Dictionary<string, string>> translationDataMap)
            : base(state, new TranslationDataColumnHeader(CreateDefaultMultiColumnHeaderState(translationDataMap)))
        {
            this.translationDataMap = translationDataMap;
            showAlternatingRowBackgrounds = true;
            Reload();
        }

        #endregion Constructors

        #region Methods

        public static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState(Dictionary<Locale, Dictionary<string, string>> translationDataMap)
        {
            return new MultiColumnHeaderState(GetColumns(translationDataMap));
        }

        public Dictionary<Locale, Dictionary<string, string>> RestoreTranslationDataMap()
        {
            Dictionary<Locale, Dictionary<string, string>> result = null;

            if (convertedTranslationDataMap != null)
            {
                result = new Dictionary<Locale, Dictionary<string, string>>();

                foreach (KeyValuePair<string, Dictionary<Locale, string>> kvp in convertedTranslationDataMap)
                {
                    string key = kvp.Key;

                    foreach (KeyValuePair<Locale, string> textMap in kvp.Value)
                    {
                        Locale locale = textMap.Key;
                        string text = textMap.Value;

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
            TreeViewItem root = new TreeViewItem(-1, -1); // dummy root node
            convertedTranslationDataMap = ConvertTranslationDataMap();

            if (convertedTranslationDataMap != null && convertedTranslationDataMap.Count > 0)
            {
                foreach (KeyValuePair<string, Dictionary<Locale, string>> kvp in convertedTranslationDataMap)
                {
                    IList<string> texts = new List<string>(kvp.Value.Count);

                    foreach (string text in kvp.Value.Values)
                    {
                        texts.Add(text);
                    }

                    root.AddChild(new TranslationDataTreeViewItem(kvp.Key, texts));
                }
            }

            return root;
        }

        protected override void RowGUI(RowGUIArgs args)
        {
            TranslationDataTreeViewItem item = args.item as TranslationDataTreeViewItem;
            if (item == null)
            {
                base.RowGUI(args);
                return;
            }

            if (!args.isRenaming)
            {
                for (int i = 0; i < args.GetNumVisibleColumns(); i++)
                {
                    CellGUI(args.GetCellRect(i), item, multiColumnHeader.state.visibleColumns[i], ref args);
                }
            }
        }

        private static MultiColumnHeaderState.Column CreateColumn(string label, bool canSort = false, bool allowToggleVisibility = false)
        {
            return new MultiColumnHeaderState.Column()
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
        }

        private static MultiColumnHeaderState.Column[] GetColumns(Dictionary<Locale, Dictionary<string, string>> translationDataMap)
        {
            int columnCount = (translationDataMap != null && translationDataMap.Count > 0) ? translationDataMap.Count + 1 : 2;
            MultiColumnHeaderState.Column[] columns = new MultiColumnHeaderState.Column[columnCount];
            columns[0] = CreateColumn("Translation Key");

            if (translationDataMap != null && translationDataMap.Count > 0)
            {
                int i = 0;

                foreach (Locale locale in translationDataMap.Keys)
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
            GUIStyle labelStyle = GUI.skin.GetStyle("Label");

            if (Event.current.type == EventType.Repaint)
            {
                if (columnIndex == 0)
                {
                    labelStyle.Draw(cellRect, viewItem.displayName, false, false, args.selected, args.focused);
                }
                else
                {
                    labelStyle.Draw(cellRect, viewItem.TranslationTexts[columnIndex - 1], false, false, args.selected, args.focused);
                }
            }
        }

        private Dictionary<string, Dictionary<Locale, string>> ConvertTranslationDataMap()
        {
            Dictionary<string, Dictionary<Locale, string>> map = null;

            if (translationDataMap != null && translationDataMap.Count > 0)
            {
                map = new Dictionary<string, Dictionary<Locale, string>>();

                foreach (KeyValuePair<Locale, Dictionary<string, string>> kvp in translationDataMap)
                {
                    Locale locale = kvp.Key;
                    foreach (KeyValuePair<string, string> texts in kvp.Value)
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

        #endregion Methods
    }

    internal class TranslationDataTreeViewItem : TreeViewItem
    {
        #region Constructors

        public TranslationDataTreeViewItem(string translationTextKey, IList<string> translationTexts)
        : base(translationTextKey.GetHashCode(), 0, translationTextKey)
        {
            TranslationTexts = translationTexts;
        }

        #endregion Constructors

        #region Properties

        public IList<string> TranslationTexts
        {
            get;
            set;
        }

        #endregion Properties
    }
}