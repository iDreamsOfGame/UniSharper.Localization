// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ReSharp.Extensions;
using ReSharp.Patterns;
using UnityEngine;
using UnityEngine.Scripting;

namespace UniSharper.Localization
{
    /// <summary>
    /// The LocalizationManager is a convenience class for managing localization assets data.
    /// Implements the <see cref="LocalizationManager"/>
    /// </summary>
    /// <seealso cref="LocalizationManager"/>
    public sealed class LocalizationManager : Singleton<LocalizationManager>
    {
        /// <summary>
        /// The default translation text.
        /// </summary>
        public const string DefaultText = "NoString";

        private readonly Dictionary<Locale, Dictionary<string, TranslationData>> localeTranslationTextsMap;

        private Locale currentLocale;

        [Preserve]
        private LocalizationManager()
        {
            localeTranslationTextsMap = new Dictionary<Locale, Dictionary<string, TranslationData>>();
        }

        /// <summary>
        /// Occurs when [locale changed].
        /// </summary>
        public event EventHandler<LocaleChangedEventArgs> LocaleChanged;

        /// <summary>
        /// Gets or sets the current locale.
        /// </summary>
        /// <value>The current locale.</value>
        public Locale CurrentLocale
        {
            get => currentLocale;
            set
            {
                if (currentLocale != null && currentLocale.Equals(value))
                    return;

                currentLocale = value;
                OnLocaleChanged(new LocaleChangedEventArgs(currentLocale));
            }
        }

        /// <summary>
        /// Gets the translation data for target locale.
        /// </summary>
        /// <param name="locale">The target locale. </param>
        /// <param name="key">The key of translation data. </param>
        /// <returns>The translation data. </returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public TranslationData GetTranslationData(Locale locale, string key)
        {
            if (locale == null)
                throw new ArgumentNullException(nameof(locale));

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            
            if (localeTranslationTextsMap.ContainsKey(locale))
            {
                var dataMap = localeTranslationTextsMap[locale];
                if(dataMap.TryGetValue(key, out var translationData))
                    return translationData;

                Debug.LogWarning($"No translation text for key [{key}] of locale [{locale}]!");
            }
            else
            {
                Debug.LogWarning($"No translation texts for locale [{locale}]!");
            }

            return null;
        }
        
        /// <summary>
        /// Gets the translation data for target locale.
        /// </summary>
        /// <param name="key">The key of translation data. </param>
        /// <returns>The translation data. </returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public TranslationData GetTranslationData(string key) => CurrentLocale != null ? GetTranslationData(CurrentLocale, key) : null;

        /// <summary>
        /// Gets the translation text for target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <returns>The translation text.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetTranslationText(Locale locale, string key) => GetTranslationData(locale, key)?.Text ?? DefaultText;

        /// <summary>
        /// Gets the translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <returns>The translation text.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetTranslationText(string key) => CurrentLocale != null ? GetTranslationText(CurrentLocale, key) : DefaultText;

        /// <summary>
        /// Loads the localization asset data.
        /// </summary>
        /// <param name="locale">The locale.</param>
        /// <param name="data">The localization asset data.</param>
        public void LoadLocalizationAssetData(Locale locale, byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                var reader = new BinaryFormatter();
                var translationDataMap = reader.Deserialize(stream) as Dictionary<string, TranslationData>;
                localeTranslationTextsMap.AddUnique(locale, translationDataMap);
            }
        }

        private void OnLocaleChanged(LocaleChangedEventArgs e) => LocaleChanged?.Invoke(this, e);
    }
}