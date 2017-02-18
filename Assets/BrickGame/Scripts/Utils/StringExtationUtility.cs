// <copyright file="StringExtationUtility.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/18/2017 18:40</date>

using System.Text;
using BrickGame.Scripts.Models;

namespace BrickGame.Scripts.Utils
{
    /// <summary>
    /// StringExtationUtility
    /// </summary>
    public static class StringExtationUtility
{
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
    // ReSharper disable once InconsistentNaming
        private static readonly StringBuilder _sb = new StringBuilder();

        //================================      Public methods      =================================
        public static string Format<T>(this Matrix<T> matrix, bool asBit)
        {
            _sb.Remove(0, _sb.Length);
            for (int y = 0; y < matrix.Height; y++)
            {
                for (int x = 0; x < matrix.Width; x++)
                {
                    if (!asBit) _sb.Append(matrix[x, y]);
                    else _sb.Append(matrix[x, y].Equals(default(T)) ? 0 : 1);
                }
                _sb.AppendLine();
            }
            return _sb.ToString();
        }
        //================================ Private|Protected methods ================================
    }
}