// <copyright file="SliederStrategy.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/23/2017 20:28</date>

using BrickGame.Scripts.Models;

namespace BrickGame.Scripts.Playgrounds.Strategies
{
    /// <summary>
    /// SliederStrategy
    /// </summary>
    public class SliederStrategy : AbstractStrategy
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected override void Apply(Matrix<bool> matrix, FigureMatrix figure)
        {
            for (int y = 0; y < matrix.Height; y++)
            {
                for (int x = matrix.Width - 1; x > 0; x--)
                {
                    int target = x == matrix.Width - 1 ? 0 : x + 1;
                    bool temp = matrix[target, y];
                    matrix[target, y] = matrix[x, y];
                    matrix[x, y] = temp;
                }
            }
        }
    }
}