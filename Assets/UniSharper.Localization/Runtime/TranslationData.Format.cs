using System;
using System.Runtime.CompilerServices;
using Cysharp.Text;

namespace UniSharper.Localization
{
    public partial class TranslationData
    {
        /// <summary>
        /// Gets the formatted text by replacing one or more format items in a string with the string representation of a specified object.
        /// </summary>
        /// <param name="arg1">The first object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <returns>The formatted text string, or the default text if the source text is null or empty.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetFormattedText<T1>(T1 arg1)
        {
            if (string.IsNullOrEmpty(Text))
                return DefaultText;

            try
            {
                return ZString.Format(Text, arg1);
            }
            catch (Exception)
            {
                return Text;
            }
        }

        /// <summary>
        /// Gets the formatted text by replacing one or more format items in a string with the string representation of two specified objects.
        /// </summary>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <returns>The formatted text string, or the default text if the source text is null or empty.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetFormattedText<T1, T2>(T1 arg1, T2 arg2)
        {
            if (string.IsNullOrEmpty(Text))
                return DefaultText;

            try
            {
                return ZString.Format(Text, arg1, arg2);
            }
            catch (Exception)
            {
                return Text;
            }
        }

        /// <summary>
        /// Gets the formatted text by replacing one or more format items in a string with the string representation of three specified objects.
        /// </summary>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <returns>The formatted text string, or the default text if the source text is null or empty.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetFormattedText<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
        {
            if (string.IsNullOrEmpty(Text))
                return DefaultText;

            try
            {
                return ZString.Format(Text, arg1, arg2, arg3);
            }
            catch (Exception)
            {
                return Text;
            }
        }

        /// <summary>
        /// Gets the formatted text by replacing one or more format items in a string with the string representation of four specified objects.
        /// </summary>
        /// <param name="arg1">The first object to format.</param>
        /// <param name="arg2">The second object to format.</param>
        /// <param name="arg3">The third object to format.</param>
        /// <param name="arg4">The fourth object to format.</param>
        /// <typeparam name="T1">The type of the first argument.</typeparam>
        /// <typeparam name="T2">The type of the second argument.</typeparam>
        /// <typeparam name="T3">The type of the third argument.</typeparam>
        /// <typeparam name="T4">The type of the fourth argument.</typeparam>
        /// <returns>The formatted text string, or the default text if the source text is null or empty.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetFormattedText<T1, T2, T3, T4>(T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4)
        {
            if (string.IsNullOrEmpty(Text))
                return DefaultText;

            try
            {
                return ZString.Format(Text, arg1, arg2, arg3, arg4);
            }
            catch (Exception)
            {
                return Text;
            }
        }

        /// <summary>
        /// Gets the formatted text by replacing one or more format items in a string with the string representation of five specified objects.
        /// </summary>
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
        /// <returns>The formatted text string, or the default text if the source text is null or empty.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetFormattedText<T1, T2, T3, T4, T5>(T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5)
        {
            if (string.IsNullOrEmpty(Text))
                return DefaultText;

            try
            {
                return ZString.Format(Text, arg1, arg2, arg3, arg4, arg5);
            }
            catch (Exception)
            {
                return Text;
            }
        }

        /// <summary>
        /// Gets the formatted text by replacing one or more format items in a string with the string representation of six specified objects.
        /// </summary>
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
        /// <returns>The formatted text string, or the default text if the source text is null or empty.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetFormattedText<T1, T2, T3, T4, T5, T6>(T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6)
        {
            if (string.IsNullOrEmpty(Text))
                return DefaultText;

            try
            {
                return ZString.Format(Text, arg1, arg2, arg3, arg4, arg5, arg6);
            }
            catch (Exception)
            {
                return Text;
            }
        }
        
