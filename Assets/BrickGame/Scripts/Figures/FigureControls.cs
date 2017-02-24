// <copyright file="FigureControls.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/18/2017 21:05</date>

using BrickGame.Scripts.Controllers;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Utils;
using JetBrains.Annotations;

namespace BrickGame.Scripts.Figures
{
    /// <summary>
    /// FigureControls - a matrix controls.
    /// </summary>
    public class FigureControls : GameBehaviour, IFigureControls,
        MessageReceiver.IPlaygroundReceiver, MessageReceiver.IFigureReceiver
    {
        //================================       Public Setup       =================================
        /// <inheritdoc />
        public int X
        {
            get { return _matrix.x; }
        }

        /// <inheritdoc />
        public int Y
        {
            get { return _matrix.y; }
        }

        /// <inheritdoc />
        public bool OutOfEdge {
            get
            {
                switch (_edge)
                {
                    case VerticalDirection.Up:
                        return _matrix.y + _matrix.Height > _playground.Height;
                    case VerticalDirection.Down:
                        return _matrix.y < 0;
                }
                return true;
            }
        }

        //================================    Systems properties    =================================

        private VerticalDirection _edge;
        /// <summary>
        /// FigureMatrix matrix, initialized with empty object
        /// </summary>
        [NotNull] private FigureMatrix _matrix = new FigureMatrix();

        /// <summary>
        /// Playground matrix (model of the playground), initialized with empty object
        /// </summary>
        [NotNull] private Matrix<bool> _playground = new Matrix<bool>(0, 0);

        //================================     Receiver methods      ================================
        /// <inheritdoc />
        public void UpdateMatix(Matrix<bool> matrix)
        {
            _playground = matrix ?? new Matrix<bool>(0, 0);
             _edge = Context.GetActor<GameModeManager>().CurrentRules.FallingDirection;
        }

        /// <inheritdoc />
        public void UpdateFigure(FigureMatrix matrix)
        {
            _matrix = matrix ?? new FigureMatrix();
        }

        //================================      Public methods      =================================
        /// <inheritdoc />
        public void Rotate(bool clockwise = true)
        {
            int x = _matrix.x;
            int y = _matrix.y;
            //Calculate new position of the matrix and
            if (_matrix.Width != _matrix.Height)
            {
                //Get new center of the matrix
                x = x + (int) ((_matrix.Width - _matrix.Height) * 0.5F);
                y = y + (int) ((_matrix.Height - _matrix.Width) * 0.5F);
                //shift from borders
                if (x < 0) x = 0;
                else if (x + _matrix.Height >= _playground.Width)
                    x = _playground.Width - _matrix.Height;
            }
            _matrix.Rotate(clockwise);
            //Check if matrix can be rotated
            if (_playground.HasIntersection(_matrix, x, y))
            {
                //Rotate matrix back
                _matrix.Rotate(!clockwise);
                return;
            }
            //set new position to the matrix
            _matrix.x = x;
            _matrix.y = y;
        }

        /// <inheritdoc />
        public bool CanMoveHorizontal(int xShift)
        {
            if (_matrix.IsNull) return false;
            int x = _matrix.x + xShift;
            //Check bounds
            if (x < 0 || x + _matrix.Width > _playground.Width) return false;
            //Check intersection
            return !_playground.HasIntersection(_matrix, x, _matrix.y);
        }

        /// <inheritdoc />
        public void MoveHorizontal(int xShift)
        {
            if (xShift != 0 && CanMoveHorizontal(xShift))
                _matrix.x += xShift;
        }

        /// <inheritdoc />
        public bool CanMoveVertical(int yShift)
        {
            if (_matrix.IsNull) return false;
            int y = _matrix.y + yShift;
            //Check bounds: ignore top bounds
            if(_edge == VerticalDirection.Down && y + _matrix.Height > _playground.Height) return false;
            if(_edge == VerticalDirection.Up && y < 0) return false;
            //Check intersection
            return !_playground.HasIntersection(_matrix, _matrix.x, y);
        }

        /// <inheritdoc />
        public void MoveVertical(int yShift)
        {
            if (yShift != 0 && CanMoveVertical(yShift))
                _matrix.y += yShift;
        }

        //================================ Private|Protected methods ================================
    }
}