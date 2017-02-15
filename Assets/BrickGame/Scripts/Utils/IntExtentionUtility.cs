// <copyright file="IntExtentionUtility.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/13/2017 11:27</date>

using System;

namespace BrickGame.Scripts.Utils
{
    /// <summary>
    /// IntExtentionUtility
    /// </summary>
    public static class IntExtentionUtility
    {
        private const string HexPrefix = "0x";
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        public static string ToHex(this int value)
        {
            string hex = String.Format("{0:X}", value);
            if (hex.Length < 2) hex = '0' + hex;
            return hex;
        }

        public static int FormHex(this string value)
        {
            if (value.StartsWith(HexPrefix, StringComparison.OrdinalIgnoreCase))
                value = value.Substring(HexPrefix.Length);
            return int.Parse(value, System.Globalization.NumberStyles.HexNumber);
        }
        /// <summary>
        /// Calculate count of digits in integer value
        /// </summary>
        /// <param name="value">integer value</param>
        /// <returns>Count of digits in value</returns>
        public static int CountOfDigits(this int value)
        {
            if (value == 0) return 1;
            int count = 0;
            while (value > 0)
            {
                value /= 10;
                ++count;
            }
            return count;
        }
        //================================ Private|Protected methods ================================
    }
}