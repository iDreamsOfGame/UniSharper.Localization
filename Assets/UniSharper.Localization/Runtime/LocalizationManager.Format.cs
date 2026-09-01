// ReSharper disable ClassCannotBeInstantiated

namespace UniSharper.Localization
{
    public sealed partial class LocalizationManager
    {
        /// <summary>
        /// Gets the formatted translation text for target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1>(Locale locale, string key, T1 arg1)
        {
            var translationData = GetTranslationData(locale, key);
            return translationData?.GetFormattedText(arg1) ?? TranslationData.DefaultText;
        }

        /// <summary>
        /// Gets the formatted translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1>(string key, T1 arg1) => GetFormattedTranslationText(CurrentLocale, key, arg1);

        /// <summary>
        /// Gets the formatted translation text for target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2>(Locale locale,
            string key,
            T1 arg1,
            T2 arg2)
        {
            var translationData = GetTranslationData(locale, key);
            return translationData?.GetFormattedText(arg1, arg2) ?? TranslationData.DefaultText;
        }

        /// <summary>
        /// Gets the formatted translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2>(string key, T1 arg1, T2 arg2) => GetFormattedTranslationText(CurrentLocale, key, arg1, arg2);

        /// <summary>
        /// Gets the formatted translation text for target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3>(Locale locale,
            string key,
            T1 arg1,
            T2 arg2,
            T3 arg3)
        {
            var translationData = GetTranslationData(locale, key);
            return translationData?.GetFormattedText(arg1, arg2, arg3) ?? TranslationData.DefaultText;
        }

        /// <summary>
        /// Gets the formatted translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3>(string key,
            T1 arg1,
            T2 arg2,
            T3 arg3) =>
            GetFormattedTranslationText(CurrentLocale, key, arg1, arg2, arg3);

        /// <summary>
        /// Gets the formatted translation text for target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4>(Locale locale,
            string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4)
        {
            var translationData = GetTranslationData(locale, key);
            return translationData?.GetFormattedText(arg1, arg2, arg3, arg4) ?? TranslationData.DefaultText;
        }

        /// <summary>
        /// Gets the formatted translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4>(string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4) =>
            GetFormattedTranslationText(CurrentLocale, key, arg1, arg2, arg3, arg4);

        /// <summary>
        /// Gets the formatted translation text for target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5>(Locale locale,
            string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5)
        {
            var translationData = GetTranslationData(locale, key);
            return translationData?.GetFormattedText(arg1, arg2, arg3, arg4, arg5) ?? TranslationData.DefaultText;
        }

        /// <summary>
        /// Gets the formatted translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5>(string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5) =>
            GetFormattedTranslationText(CurrentLocale, key, arg1, arg2, arg3, arg4, arg5);

        /// <summary>
        /// Gets the formatted translation text for target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6>(Locale locale,
            string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6)
        {
            var translationData = GetTranslationData(locale, key);
            return translationData?.GetFormattedText(arg1, arg2, arg3, arg4, arg5, arg6) ?? TranslationData.DefaultText;
        }

        /// <summary>
        /// Gets the formatted translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6>(string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6) =>
            GetFormattedTranslationText(CurrentLocale, key, arg1, arg2, arg3, arg4, arg5, arg6);

        /// <summary>
        /// Gets the formatted translation text for target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7>(Locale locale,
            string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7)
        {
            var translationData = GetTranslationData(locale, key);
            return translationData?.GetFormattedText(arg1, arg2, arg3, arg4, arg5, arg6, arg7) ?? TranslationData.DefaultText;
        }

        /// <summary>
        /// Gets the formatted translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7>(string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7) =>
            GetFormattedTranslationText(CurrentLocale, key, arg1, arg2, arg3, arg4, arg5, arg6, arg7);

        /// <summary>
        /// Gets the formatted translation text for target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8>(Locale locale,
            string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8)
        {
            var translationData = GetTranslationData(locale, key);
            return translationData?.GetFormattedText(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) ?? TranslationData.DefaultText;
        }

        /// <summary>
        /// Gets the formatted translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8>(string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8) =>
            GetFormattedTranslationText(CurrentLocale, key, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);

        /// <summary>
        /// Gets the formatted translation text for target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <param name="arg9">The ninth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <typeparam name="T9">The type of the ninth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Locale locale,
            string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9)
        {
            var translationData = GetTranslationData(locale, key);
            return translationData?.GetFormattedText(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) ?? TranslationData.DefaultText;
        }

        /// <summary>
        /// Gets the formatted translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <param name="arg9">The ninth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <typeparam name="T9">The type of the ninth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9) =>
            GetFormattedTranslationText(CurrentLocale, key, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);

        /// <summary>
        /// Gets the formatted translation text for target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <param name="arg9">The ninth object to format.</param>
        /// <param name="arg10">The tenth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <typeparam name="T9">The type of the ninth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Locale locale,
            string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10)
        {
            var translationData = GetTranslationData(locale, key);
            return translationData?.GetFormattedText(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) ?? TranslationData.DefaultText;
        }

        /// <summary>
        /// Gets the formatted translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <param name="arg9">The ninth object to format.</param>
        /// <param name="arg10">The tenth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <typeparam name="T9">The type of the ninth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10) =>
            GetFormattedTranslationText(CurrentLocale, key, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

        /// <summary>
        /// Gets the formatted translation text for target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <param name="arg9">The ninth object to format.</param>
        /// <param name="arg10">The tenth object to format.</param>
        /// <param name="arg11">The eleventh object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <typeparam name="T9">The type of the ninth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Locale locale,
            string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11)
        {
            var translationData = GetTranslationData(locale, key);
            return translationData?.GetFormattedText(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11) ?? TranslationData.DefaultText;
        }

        /// <summary>
        /// Gets the formatted translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <param name="arg9">The ninth object to format.</param>
        /// <param name="arg10">The tenth object to format.</param>
        /// <param name="arg11">The eleventh object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <typeparam name="T9">The type of the ninth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11) =>
            GetFormattedTranslationText(CurrentLocale, key, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);

        /// <summary>
        /// Gets the formatted translation text for target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <param name="arg9">The ninth object to format.</param>
        /// <param name="arg10">The tenth object to format.</param>
        /// <param name="arg11">The eleventh object to format.</param>
        /// <param name="arg12">The twelfth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <typeparam name="T9">The type of the ninth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <typeparam name="T12">The type of the twelfth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Locale locale,
            string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12)
        {
            var translationData = GetTranslationData(locale, key);
            return translationData?.GetFormattedText(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12) ?? TranslationData.DefaultText;
        }

        /// <summary>
        /// Gets the formatted translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <param name="arg9">The ninth object to format.</param>
        /// <param name="arg10">The tenth object to format.</param>
        /// <param name="arg11">The eleventh object to format.</param>
        /// <param name="arg12">The twelfth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <typeparam name="T9">The type of the ninth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <typeparam name="T12">The type of the twelfth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12) =>
            GetFormattedTranslationText(CurrentLocale, key, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);

        /// <summary>
        /// Gets the formatted translation text for target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <param name="arg9">The ninth object to format.</param>
        /// <param name="arg10">The tenth object to format.</param>
        /// <param name="arg11">The eleventh object to format.</param>
        /// <param name="arg12">The twelfth object to format.</param>
        /// <param name="arg13">The thirteenth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <typeparam name="T9">The type of the ninth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <typeparam name="T12">The type of the twelfth argument.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Locale locale,
            string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12,
            T13 arg13)
        {
            var translationData = GetTranslationData(locale, key);
            return translationData?.GetFormattedText(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13) ?? TranslationData.DefaultText;
        }

        /// <summary>
        /// Gets the formatted translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <param name="arg9">The ninth object to format.</param>
        /// <param name="arg10">The tenth object to format.</param>
        /// <param name="arg11">The eleventh object to format.</param>
        /// <param name="arg12">The twelfth object to format.</param>
        /// <param name="arg13">The thirteenth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <typeparam name="T9">The type of the ninth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <typeparam name="T12">The type of the twelfth argument.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12,
            T13 arg13) =>
            GetFormattedTranslationText(CurrentLocale, key, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);

        /// <summary>
        /// Gets the formatted translation text for target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <param name="arg9">The ninth object to format.</param>
        /// <param name="arg10">The tenth object to format.</param>
        /// <param name="arg11">The eleventh object to format.</param>
        /// <param name="arg12">The twelfth object to format.</param>
        /// <param name="arg13">The thirteenth object to format.</param>
        /// <param name="arg14">The fourteenth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <typeparam name="T9">The type of the ninth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <typeparam name="T12">The type of the twelfth argument.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Locale locale,
            string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12,
            T13 arg13,
            T14 arg14)
        {
            var translationData = GetTranslationData(locale, key);
            return translationData?.GetFormattedText(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14) ?? TranslationData.DefaultText;
        }

        /// <summary>
        /// Gets the formatted translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <param name="arg9">The ninth object to format.</param>
        /// <param name="arg10">The tenth object to format.</param>
        /// <param name="arg11">The eleventh object to format.</param>
        /// <param name="arg12">The twelfth object to format.</param>
        /// <param name="arg13">The thirteenth object to format.</param>
        /// <param name="arg14">The fourteenth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <typeparam name="T9">The type of the ninth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <typeparam name="T12">The type of the twelfth argument.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12,
            T13 arg13,
            T14 arg14) =>
            GetFormattedTranslationText(CurrentLocale, key, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);

        /// <summary>
        /// Gets the formatted translation text for target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <param name="arg9">The ninth object to format.</param>
        /// <param name="arg10">The tenth object to format.</param>
        /// <param name="arg11">The eleventh object to format.</param>
        /// <param name="arg12">The twelfth object to format.</param>
        /// <param name="arg13">The thirteenth object to format.</param>
        /// <param name="arg14">The fourteenth object to format.</param>
        /// <param name="arg15">The fifteenth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <typeparam name="T9">The type of the ninth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <typeparam name="T12">The type of the twelfth argument.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth argument.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Locale locale,
            string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12,
            T13 arg13,
            T14 arg14,
            T15 arg15)
        {
            var translationData = GetTranslationData(locale, key);
            return translationData?.GetFormattedText(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15) ?? TranslationData.DefaultText;
        }

        /// <summary>
        /// Gets the formatted translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <param name="arg9">The ninth object to format.</param>
        /// <param name="arg10">The tenth object to format.</param>
        /// <param name="arg11">The eleventh object to format.</param>
        /// <param name="arg12">The twelfth object to format.</param>
        /// <param name="arg13">The thirteenth object to format.</param>
        /// <param name="arg14">The fourteenth object to format.</param>
        /// <param name="arg15">The fifteenth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <typeparam name="T9">The type of the ninth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <typeparam name="T12">The type of the twelfth argument.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth argument.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12,
            T13 arg13,
            T14 arg14,
            T15 arg15) =>
            GetFormattedTranslationText(CurrentLocale, key, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);

        /// <summary>
        /// Gets the formatted translation text for target locale.
        /// </summary>
        /// <param name="locale">The target locale.</param>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <param name="arg9">The ninth object to format.</param>
        /// <param name="arg10">The tenth object to format.</param>
        /// <param name="arg11">The eleventh object to format.</param>
        /// <param name="arg12">The twelfth object to format.</param>
        /// <param name="arg13">The thirteenth object to format.</param>
        /// <param name="arg14">The fourteenth object to format.</param>
        /// <param name="arg15">The fifteenth object to format.</param>
        /// <param name="arg16">The sixteenth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <typeparam name="T9">The type of the ninth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <typeparam name="T12">The type of the twelfth argument.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth argument.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth argument.</typeparam>
        /// <typeparam name="T16">The type of the sixteenth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Locale locale,
            string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12,
            T13 arg13,
            T14 arg14,
            T15 arg15,
            T16 arg16)
        {
            var translationData = GetTranslationData(locale, key);
            return translationData?.GetFormattedText(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16) ?? TranslationData.DefaultText;
        }

        /// <summary>
        /// Gets the formatted translation text for current locale.
        /// </summary>
        /// <param name="key">The key of translation text.</param>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <param name="arg5">The fifth object to format.</param>
        /// <param name="arg6">The sixth object to format.</param>
        /// <param name="arg7">The seventh object to format.</param>
        /// <param name="arg8">The eighth object to format.</param>
        /// <param name="arg9">The ninth object to format.</param>
        /// <param name="arg10">The tenth object to format.</param>
        /// <param name="arg11">The eleventh object to format.</param>
        /// <param name="arg12">The twelfth object to format.</param>
        /// <param name="arg13">The thirteenth object to format.</param>
        /// <param name="arg14">The fourteenth object to format.</param>
        /// <param name="arg15">The fifteenth object to format.</param>
        /// <param name="arg16">The sixteenth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <typeparam name="T5">The type of the fifth argument.</typeparam>
        /// <typeparam name="T6">The type of the sixth argument.</typeparam>
        /// <typeparam name="T7">The type of the seventh argument.</typeparam>
        /// <typeparam name="T8">The type of the eighth argument.</typeparam>
        /// <typeparam name="T9">The type of the ninth argument.</typeparam>
        /// <typeparam name="T10">The type of the tenth argument.</typeparam>
        /// <typeparam name="T11">The type of the eleventh argument.</typeparam>
        /// <typeparam name="T12">The type of the twelfth argument.</typeparam>
        /// <typeparam name="T13">The type of the thirteenth argument.</typeparam>
        /// <typeparam name="T14">The type of the fourteenth argument.</typeparam>
        /// <typeparam name="T15">The type of the fifteenth argument.</typeparam>
        /// <typeparam name="T16">The type of the sixteenth argument.</typeparam>
        /// <returns>The formatted translation text string.</returns>
        /// <exception cref="ArgumentNullException">locale or key</exception>
        public string GetFormattedTranslationText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(string key,
            T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9,
            T10 arg10,
            T11 arg11,
            T12 arg12,
            T13 arg13,
            T14 arg14,
            T15 arg15,
            T16 arg16) =>
            GetFormattedTranslationText(CurrentLocale, key, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
    }
}