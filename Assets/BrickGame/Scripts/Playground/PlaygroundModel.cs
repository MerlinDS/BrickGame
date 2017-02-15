// <copyright file="PlaygroundModel.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 13:33</date>

namespace BrickGame.Scripts.Playground
{
    /// <summary>
    /// PlaygroundModel - model of the playground
    /// </summary>
    public class PlaygroundModel
    {
        //================================       Public Setup       =================================
        public bool ViewUp2date;
        /// <summary>
        /// Width of the playground
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Height of the playground
        /// </summary>
        public int Height { get; private set; }

        public int RemovedLines { get; private set; }

        /// <summary>
        /// Accessor by coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public bool this[int x, int y]
        {
            get { return _matrix[x + y * Width]; }
            set
            {
                if (x < 0 || x >= Width || y < 0 || y >= Height)return;
                int index = x + y * Width;
                if (_matrix[index] == value) return;
                ViewUp2date = false;
                _matrix[index] = value;
            }
        }

        /// <summary>
        /// Accessor for cells by Index
        /// </summary>
        /// <param name="index">Index of cell in liner array</param>
        public bool this[int index]
        {
            get { return _matrix[index]; }
        }
        //================================    Systems properties    =================================
        /// <summary>
        /// Playground matrix
        /// </summary>
        private readonly bool[] _matrix;
        //================================      Public methods      =================================
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="width">Width of the playground matrix (count of columns)</param>
        /// <param name="height">Height of the playground matrix (count of rows)</param>
        public PlaygroundModel(int width, int height)
        {
            Width = width;
            Height = height;
            _matrix = new bool[width * height];
        }

        public PlaygroundModel(int width, int height, bool[] matrix)
        {
            Width = width;
            Height = height;
            _matrix = matrix;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            string result = "Playground :\n";
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                    result += this[x, y] + ",";
                result = result.Remove(result.Length - 1) + "\n";
            }
            return result;
        }

        /// <summary>
        /// Check if line is full filled
        /// </summary>
        /// <param name="line">number of a line</param>
        /// <returns>If line full filled returns true, false in other case</returns>
        public bool FullLine(int line)
        {
            if (line < 0 || line > Height) return false;
            int offset = line * Width;
            for (int x = 0; x < Width; x++)
                if (!_matrix[x + offset]) return false;
            return true;
        }

        /// <summary>
        /// Move lines down
        /// </summary>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        public void MoveDown(int top, int bottom)
        {
            do
            {
                for (int x = 0; x < Width; ++x)
                    _matrix[x + bottom * Width] = _matrix[x + top * Width];
                top--;
                bottom--;

            } while (top >0);

        }

        public void RemoveLine(int y)
        {
            for (int x = 0; x < Width; ++x)this[x, y] = false;
            RemovedLines++;
        }
        //================================ Private|Protected methods ================================
    }
}