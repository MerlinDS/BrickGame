// <copyright file="FigureControls.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/18/2017 21:05</date>

using BrickGame.Scripts.Models;

namespace BrickGame.Scripts.Figures
{
    /// <summary>
    /// FigureControls
    /// </summary>
    public class FigureControls : IFigureContols
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private FigureMatrix _figure;
        private PlaygroundMatrix _playground;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
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
                else if (x + _figure.Height >= _playground.Width)
                    x = _playground.Width - _figure.Height;
            }
            //Check if figure can be rotated
            if (!_playground.HasIntersection(_figure, x, y))
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
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void MoveHorizontal(int xShift)
        {
            _figure.x += xShift;
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public bool CanMoveVertical(int yShift)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void MoveVertical(int yShift)
        {
            _figure.y = yShift;
            throw new System.NotImplementedException();
        }
    }
}