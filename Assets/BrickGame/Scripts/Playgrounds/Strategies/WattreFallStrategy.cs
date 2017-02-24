// <copyright file="WattreFallStrategy.cs" company="Near Fancy">
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
    /// WattreFallStrategy
    /// </summary>
    public class WattreFallStrategy : AbstractStrategy
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected sealed override void Apply(Playground playground, Figure figure)
        {

        }
      /*  protected override void Apply(Matrix<bool> matrix, Figure figure)
        {
            bool[] temp = new bool[matrix.Width];
            int y = matrix.Width;
            int x = Random.Range(0, matrix.Width - 1);
            int setted = 0;
            while (y-- > 0)
            {
                if (x >= matrix.Width) x = 0;
                float rand = Random.Range(setted, matrix.Width);
                temp[x] = rand > matrix.Width * 0.25F;
                if (!temp[x++]) setted++;
            }
            if (setted >= matrix.Width) temp[--x] = true;

            for (y = 0; y < matrix.Height; y++)
            {
                for (x = 0; x < matrix.Width; x++)
                {
                    matrix[x, y-1] = matrix[x, y];
                    matrix[x, y] = temp[x];
                }
            }

        }*/
    }
}