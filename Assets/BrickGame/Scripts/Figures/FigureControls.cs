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
    /// FigureControls - a figure controls.
    /// </summary>
    public class FigureControls : GameBehaviour, IFigureControls,
        MessageReceiver.IPlaygroundReceiver, MessageReceiver.IFigureReceiver
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        /// <summary>
        /// Figure matrix, initialized with empty object
        /// </summary>
        [NotNull] private Figure _figure = new Figure();
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
        public void UpdateFigure(Figure figure)
        {
            _figure = figure ?? new Figure();
        }
        //================================      Public methods      =================================
        /// <inheritdoc />
        public void Rotate(bool clockwise = true)
        {
            _figure.Rotate(clockwise);
            int x = _figure.x;
            int y = _figure.y;
            //Calculate new position of the figure and
            if (_figure.Width != _figure.Height)
            {
                //Get new center of the figure
                x = x + (int) ((_figure.Width - _figure.Height) * 0.5F);
                y = y + (int) ((_figure.Height - _figure.Width) * 0.5F);
                //shift from borders
                if (x < 0) x = 0;
                else if (x + _figure.Height >= _matrix.Width)
                    x = _matrix.Width - _figure.Height;
            }
            //Check if figure can be rotated
            if (!_matrix.HasIntersection(_figure, x, y))
            {
                //Rotate figure back
                _figure.Rotate(!clockwise);
                return;
            }
            //set new position to the figure
            _figure.x = x;
            _figure.y = y;

        }

        /// <inheritdoc />
        public bool CanMoveHorizontal(int xShift)
        {
            int x = _figure.x + xShift;
            //Check bounds
            if (x < 0 || x + _figure.Width > _matrix.Width) return false;
            //Check intersection
            return _matrix.HasIntersection(_figure, x, _figure.y);
        }

        /// <inheritdoc />
        public void MoveHorizontal(int xShift)
        {
            if(xShift == 0 || !CanMoveHorizontal(xShift))return;
            _figure.x += xShift;
        }

        /// <inheritdoc />
        public bool CanMoveVertical(int yShift)
        {
            int y = _figure.x + yShift;
            //Check bounds
            if (y < 0 || y + _figure.Height > _matrix.Height) return false;
            //Check intersection
            return _matrix.HasIntersection(_figure, _figure.x, y);
        }

        /// <inheritdoc />
        public void MoveVertical(int yShift)
        {
            if(yShift == 0 || CanMoveHorizontal(yShift))return;
            _figure.y -= yShift;
        }
        //================================ Private|Protected methods ================================
        /// <summary>
        /// Initialize controls
        /// </summary>
        private void Start()
        {
            if (!gameObject.CompareTag(SRTags.Player) || !gameObject.CompareTag(SRTags.AI))
            {
                Debug.LogWarning("Controls not set to play or AI, and will be set to AI automatically!");
                gameObject.tag = SRTags.AI;
            }
            Matrix<bool>.Copy(_figure, _figure);
        }
    }
}