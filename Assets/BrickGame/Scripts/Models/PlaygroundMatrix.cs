// <copyright file="PlaygroundMatrix.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/18/2017 18:56</date>

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

        /// <inheritdoc />
        public override bool this[int x, int y]
        {
            //TODO: mix figure cells to matrix (without matrix updating)
            get { return base[x, y]; }
        }
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
        /// Move rows value down to bottom of playground matrix
        /// </summary>
        /// <param name="from">From row</param>
        /// <param name="to">To row</param>
        public void MoveDownRows(int @from, int to)
        {
            do
            {
                for (int x = 0; x < Width; ++x)
                    this[x, to] = this[x, @from];
                @from--; to--;

            } while (@from > 0);
        }

        /// <summary>
        /// Fill row of playground matrix with specified value
        /// </summary>
        /// <param name="row">Index of row</param>
        /// <param name="value">Specified vlaue to fill the row</param>
        public void FillRow(int row, bool value = false)
        {
            for (int x = 0; x < Width; ++x)this[x, row] = value;
        }
        //================================ Private|Protected methods ================================
    }
}