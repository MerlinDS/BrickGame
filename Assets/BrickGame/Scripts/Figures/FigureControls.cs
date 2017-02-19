// <copyright file="FigureControls.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/18/2017 21:05</date>

using BrickGame.Scripts.Models;
using JetBrains.Annotations;
using UnityEngine;

namespace BrickGame.Scripts.Figures
{
    /// <summary>
    /// FigureControls - a figureMatrix controls.
    /// </summary>
    public class FigureControls : GameBehaviour, IFigureControls,
        MessageReceiver.IPlaygroundReceiver, MessageReceiver.IFigureReceiver
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        /// <summary>
        /// FigureMatrix matrix, initialized with empty object
        /// </summary>
        [NotNull] private FigureMatrix _figureMatrix = new FigureMatrix();
        /// <summary>
        /// Playground matrix (model of the playground), initialized with empty object
        /// </summary>
        [NotNull] private Matrix<bool> _matrix = new Matrix<bool>(0, 0);
        //================================     Receiver methods      ================================
        /// <inheritdoc />
        public void UpdateMatix(Matrix<bool> matrix)
        {
            _matrix = matrix ?? new Matrix<bool>(0, 0);
        }

        /// <inheritdoc />
        public void UpdateFigure(FigureMatrix figureMatrix)
        {
            _figureMatrix = figureMatrix ?? new FigureMatrix();
        }
        //================================      Public methods      =================================
        /// <inheritdoc />
        public void Rotate(bool clockwise = true)
        {
            _figureMatrix.Rotate(clockwise);
            int x = _figureMatrix.x;
            int y = _figureMatrix.y;
            //Calculate new position of the figureMatrix and
            if (_figureMatrix.Width != _figureMatrix.Height)
            {
                //Get new center of the figureMatrix
                x = x + (int) ((_figureMatrix.Width - _figureMatrix.Height) * 0.5F);
                y = y + (int) ((_figureMatrix.Height - _figureMatrix.Width) * 0.5F);
                //shift from borders
                if (x < 0) x = 0;
                else if (x + _figureMatrix.Height >= _matrix.Width)
                    x = _matrix.Width - _figureMatrix.Height;
            }
            //Check if figureMatrix can be rotated
            if (!_matrix.HasIntersection(_figureMatrix, x, y))
            {
                //Rotate figureMatrix back
                _figureMatrix.Rotate(!clockwise);
                return;
            }
            //set new position to the figureMatrix
            _figureMatrix.x = x;
            _figureMatrix.y = y;

        }

        /// <inheritdoc />
        public bool CanMoveHorizontal(int xShift)
        {
            int x = _figureMatrix.x + xShift;
            //Check bounds
            if (x < 0 || x + _figureMatrix.Width > _matrix.Width) return false;
            //Check intersection
            return !_matrix.HasIntersection(_figureMatrix, x, _figureMatrix.y);
        }

        /// <inheritdoc />
        public void MoveHorizontal(int xShift)
        {
            if(xShift != 0 && CanMoveHorizontal(xShift))
                _figureMatrix.x += xShift;
        }

        /// <inheritdoc />
        public bool CanMoveVertical(int yShift)
        {
            int y = _figureMatrix.y + yShift;
            //Check bounds
            if (y < 0 || y + _figureMatrix.Height > _matrix.Height) return false;
            //Check intersection
            return !_matrix.HasIntersection(_figureMatrix, _figureMatrix.y, y);
        }

        /// <inheritdoc />
        public void MoveVertical(int yShift)
        {
            if(yShift != 0 && CanMoveVertical(yShift))
                _figureMatrix.y += yShift;
        }
        //================================ Private|Protected methods ================================
        /// <summary>
        /// Initialize controls
        /// </summary>
        private void Start()
        {
            if (!gameObject.CompareTag(SRTags.Player) &&
                !gameObject.CompareTag(SRTags.AI))
            {
                Debug.LogWarning("Controls not set to play or AI, and will be set to AI automatically!");
                gameObject.tag = SRTags.AI;
            }
            Matrix<bool>.Copy(_figureMatrix, _figureMatrix);
        }
    }
}