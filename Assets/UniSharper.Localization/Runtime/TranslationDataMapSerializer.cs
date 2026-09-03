// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Buffers;
using System.Collections.Generic;
using MemoryPack;
using MemoryPack.Compression;

namespace UniSharper.Localization
{
    internal static class TranslationDataMapSerializer
    {
#if UNITY_EDITOR
        internal static byte[] Serialize(Dictionary<string, TranslationData> map, bool useBrotliCompression)
        {
            byte[] data;
            if (useBrotliCompression)
            {
                using var compressor = new BrotliCompressor();
                MemoryPackSerializer.Serialize(compressor, map);
                data = compressor.ToArray();
            }
            else
            {
                data = MemoryPackSerializer.Serialize(map);
            }

            var totalLength = data.Length + 1;
            
            // Combine data.
            var result = ArrayPool<byte>.Shared.Rent(totalLength);
            result[0] = useBrotliCompression ? (byte)1 : (byte)0;
            Buffer.BlockCopy(data, 0, result, 1, data.Length);
            
            // Output final result.
            var finalResult = new byte[totalLength];
            Buffer.BlockCopy(result, 0, finalResult, 0, totalLength);
            return finalResult;
        }
#endif

        internal static TranslationDataMap Deserialize(byte[] data, bool useInternStringPool = true)
        {
            Dictionary<string, TranslationData> map;
            ReadOnlySpan<byte> span = data;
            var compressionFlag = span[0];
            if (compressionFlag == 1)
            {
                // Use Brotli compression
                using var decompressor = new BrotliDecompressor();
                var decompressedBuffer = decompressor.Decompress(span[1..]);
                map = MemoryPackSerializer.Deserialize<Dictionary<string, TranslationData>>(decompressedBuffer);
            }
            else
            {
                // No compression
                map = MemoryPackSerializer.Deserialize<Dictionary<string, TranslationData>>(span[1..]);
            }

            return useInternStringPool ? ProcessWithInternStringPool(map) : new TranslationDataMap(map);
        }
        
        private static TranslationDataMap ProcessWithInternStringPool(Dictionary<string, TranslationData> source)
        {
            var map = new TranslationDataMap();

            foreach (var (key, translationData) in source)
            {
                var internedKey = string.IsInterned(key) ?? string.Intern(key);
                var internedText = string.IsInterned(translationData.Text) ?? string.Intern(translationData.Text);
                translationData.Text = internedText;
                map[internedKey] = translationData;
            }
            
            return map;
        }
    }
}