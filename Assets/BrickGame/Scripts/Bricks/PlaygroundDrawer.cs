// <copyright file="PlaygroundDrawer.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 19:10</date>

using BrickGame.Scripts.Figures;
using BrickGame.Scripts.Models;
using JetBrains.Annotations;
using UnityEngine;

namespace BrickGame.Scripts.Bricks
{
    /// <summary>
    /// PlaygroundDrawer - component that resonsible for playground bricks state updating
    /// </summary>
    public class PlaygroundDrawer : BricksDrawer
    {
        //================================       Public Setup       =================================
        [Tooltip("Width in bricks of the component, can be changed by playground.")]
        public int Width = 10;
        [Tooltip("Height in bricks of the component, can be changed by playground.")]
        public int Height = 20;
        [Tooltip("Figure to draw.")]
        public Figure Figure;
        [Tooltip("Playground to draw.")]
        public Playgrounds.Playground Playground;
        //================================    Systems properties    =================================
        [NotNull] private Brick[] _bricks = new Brick[0];
        //================================      Public methods      =================================
        public void UpdateSize()
        {
            if(!Validate())return;
            _bricks = RestoreBricks(Width, Height, transform.localScale);
        }

        private void OnEnable()
        {
            UpdateSize();
        }
        //================================ Private|Protected methods ================================

        private void Update()
        {
            //Update form playground matrix
            if (Playground == null || Playground.Matrix.IsNull) return;
            Matrix<bool> matrix = Playground.Matrix;
            if (matrix.Width != Width || matrix.Height != Height)
            {
                Width = matrix.Width;
                Height = matrix.Height;
                UpdateSize();
            }

            for (int x = 0; x < matrix.Width; ++x)
            {
                for (int y = 0; y < matrix.Height; ++y)
                {
                    _bricks[x + y * matrix.Width].Active = matrix[x, y];
                }
            }

            if(Figure == null || Figure.Matrix.IsNull)return;
            FigureMatrix figure = Figure.Matrix;
            //Update from figureMatrix
            for (int x = 0; x < figure.Width; ++x)
            {
                for (int y = 0; y < figure.Height; ++y)
                {
                    int c = (figure.x + x) + (figure.y + y) * matrix.Width;
                    if(c < 0 || c >= _bricks.Length)continue;
                    _bricks[ c ].Active = figure[x, y];
                }
            }
        }
    }
}