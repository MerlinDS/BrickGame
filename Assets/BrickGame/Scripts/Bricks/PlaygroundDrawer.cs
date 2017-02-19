// <copyright file="PlaygroundDrawer.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 19:10</date>

using BrickGame.Scripts.Figures;
using BrickGame.Scripts.Models;
using JetBrains.Annotations;

namespace BrickGame.Scripts.Bricks
{
    /// <summary>
    /// PlaygroundDrawer - component that resonsible for playground bricks state updating
    /// </summary>
    public class PlaygroundDrawer : BricksDrawer
    {
        //================================       Public Setup       =================================
        public Figure Figure;
        //================================    Systems properties    =================================
        [NotNull] private Matrix<bool> _matrix = new Matrix<bool>(10,20);
        [NotNull] private Brick[] _bricks = new Brick[0];
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================

        private void OnEnable()
        {
            if(!Validate())return;
            _bricks = RestoreBricks(_matrix.Width, _matrix.Height, transform.localScale);
        }

        private void Update()
        {
            //Update form playground matrix
            for (int x = 0; x < _matrix.Width; ++x)
            {
                for (int y = 0; y < _matrix.Height; ++y)
                {
                    _bricks[x + y * _matrix.Width].Active = _matrix[x, y];
                }
            }

            if(Figure == null)return;
            FigureMatrix figure = Figure.Matrix;
            //Update from figureMatrix
            for (int x = 0; x < figure.Width; ++x)
            {
                for (int y = 0; y < figure.Height; ++y)
                {
                    int c = (figure.x + x) + (figure.y + y) * _matrix.Width;
                    if(c < 0 || c >= _bricks.Length)continue;
                    _bricks[ c ].Active = figure[x, y];
                }
            }
        }
    }
}