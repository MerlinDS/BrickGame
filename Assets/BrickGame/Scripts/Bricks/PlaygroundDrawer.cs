// <copyright file="PlaygroundDrawer.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 19:10</date>

using System.Collections.Generic;
using BrickGame.Scripts.Figures;
using BrickGame.Scripts.Models;
using JetBrains.Annotations;
using UnityEngine;

namespace BrickGame.Scripts.Bricks
{
    /// <summary>
    /// PlaygroundDrawer - component that resonsible for playground bricks state updating
    /// </summary>
    public class PlaygroundDrawer : BricksDrawer, MessageReceiver.IPlaygroundReceiver
    {
        //================================       Public Setup       =================================
        [Tooltip("Width in bricks of the component, can be changed by playground.")]
        public int Width = 10;
        [Tooltip("Height in bricks of the component, can be changed by playground.")]
        public int Height = 20;
        [Tooltip("Figure to draw.")]
        public Figure Figure;
        //================================    Systems properties    =================================
        [NotNull] private Matrix<bool> _matrix = new Matrix<bool>(0, 0);
        [NotNull] private Brick[] _bricks = new Brick[0];
        //================================      Public methods      =================================
        public void UpdateSize()
        {
            if(!Validate())return;
            _bricks = RestoreBricks(Width, Height, transform.localScale);
        }

        /// <inheritdoc />
        public void UpdateMatix(Matrix<bool> matrix)
        {
            _matrix = matrix ?? new PlaygroundMatrix(Width, Height);
            UpdateSize();
        }

        public void Pause()
        {
            if(!gameObject.activeInHierarchy)return;
            enabled = false;
        }

        public void Resume()
        {
            if(!gameObject.activeInHierarchy)return;
            enabled = true;
        }

        /// <inheritdoc />
        public void GetRow(int y, ref List<Brick> list)
        {
            if(y < 0 || y >= Height)return;
            if (list == null) list = new List<Brick>();
            for (int x = 0; x < Width; ++x)
                list.Add(_bricks[x + y * Width]);
        }
        //================================ Private|Protected methods ================================

        private void Start()
        {
            Context.AddListener(GameState.Start, StateHandler);
            Context.AddListener(GameState.Pause, StateHandler);
            Context.AddListener(GameState.End, StateHandler);
        }

        private void OnDestroy()
        {
            Context.RemoveListener(GameState.Start, StateHandler);
            Context.RemoveListener(GameState.Pause, StateHandler);
            Context.RemoveListener(GameState.End, StateHandler);
        }

        private void StateHandler(string state)
        {
            if(!gameObject.activeInHierarchy)return;
            switch (state)
            {
                case GameState.Start:
                    enabled = true;
                    break;
                case GameState.End:
                    enabled = false;
                    break;
                case GameState.Pause:
                    enabled = !enabled;
                    break;
            }
        }

        private void Update()
        {
            if(_matrix.IsNull)return;
            //Update form playground matrix
            if (_matrix.Width != Width || _matrix.Height != Height)
            {
                Width = _matrix.Width;
                Height = _matrix.Height;
                UpdateSize();
            }

            for (int x = 0; x < _matrix.Width; ++x)
            {
                for (int y = 0; y < _matrix.Height; ++y)
                {
                    _bricks[x + y * _matrix.Width].Active = _matrix[x, y];
                }
            }

            if(Figure == null || Figure.Matrix.IsNull)return;
            FigureMatrix figure = Figure.Matrix;
            //Update from figureMatrix
            for (int x = 0; x < figure.Width; ++x)
            {
                for (int y = 0; y < figure.Height; ++y)
                {
                    int c = (figure.x + x) + (figure.y + y) * Width;
                    if(c < 0 || c >= _bricks.Length || !figure[x, y])continue;
                    _bricks[c].Active = true;
                }
            }
        }


    }
}