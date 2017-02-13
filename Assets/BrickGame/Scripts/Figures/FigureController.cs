// <copyright file="FigureController.cs" company="Near Fancy">
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
    /// FigureController - concrete figure controller
    /// </summary>
    [RequireComponent(typeof(PlaygroundController))]
    public class FigureController : AbstractFigureController
    {
        //================================       Public Setup       =================================
        public Vector2 SpawnCenter;
        //================================    Systems properties    =================================
        private int _x;
        private int _y;
        private float _timer;
        private List<int> _previous;
        private PlaygroundModel _model;
        private PlaygroundController _controller;
        //================================      Public methods      =================================
        /// <inheritdoc />
        public override bool Turn()
        {
            if (Figure == null) return false;
            int tempX = _x;
            int tempY = _y;
            int width = Figure.GetLength(0);
            int height = Figure.GetLength(1);
            bool[,] temp = Figure;
            bool[,] matrix = new bool[height,width];
            int x, y;
            for (x = 0; x < width; x++)
            {
                for (y = 0; y < height; y++)
                    matrix[y, x] = Figure[ x, (height - 1) - y];
            }
            x = _x + (int)Mathf.Floor(width * 0.5F);
            y = _y + (int)Mathf.Floor(height * 0.5F);
            width = matrix.GetLength(0);
            height = matrix.GetLength(1);
            _x = x - (int)Mathf.Floor(width * 0.5F);
            _y = y - (int)Mathf.Floor(height * 0.5F);
            Figure = matrix;
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
            if (Figure == null) return false;
            return ShiftFigure(-1);
        }

        /// <inheritdoc />
        public override bool MoveRight()
        {
            if (Figure == null) return false;
            return ShiftFigure(1);
        }

        /// <inheritdoc />
        public override bool MoveDown()
        {
            if (Figure == null) return false;
            return ShiftFigure(0, 1);
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
        /// Update figure cells in model
        /// </summary>
        private void Update()
        {
            int x, y;
            //Clean previous cells
            for (int i = 0; i < _previous.Count; i += 2)
            {
                x = _previous[i];
                y = _previous[i + 1];
                _model[x, y] = false;
            }
            _previous.Clear();
            if (Figure == null) return;
            //Set figure cells
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
        /// Try to shift figure
        /// </summary>
        /// <param name="xShift"></param>
        /// <param name="yShift"></param>
        /// <returns></returns>
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
                    int y0 = _y + y;
                    if (!Figure[x, y] || y0 < 0) continue;
                    int x0 = _x + x;
                    if (y0 >= _model.Height || x0 < 0 || x0 > _model.Width - 1) return false;
                    //interaction with other cells
                    if (_model[x0, y0])
                    {
                        bool previous = false;
                        for (int i = 0; i < _previous.Count; i += 2)
                        {
                            if (x0 == _previous[i] && y0 == _previous[i + 1])
                            {
                                previous = true;
                                break;
                            }
                        }
                        if (previous) continue;
                        return false;
                    }
                }
            }
            return true;
        }
    }
}