        /// <summary>
        /// Gets the formatted text by replacing one or more format items in a string with the string representation of seven specified objects.
        /// </summary>
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
        /// <returns>The formatted text string, or the default text if the source text is null or empty.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetFormattedText<T1, T2, T3, T4, T5, T6, T7>(T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7)
        {
            if (string.IsNullOrEmpty(Text))
                return DefaultText;
            
            try
            {
                return ZString.Format(Text, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            }
            catch (Exception)
            {
                return Text;
            }
        }

        /// <summary>
        /// Gets the formatted text by replacing one or more format items in a string with the string representation of eight specified objects.
        /// </summary>
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
        /// <returns>The formatted text string, or the default text if the source text is null or empty.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetFormattedText<T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8)
        {
            if (string.IsNullOrEmpty(Text))
                return DefaultText;
            
            try
            {
                return ZString.Format(Text, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            }
            catch (Exception)
            {
                return Text;
            }
        }

        /// <summary>
        /// Gets the formatted text by replacing one or more format items in a string with the string representation of nine specified objects.
        /// </summary>
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
        /// <returns>The formatted text string, or the default text if the source text is null or empty.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetFormattedText<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 arg1,
            T2 arg2,
            T3 arg3,
            T4 arg4,
            T5 arg5,
            T6 arg6,
            T7 arg7,
            T8 arg8,
            T9 arg9)
        {
            if (string.IsNullOrEmpty(Text))
                return DefaultText;
            
            try
            {
                return ZString.Format(Text, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            }
            catch (Exception)
            {
                return Text;
            }
        }

        /// <summary>
        /// Gets the formatted text by replacing one or more format items in a string with the string representation of ten specified objects.
        /// </summary>
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
        /// <returns>The formatted text string, or the default text if the source text is null or empty.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetFormattedText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 arg1,
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
            if (string.IsNullOrEmpty(Text))
                return DefaultText;
            
            try
            {
                return ZString.Format(Text, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            }
            catch (Exception)
            {
                return Text;
            }
        }

        /// <summary>
        /// Gets the formatted text by replacing one or more format items in a string with the string representation of eleven specified objects.
        /// </summary>
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
        /// <returns>The formatted text string, or the default text if the source text is null or empty.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetFormattedText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T1 arg1,
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
            if (string.IsNullOrEmpty(Text))
                return DefaultText;

            try
            {
                return ZString.Format(Text, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
            }
            catch (Exception)
            {
                return Text;
            }
        }

        /// <summary>
        /// Gets the formatted text by replacing one or more format items in a string with the string representation of twelve specified objects.
        /// </summary>
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
        /// <returns>The formatted text string, or the default text if the source text is null or empty.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetFormattedText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(T1 arg1,
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
            if (string.IsNullOrEmpty(Text))
                return DefaultText;

            try
            {
                return ZString.Format(Text, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
            }
            catch (Exception)
            {
                return Text;
            }
        }
        
        /// <summary>
        /// Gets the formatted text by replacing one or more format items in a string with the string representation of thirteen specified objects.
        /// </summary>
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
        /// <returns>The formatted text string, or the default text if the source text is null or empty.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetFormattedText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(T1 arg1,
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
            if (string.IsNullOrEmpty(Text))
                return DefaultText;

            try
            {
                return ZString.Format(Text, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
            }
            catch (Exception)
            {
                return Text;
            }
        }
        
        /// <summary>
        /// Gets the formatted text by replacing one or more format items in a string with the string representation of fourteen specified objects.
        /// </summary>
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
        /// <returns>The formatted text string, or the default text if the source text is null or empty.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetFormattedText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(T1 arg1,
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
            if (string.IsNullOrEmpty(Text))
                return DefaultText;

            try
            {
                return ZString.Format(Text, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
            }
            catch (Exception)
            {
                return Text;
            }
        }
        
        /// <summary>
        /// Gets the formatted text by replacing one or more format items in a string with the string representation of fifteen specified objects.
        /// </summary>
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
        /// <returns>The formatted text string, or the default text if the source text is null or empty.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetFormattedText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(T1 arg1,
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
            if (string.IsNullOrEmpty(Text))
                return DefaultText;

            try
            {
                return ZString.Format(Text, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
            }
            catch (Exception)
            {
                return Text;
            }
        }
        
        /// <summary>
        /// Gets the formatted text by replacing one or more format items in a string with the string representation of sixteen specified objects.
        /// </summary>
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
        /// <returns>The formatted text string, or the default text if the source text is null or empty.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetFormattedText<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(T1 arg1,
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
            if (string.IsNullOrEmpty(Text))
                return DefaultText;

            try
            {
                return ZString.Format(Text, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
            }
            catch (Exception)
            {
                return Text;
            }
        }
    }
}