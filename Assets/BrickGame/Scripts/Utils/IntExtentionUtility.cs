// <copyright file="IntExtentionUtility.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/13/2017 11:27</date>
namespace BrickGame.Scripts.Utils
{
    /// <summary>
    /// IntExtentionUtility
    /// </summary>
    public static class IntExtentionUtility
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
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