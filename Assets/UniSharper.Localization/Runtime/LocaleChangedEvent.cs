// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;

namespace UniSharper.Localization
{
    /// <summary>
    /// Contains the event data of the LocaleChanged event. Implements the <see cref="System.EventArgs"/>
    /// </summary>
    /// <seealso cref="System.EventArgs"/>
    public class LocaleChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocaleChangedEventArgs"/> class.
        /// </summary>
        /// <param name="currentLocale">The current locale.</param>
        public LocaleChangedEventArgs(Locale currentLocale)
        {
            CurrentLocale = currentLocale;
        }

        /// <summary>
        /// Gets or sets the current locale.
        /// </summary>
        /// <value>The current locale.</value>
        public Locale CurrentLocale { get; }
    }
}