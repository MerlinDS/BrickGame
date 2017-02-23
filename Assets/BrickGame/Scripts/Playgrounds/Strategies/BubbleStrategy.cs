// <copyright file="BubbleStrategy.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/23/2017 19:34</date>

using BrickGame.Scripts.Models;

namespace BrickGame.Scripts.Playgrounds.Strategies
{
    /// <summary>
    /// BubbleStrategy
    /// </summary>
    public class BubbleStrategy : AbstractStrategy
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected override void Apply(Matrix<bool> matrix)
        {
            for (int x = 0; x < matrix.Width; x+=2)
            {
                if (matrix[x, 0]) matrix[x, 0] = false;
            }
        }
    }
}