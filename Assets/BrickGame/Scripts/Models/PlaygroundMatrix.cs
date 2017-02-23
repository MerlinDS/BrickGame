// <copyright file="PlaygroundMatrix.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/18/2017 18:56</date>

using System.Collections.Generic;
using BrickGame.Scripts.Utils;
using JetBrains.Annotations;

namespace BrickGame.Scripts.Models
{
    /// <summary>
    /// PlaygroundMatrix - matrix for playground data.
    ///  This is not stric matrix neather read only (can be modifyied).
    /// </summary>
    public class PlaygroundMatrix : Matrix<bool>
    {
        //================================       Public Setup       =================================
        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <inheritdoc />
        public PlaygroundMatrix(int width, int height) : base(width, height)
        {
        }

        /// <inheritdoc />
        public PlaygroundMatrix([NotNull] bool[] matrix, int width, int height) :
            base(matrix, width, height, false, false)
        {
        }

        /// <summary>
        /// Remove rows, Substitute empty rows and return list of removed lines.
        /// </summary>
        /// <param name="direction">Down edge direction.</param>
        /// <returns>Array of removed lines indexes</returns>
        public int[] RemoveRows(VerticalDirection direction)
        {
            List<int> rows = new List<int>();
            //Remove filled rows
            for (int y = 0; y < Height; y++)
            {
                if(!RowContainsValue(y, true))continue;
                rows.Add(y);
                FillRow(y);//Remove row from matrix
            }
            if(rows.Count > 0)SubstituteRows(direction == VerticalDirection.Down);
            return rows.ToArray();
        }
        //================================ Private|Protected methods ================================
    }
}