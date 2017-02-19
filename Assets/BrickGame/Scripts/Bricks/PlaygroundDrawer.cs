﻿// <copyright file="PlaygroundDrawer.cs" company="Near Fancy">
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
        public FigureMatrix FigureMatrix;
        //================================    Systems properties    =================================
        [NotNull] private FigureMatrix _figureMatrix = new FigureMatrix();
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
            //Update from figureMatrix
            for (int x = 0; x < _figureMatrix.Width; ++x)
            {
                for (int y = 0; y < _figureMatrix.Height; ++y)
                {
                    _bricks[ (_figureMatrix.x + x) + (_figureMatrix.y + y) * _matrix.Width].Active
                        = _figureMatrix[x, y];
                }
            }
        }
    }
}