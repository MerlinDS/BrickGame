// <copyright file="WatterFallStrategy.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/23/2017 20:15</date>

using BrickGame.Scripts.Figures;
using BrickGame.Scripts.Models;
using UnityEngine;

namespace BrickGame.Scripts.Playgrounds.Strategies
{
    /// <summary>
    /// WatterFallStrategy
    /// </summary>
    public class WatterFallStrategy : AbstractStrategy
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected sealed override void Apply(Playground playground, Figure figure)
        {
            Matrix<bool> matrix = playground.Matrix;
            bool[] temp = GetRowWithRandoms(matrix.Width);

            //Slide matrix up and set random row to last row
            for (int y = 0; y < matrix.Height; y++)
            {
                for (int x = 0; x < matrix.Width; x++)
                {
                    matrix[x, y - 1] = matrix[x, y];
                    matrix[x, y] = temp[x];
                }
            }
            if (matrix.HasIntersection(figure.Matrix, figure.Matrix.x, figure.Matrix.y))
            {
                figure.Matrix.y--;
            }
        }

        private bool[] GetRowWithRandoms(int width)
        {
            bool[] row = new bool[width];
            int y = width;
            //Random starting cell
            int x = Random.Range(0, width - 1);
            int setted = 0;
            while (y-- > 0)
            {
                if (x >= width) x = 0;
                if (!(row[x++] = Random.Range(setted, width) > width * 0.25F)) setted++;
            }
            //Prevent row with all true cells
            if (setted >= width) row[--x] = true;
            return row;
        }
    }
}