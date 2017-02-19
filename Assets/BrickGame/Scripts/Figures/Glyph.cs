// <copyright file="Glyph.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 17:09</date>

using System;
using UnityEngine;

namespace BrickGame.Scripts.Figures
{
    //TODO: Mode to model package
    /// <summary>
    /// Glyph of figureMatrix
    /// </summary>
    public class Glyph : ScriptableObject
    {
        //================================       Public Setup       =================================
        public readonly int Width;

        public readonly int Height;

        public bool this[int x, int y]
        {
            get { return _matrix[x + y * Width]; }
            set { _matrix[x + y * Width] = value; }
        }
        //================================    Systems properties    =================================
        [SerializeField] [HideInInspector] private bool[] _matrix;
        //================================      Public methods      =================================
        /// <inheritdoc />
        public Glyph()
        {
            Width = 4;
            Height = 2;
            _matrix = new bool[Width * Height];
        }

        /// <summary>
        /// Get figureMatrix matrix from empty borders
        /// </summary>
        /// <returns></returns>
        [Obsolete("Method is deprecated. Use Flip method if Matrix<> instead")]
        public bool[,] GetFlippedFigureMatrix()
        {
            bool[,] t = GetFigureMatrix();
            int w = t.GetLength(0);
            int h = t.GetLength(1);
            bool[,] c = new bool[w, h];
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                    c[x, y] = t[(w - 1) - x, y];
            }
            return c;
        }

        /// <summary>
        /// Get figureMatrix matrix from empty borders
        /// </summary>
        /// <returns>matrix of figureMatrix</returns>
        public bool[,] GetFigureMatrix()
        {
            int i, x = 1, y = 1, width = Width - 1, height = Height - 1;
            //
            for (i = 0; i < Height; ++i)
            {
                if (!this[0, i]) continue;
                x = 0;
                break;
            }
//
            for (i = 0; i < Height; ++i)
            {
                if (!this[Width - 1, i]) continue;
                width += 1;
                break;
            }
            //
            for (i = 0; i < Width; ++i)
            {
                if (!this[i, 0]) continue;
                y = 0;
                break;
            }
            //
            for (i = 0; i < Height; ++i)
            {
                if (!this[i, Height - 1]) continue;
                height += 1;
                break;
            }
            width = width - x;
            height = height - y;
            bool[,] cells = new bool[width, height];

            for (int y0 = 0; y0 < height; y0++)
            {
                for (int x0 = 0; x0 < width; x0++)
                    cells[x0, y0] = _matrix[(x + x0) + (y + y0) * Width];
            }

            return cells;
        }
        //================================ Private|Protected methods ================================
    }
}