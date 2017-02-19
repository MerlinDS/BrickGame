﻿// <copyright file="PlaygroundBehaviour.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 15:21</date>

using System;
using BrickGame.Scripts.Bricks;
using UnityEngine;

namespace BrickGame.Scripts.Playground
{
    /// <summary>
    /// PlaygroundBehaviour - concrete playground drawer
    /// </summary>
    [Obsolete("Depricated class.")]
    public class PlaygroundBehaviour : BricksDrawer, IModelMessageResiver
    {
        //================================       Public Setup       =================================
        public int BlinkingCount;
        public float BlinkingCooldown;
        //================================    Systems properties    =================================
        private bool _blinking;
        private int _blinks;
        private float _timer;
        private int[] _blinkingLines;
        private Brick[] _bricks;
        private PlaygroundModel _model;
        //================================      Public methods      =================================
        public void UpdateModel(PlaygroundModel model)
        {
            if(!Validate())return;
            _model = model;
            _bricks = RestoreBricks(model.Width, model.Height, transform.localScale);
            enabled = true;
        }
        //================================ Private|Protected methods ================================
        /// <summary>
        /// UpdateColors activity of bricks depending on data in matrix of playground.
        /// </summary>
        private void Update()
        {
            if (_model == null)
            {
                enabled = false;
                return;
            }
            Brick brick;
            int x, y, w, h;

            if (!_blinking)
            {
                if (_model.ViewUp2date) return;
                w = _model.Width * _model.Height;
                //UpdateColors active bricks
                for (x = 0; x < w; ++x)
                {
                    brick = _bricks[x];
                    brick.Active = _model[x];
                }
                _model.ViewUp2date = true;
                return;
            }

            //Blinking
            if ((_timer += Time.deltaTime) < BlinkingCooldown) return;
            _timer = 0;

            //Remove blinking
            if (_blinks++ >= BlinkingCount)
            {
                _blinking = false;
                EventHandler handler = EndOfBlinking;
                if(handler != null)handler(this, EventArgs.Empty);
                return;
            }
            w = _model.Width;
            h = _blinkingLines.Length;
            bool blinked = _blinks % 2 == 0;
            for (int line = 0; line < h; line++)
            {
                y = _blinkingLines[line];
                for (x = 0; x < w; x++)
                {
                    brick = _bricks[x + y * w];
                    brick.Active = blinked;
                }
            }
        }



        /// <summary>
        /// Execute blinking of lines
        /// </summary>
        /// <param name="lines"></param>
        public void Blink(int[] lines)
        {
            _blinking = true;
            _blinkingLines = lines;
            _blinks = 0;
            _timer = 0;
        }

        public event EventHandler EndOfBlinking;
    }
}
