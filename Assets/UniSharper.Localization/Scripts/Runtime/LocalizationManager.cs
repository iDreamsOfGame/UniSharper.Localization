// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using ReSharp.Patterns;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
        #region Fields

        /// <summary>
        /// The default translation text.
        /// </summary>
        public const string DefaultText = "NoString";

        private readonly Dictionary<Locale, Dictionary<string, string>> localeTranslationTextsMap;
        private Locale currentLocale;
        private EventHandler<LocaleChangedEventArgs> onLocaleChangedDelegate;

        #endregion Fields

        #region Constructors

        [Preserve]
        private LocalizationManager()
        {
            localeTranslationTextsMap = new Dictionary<Locale, Dictionary<string, string>>();
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Occurs when [locale changed].
        /// </summary>
        public event EventHandler<LocaleChangedEventArgs> LocaleChanged
        {
            add => onLocaleChangedDelegate += value;
            remove
            {
                if (onLocaleChangedDelegate != null)
                    onLocaleChangedDelegate -= value;
            }
        }

        #endregion Events

        #region Properties

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
                onLocaleChangedDelegate?.Invoke(this, new LocaleChangedEventArgs(currentLocale));
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the translation text of the target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <returns>The translation text.</returns>
        /// <exception cref="System.ArgumentNullException">locale or key</exception>
        public string GetTranslationText(Locale locale, string key)
        {
            string text = DefaultText;

            if (locale == null)
            {
                throw new ArgumentNullException(nameof(locale));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (localeTranslationTextsMap.ContainsKey(locale))
            {
                Dictionary<string, string> translationData = localeTranslationTextsMap[locale];
                if (translationData.ContainsKey(key))
                {
                    text = translationData[key];
                }
                else
                {
                    Debug.LogWarningFormat("No translation text for key [{0}] of locale [{1}]!", key, locale);
                }
            }
            else
            {
                Debug.LogWarningFormat("No translation texts for locale [{0}]!", locale);
            }

            return text;
        }

        /// <summary>
        /// Gets the translation text of current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <returns>The translation text.</returns>
        /// <exception cref="System.ArgumentNullException">key</exception>
        public string GetTranslationText(string key)
        {
            string text = DefaultText;

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (CurrentLocale != null)
            {
                text = GetTranslationText(CurrentLocale, key);
            }

            return text;
        }

        /// <summary>
        /// Loads the localization asset data.
        /// </summary>
        /// <param name="locale">The locale.</param>
        /// <param name="data">The localization asset data.</param>
        public void LoadLocalizationAssetData(Locale locale, byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                BinaryFormatter reader = new BinaryFormatter();
                Dictionary<string, string> translationData = reader.Deserialize(stream) as Dictionary<string, string>;
                localeTranslationTextsMap.AddUnique(locale, translationData);
            }
        }

        #endregion Methods
    }
}