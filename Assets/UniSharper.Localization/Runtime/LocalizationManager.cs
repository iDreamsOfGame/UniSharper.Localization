// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
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
    public sealed partial class LocalizationManager : Singleton<LocalizationManager>
    {
        private readonly Dictionary<Locale, TranslationDataMap> localeTranslationTextsMap;

        private Locale currentLocale;

        [Preserve]
        private LocalizationManager()
        {
            localeTranslationTextsMap = new Dictionary<Locale, TranslationDataMap>();
            currentLocale = Locale.English;
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
            
            if (localeTranslationTextsMap.TryGetValue(locale, out var map))
            {
                var lookupKey = map.UseInternStringPool ? string.IsInterned(key) ?? key : key;
                if (map.TryGetValue(lookupKey, out var translationData))
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
        public string GetTranslationText(Locale locale, string key)
        {
            var translationData = GetTranslationData(locale, key);
            var text = translationData?.Text;
            return !string.IsNullOrEmpty(text) ? text : TranslationData.DefaultText;
        }

        /// <summary>
        /// Gets the translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <returns>The translation text.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetTranslationText(string key) => GetTranslationText(CurrentLocale, key);

        /// <summary>
        /// Loads the localization asset data.
        /// </summary>
        /// <param name="locale">The locale.</param>
        /// <param name="data">The localization asset data.</param>
        /// <param name="useInternStringPool">if set to <c>true</c> [use intern string pool].</param>
        public void LoadLocalizationAssetData(Locale locale, byte[] data, bool useInternStringPool = true)
        {
            var map = TranslationDataMapSerializer.Deserialize(data, useInternStringPool);
            localeTranslationTextsMap.AddUnique(locale, map);
        }

        private void OnLocaleChanged(LocaleChangedEventArgs e) => LocaleChanged?.Invoke(this, e);
    }
}