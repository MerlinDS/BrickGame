// <copyright file="DataConverter.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 20:54</date>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BrickGame.Scripts.Utils
{
    /// <summary>
    /// DataConverter - Utility class for converting playground data
    /// </summary>
    public static class DataConverter
    {
        //================================       Public Setup       =================================
        private const char Separator = '|';
        private const short ChunkSize = 16;//UInt166
        private const short BytesLength = ChunkSize / 8;
        private const short HexLength = ChunkSize / 4;
        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <summary>
        /// Convert a playground model to compressed string
        /// </summary>
        /// <param name="data">Playground model</param>
        /// <returns>Compressed string</returns>
        /// <exception cref="ArgumentNullException">data can't be null</exception>
        public static string ToString(bool[] data)
        {
            if(data == null)
                throw new ArgumentNullException("data");

            StringBuilder sb = new StringBuilder();
            BitArray bits = null;
            int i, n = data.Length;
            for (i = 0; i < n; i++)
            {
                int index = i % ChunkSize;
                if (index == 0)
                {
                    if (bits != null) sb.Append(ToString(bits));
                    bits = new BitArray(ChunkSize);
                }

                // ReSharper disable once PossibleNullReferenceException
                bits[index] = data[i];
            }

            if (bits != null && bits.Length > 0)
                sb.Append(ToString(bits));

            return sb.Append(Separator).ToString();
        }

        /// <summary>
        /// Convert compressed string to an array of bools.
        /// </summary>
        /// <param name="data">
        /// Compressed string, from method
        /// <see>
        ///     <cref>ToString(int[])</cref>
        /// </see>
        ///     or
        /// <see>
        ///     <cref>ToString(int[][])</cref>
        /// </see>
        /// </param>
        /// <returns>An array of bools that represent playground matrix</returns>
        /// <exception cref="ArgumentNullException">data can't be null</exception>
        public static bool[] GetMatrix(string data)
        {
            if(data == null)
                throw new ArgumentNullException("data");
            List<bool> result = new List<bool>();
            int i, j, m, n = data.Length / HexLength;
            for (i = 0; i < n; ++i)
            {
                byte[] bytes = new byte[BytesLength];
                string c = data.Substring(i * HexLength, HexLength);
                for (j = 0; j < BytesLength; ++j)
                {
                    string hex = c.Substring(j * 2, 2);
                    bytes[j] = Convert.ToByte( hex, ChunkSize);
                }
                BitArray bits = new BitArray(bytes);
                m = bits.Length;
                for (j = 0; j < m; ++j)result.Add(bits[j]);
            }
            return result.ToArray();
        }
        //================================ Private|Protected methods ================================
        /// <summary>
        /// Convert a bit array to hex string
        /// </summary>
        /// <param name="bits">an array of bits</param>
        /// <returns>Hex string</returns>
        private static string ToString(BitArray bits)
        {
            byte[] bytes = new byte[BytesLength];
            bits.CopyTo(bytes, 0);
            return BitConverter.ToString(bytes).Remove(2, 1);//Removal "-" from result
        }


    }
}