// <copyright file="Figure.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 19:41</date>

using BrickGame.Scripts.Models;
using JetBrains.Annotations;
using UnityEngine;

namespace BrickGame.Scripts.Figures
{
    /// <summary>
    /// Figure - Component represent access to
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("BrickGame/Game Components/Figure")]
    [RequireComponent(typeof(FigureController))]
    public class Figure : GameBehaviour, MessageReceiver.IFigureReceiver, MessageReceiver.IPlaygroundReceiver
    {
        //================================       Public Setup       =================================
        /// <summary>
        /// Current speed of the figure
        /// </summary>
        public float Speed { get; private set; }
        [NotNull]
        public FigureMatrix Matrix { get { return _matrix; } }
        //================================    Systems properties    =================================
        [NotNull]private FigureMatrix _matrix = new FigureMatrix();
        [NotNull]private Matrix<bool> _playground = new PlaygroundMatrix(0, 0);
        //================================      Public methods      =================================
        /// <inheritdoc />
        public void UpdateFigure(FigureMatrix matrix)
        {
            _matrix = matrix ?? new FigureMatrix();
        }

        /// <inheritdoc />
        public void UpdateMatix(Matrix<bool> matrix)
        {
            _playground = matrix ?? new PlaygroundMatrix(0, 0);
        }

        /// <summary>
        /// Update figure speed
        /// </summary>
        /// <param name="speed">new speed of the figure</param>
        [UsedImplicitly]
        private void AccelerateFigure(float speed)
        {
            Speed = speed;
        }

        /// <summary>
        /// Append figure data to playground matrix
        /// </summary>
        [UsedImplicitly]
        public void AppendFigure()
        {
            if(_matrix.IsNull || _playground.IsNull)return;
            for (int x = 0; x < _matrix.Width; ++x)
            {
                for (int y = 0; y < _matrix.Height; ++y)
                {
                    if(!_matrix[x, y])continue;
                    _playground[_matrix.x + x, _matrix.y + y] = _matrix[x, y];
                }
            }
        }
        //================================ Private|Protected methods ================================
    }
}