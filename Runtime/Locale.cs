// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UniSharper.Localization
{
    /// <summary>
    /// A <b>Locale</b> object represents a specific geographical, political, or cultural region.
    /// Implements the <see cref="Locale"/> Implements the <see cref="System.Runtime.Serialization.ISerializable"/>
    /// </summary>
    /// <seealso cref="Locale"/>
    /// <seealso cref="System.Runtime.Serialization.ISerializable"/>
    [Serializable]
    public sealed partial class Locale : IEquatable<Locale>, ISerializable
    {
        #region Fields

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the Arabic language. This field is read-only.
        /// </summary>
        public static readonly Locale Arabic = new Locale("ar");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the English language in Canada. This field is read-only.
        /// </summary>
        public static readonly Locale Canada = new Locale("en_CA");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the French language in Canada. This field is read-only.
        /// </summary>
        public static readonly Locale CanadaFrench = new Locale("fr_CA");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the Chinese language. This field is read-only.
        /// </summary>
        public static readonly Locale Chinese = new Locale("zh");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the general English language. This field is read-only.
        /// </summary>
        public static readonly Locale English = new Locale("en");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the French language in France. This field is read-only.
        /// </summary>
        public static readonly Locale France = new Locale("fr_FR");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the general French language. This field is read-only.
        /// </summary>
        public static readonly Locale French = new Locale("fr");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the general German language. This field is read-only.
        /// </summary>
        public static readonly Locale German = new Locale("de");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the German language in Germany. This field is read-only.
        /// </summary>
        public static readonly Locale Germany = new Locale("de_DE");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the general Italian language. This field is read-only.
        /// </summary>
        public static readonly Locale Italian = new Locale("it");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the Italian language in Italy. This field is read-only.
        /// </summary>
        public static readonly Locale Italy = new Locale("it_IT");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the Japanese language in Japan. This field is read-only.
        /// </summary>
        public static readonly Locale Japan = new Locale("jp_JP");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the general Japanese language. This field is read-only.
        /// </summary>
        public static readonly Locale Japanese = new Locale("jp");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the general Korea language. This field is read-only.
        /// </summary>
        public static readonly Locale Korea = new Locale("ko");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the Korea language in Korean. This field is read-only.
        /// </summary>
        public static readonly Locale Korean = new Locale("ko_KR");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the Portuguese language in Portugal. This field is read-only.
        /// </summary>
        public static readonly Locale Portugal = new Locale("pt_PT");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the general Portuguese language. This field is read-only.
        /// </summary>
        public static readonly Locale Portuguese = new Locale("pt");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the Russian language in Russia. This field is read-only.
        /// </summary>
        public static readonly Locale Russia = new Locale("ru_RU");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the general Russian language. This field is read-only.
        /// </summary>
        public static readonly Locale Russian = new Locale("ru");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the Arabic language in Saudi Arabia. This field is read-only.
        /// </summary>
        public static readonly Locale SaudiArabia = new Locale("ar_SA");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the Simplified Chinese language. This field is read-only.
        /// </summary>
        public static readonly Locale SimplifiedChinese = new Locale("zh_CN");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the Spanish language in Spain. This field is read-only.
        /// </summary>
        public static readonly Locale Spain = new Locale("es_ES");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for general the Spanish language in. This field is read-only.
        /// </summary>
        public static readonly Locale Spanish = new Locale("es");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the general Thai language. This field is read-only.
        /// </summary>
        public static readonly Locale Thai = new Locale("th");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the Thai language in Thailand. This field is read-only.
        /// </summary>
        public static readonly Locale Thailand = new Locale("th_TH");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the Tradition Chinese language. This field is read-only.
        /// </summary>
        public static readonly Locale TraditionalChinese = new Locale("zh_TW");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the English language in United Kingdom. This field is read-only.
        /// </summary>
        public static readonly Locale UK = new Locale("en_GB");

        /// <summary>
        /// Represents an instance of <see cref="UniSharper.Localization.Locale"/> that is useful
        /// for the English language in United States. This field is read-only.
        /// </summary>
        public static readonly Locale US = new Locale("en_US");

        private string locale;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Locale"/> class.
        /// </summary>
        /// <param name="locale">The locale code. Such as "em_US", "zh_CN".</param>
        /// <exception cref="ArgumentNullException">locale</exception>
        public Locale(string locale)
        {
            ParseLocaleString(locale);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Locale"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.
        /// </param>
        /// <param name="context">
        /// The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for
        /// this serialization.
        /// </param>
        public Locale(SerializationInfo info, StreamingContext context)
        {
            ParseLocaleString(info.GetString("locale"));
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the country code string.
        /// </summary>
        /// <value>An ISO 3166 alpha-2 country code or a UN M.49 numeric-3 area code.</value>
        public string Country { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the name of the defined constant. If the defined constant exists.
        /// </summary>
        /// <returns>The name of the defined constant.</returns>
        public string DefinedConstantName
        {
            get
            {
                Dictionary<string, object> dict = this.GetType().GetStaticFieldValuePairs();

                foreach (KeyValuePair<string, object> kvp in dict)
                {
                    if (kvp.Value is Locale && (kvp.Value as Locale).ToString() == locale)
                    {
                        return kvp.Key;
                    }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the language code string.
        /// </summary>
        /// <value>
        /// An ISO 639 alpha-2 or alpha-3 language code, or a language subtag up to 8 characters in length.
        /// </value>
        public string Language { get; private set; } = string.Empty;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Locale"/>.
        /// </summary>
        /// <param name="value">The <see cref="System.String"/> value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Locale(string value)
        {
            return new Locale(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Locale"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="value">The <see cref="UniSharper.Localization.Locale"/> value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(Locale value)
        {
            return value.locale;
        }

        /// <summary>
        /// Performs a logical comparison of the two <see cref="Locale"/> parameters to determine
        /// whether they are not equal.
        /// </summary>
        /// <param name="left">A <see cref="Locale"/> object.</param>
        /// <param name="right">A <see cref="Locale"/> object.</param>
        /// <returns>
        /// <c>true</c> if the two instances are not equal or <c>false</c> if the two instances are equal.
        /// </returns>
        public static bool operator !=(Locale left, Locale right) => !Equals(left, right);

        /// <summary>
        /// Performs a logical comparison of the two <see cref="Locale"/> parameters to determine
        /// whether they are equal.
        /// </summary>
        /// <param name="left">A <see cref="Locale"/> object.</param>
        /// <param name="right">A <see cref="Locale"/> object.</param>
        /// <returns>
        /// <c>true</c> if the two instances are equal or <c>false</c> if the two instances are not equal.
        /// </returns>
        public static bool operator ==(Locale left, Locale right) => Equals(left, right);

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance;
        /// otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj) => Equals(obj as Locale);

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter;
        /// otherwise, false.
        /// </returns>
        public bool Equals(Locale other)
        {
            return other != null && string.Equals(locale, other.locale);
        }

        /// <summary>
        /// Gets the name of constant <see cref="UniSharper.Localization.Locale"/>.
        /// </summary>
        /// <param name="constantName">Name of the constant.</param>
        /// <returns>
        /// <c>true</c> if has defined constant for this <see
        /// cref="UniSharper.Localization.Locale"/>, <c>false</c> otherwise.
        /// </returns>
        public bool GetConstantName(out string constantName)
        {
            var definedConstantName = DefinedConstantName;

            if (string.IsNullOrEmpty(definedConstantName))
            {
                if (!string.IsNullOrEmpty(Country))
                {
                    constantName = Country.ToUpper();
                    return false;
                }

                if (!string.IsNullOrEmpty(Language))
                {
                    constantName = Language.ToUpper();
                    return false;
                }
            }

            constantName = definedConstantName;
            return true;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return locale.GetHashCode();
        }

        /// <summary>
        /// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data
        /// needed to serialize the target object.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.
        /// </param>
        /// <param name="context">
        /// The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for
        /// this serialization.
        /// </param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("locale", locale);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return locale;
        }

        private void ParseLocaleString(string locale)
        {
            if (string.IsNullOrEmpty(locale))
            {
                throw new ArgumentNullException(nameof(locale));
            }

            this.locale = locale.Replace("-", "_");
            var segments = this.locale.Split('_');

            if (segments.Length <= 0)
                return;

            Language = segments[0];

            if (segments.Length > 1)
            {
                Country = segments[1];
            }
        }

        #endregion Methods
    }
}