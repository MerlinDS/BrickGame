﻿// <copyright file="FigureController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 18:16</date>

using System.Collections.Generic;
using BrickGame.Scripts.Playground;
using UnityEngine;

namespace BrickGame.Scripts.Figures
{
    /// <summary>
    /// FigureController - concrete figure controller.
    /// Provide controls for controlling figure matrix and position of the figure on playground.
    /// </summary>
    [RequireComponent(typeof(PlaygroundController))]
    public class FigureController : AbstractFigureController
    {
        //================================       Public Setup       =================================
        public Vector2 SpawnCenter;
        //================================    Systems properties    =================================
        /// <summary>
        /// X position of the figure on playground
        /// </summary>
        private int _x;
        /// <summary>
        /// Y position of the figure on playground
        /// </summary>
        private int _y;
        private float _timer;
        /// <summary>
        /// Coordinates of the previous active cells of the figure.
        /// [0] - x, [1] - y.
        /// </summary>
        private List<int> _previous;
        private PlaygroundModel _model;
        private PlaygroundController _controller;
        //================================      Public methods      =================================
        /// <inheritdoc />
        public override bool Turn()
        {
            if (Figure == null) return false;
            //Save current state of figure to temporary values
            int tempX = _x;
            int tempY = _y;
            bool[,] temp = Figure;
            //Create turned matrix and fill it with current figure values
            int width = Figure.GetLength(0);
            int height = Figure.GetLength(1);
            bool[,] matrix = new bool[height,width];
            int x, y;
            for (x = 0; x < width; x++)
            {
                for (y = 0; y < height; y++)
                    matrix[y, x] = Figure[ x, (height - 1) - y];
            }
            /*
                Calculate new position of the figure and
                save turned matrix as a current figure.
            */
            if (width != height)
            {
                //Set new center of the figure
                _x = _x + (int)((width - height) * 0.5F);
                _y = _y + (int)((height - width) * 0.5F);
                //shift from borders
                if (_x < 0) _x = 0;
                else if (_x + height >= _model.Width)
                    _x = _model.Width - height;
            }
            Figure = matrix;
            //If vigure can't be turned revert changes
            if (ValidateFigure())return true;
            _x = tempX;
            _y = tempY;
            Figure = temp;
            return false;
        }

        /// <inheritdoc />
        public override bool OutOfBounds
        {
            get
            {
                if (Figure == null) return false;
                return _y <= SpawnCenter.y;
            }
        }

        /// <inheritdoc />
        public override bool MoveLeft()
        {
            return Figure != null && ShiftFigure(-1);
        }

        /// <inheritdoc />
        public override bool MoveRight()
        {
            return Figure != null && ShiftFigure(1);
        }

        /// <inheritdoc />
        public override bool MoveDown()
        {
            //TODO BAG: Fix move down validate:
            return Figure != null && ShiftFigure(0, 1);
        }

        /// <inheritdoc />
        public override void Remove()
        {
            Figure = null;
            _previous.Clear();
        }


        //================================ Private|Protected methods ================================
        /// <summary>
        /// Initialize controller
        /// </summary>
        private void Start()
        {
            _previous = new List<int>();
            _controller = GetComponent<PlaygroundController>();
        }
        /// <summary>
        /// Create new figure
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private void CreateFigure()
        {
            PopFigure();
            _model = _controller.Model;//Update model
            _x = (int)(SpawnCenter.x  - Figure.GetLength(0) * 0.5F);
            _y = (int)(SpawnCenter.y  -  Figure.GetLength(1) * 0.5F);
            BroadcastNofitication(GameNotification.FigureChanged);
        }

        /// <summary>
        /// Update figure cells in playground matrix.
        /// Remove figure cells from previous place.
        /// Add figure cells to new place.
        /// </summary>
        private void Update()
        {
            int x, y;
            //Clean previous poosition of figure cells
            for (int i = 0; i < _previous.Count; i += 2)
            {
                x = _previous[i];
                y = _previous[i + 1];
                _model[x, y] = false;
            }
            _previous.Clear();
            if (Figure == null) return;
            //Set new position of figure cells
            int width = Figure.GetLength(0);
            int height = Figure.GetLength(1);
            for (x = 0; x < width; x++)
            {
                for (y = 0; y < height; y++)
                {
                    if(!Figure[x, y])continue;//update only pressent cells
                    _model[_x + x, _y + y] = true;
                    _previous.Add(_x + x);
                    _previous.Add(_y + y);
                }
            }
        }

        /// <summary>
        /// Try to shift figure in playground matrix
        /// </summary>
        /// <param name="xShift"></param>
        /// <param name="yShift"></param>
        /// <returns>True if shifting was succeseful, false in other case.</returns>
        private bool ShiftFigure(int xShift = 0, int yShift = 0)
        {
            _x += xShift;
            _y += yShift;
            if (ValidateFigure()) return true;
            _x -= xShift;
            _y -= yShift;
            return false;
        }

        /// <summary>
        /// Validate figure for placing on playground
        /// </summary>
        /// <returns>True if figure can be places on playground, false in other cases.</returns>
        private bool ValidateFigure()
        {
            int width = Figure.GetLength(0);
            int height = Figure.GetLength(1);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //Check if figure upper then playground matrix
                    int y0 = _y + y;
                    if (!Figure[x, y] || y0 < 0) continue;
                    //Check if figure is out of bounds of playground matrix
                    int x0 = _x + x;
                    if (y0 >= _model.Height || x0 < 0 || x0 > _model.Width - 1) return false;
                    //Check interaction with other figures (cells that not empty already)
                    if (_model[x0, y0])
                    {
                        //It cound be cells from previous figure position.
                        bool previous = false;
                        for (int i = 0; i < _previous.Count; i += 2)
                        {
                            if (x0 == _previous[i] && y0 == _previous[i + 1])
                            {
                                previous = true;
                                break;
                            }
                        }
                        //If figure contains current cell ignore it, if not, figure is invalid.
                        if (previous) continue;
                        return false;
                    }
                }
            }
            //Figure is valid
            return true;
        }
    }
}