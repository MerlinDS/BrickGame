// <copyright file="FigureDrawer.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 22:51</date>

using BrickGame.Scripts.Figures;
using BrickGame.Scripts.Models;
using JetBrains.Annotations;
using UnityEngine;

namespace BrickGame.Scripts.Bricks
{
    /// <summary>
    /// FigureDrawer
    /// </summary>
    public class FigureDrawer : BricksDrawer, MessageReceiver.IFigureReceiver
    {
        //================================       Public Setup       =================================
        [Tooltip("Width in bricks of the component.")]
        public int Width = 4;
        [Tooltip("Height in bricks of the component.")]
        public int Height = 4;
        //================================    Systems properties    =================================
        [NotNull] private Brick[] _bricks = new Brick[0];
        private FigureBuilder _builder;
        private Vector2 _offset;
        private RectTransform _rectTransform;
        //================================      Public methods      =================================
        public void UpdateSize()
        {
            if(!Validate())return;
            _rectTransform = _content.GetComponent<RectTransform>();
            _offset = _rectTransform.rect.size * 0.5F;
            _offset -= _brickPrefab.Size * 0.5F;
            _offset.x *= -1;
            _bricks = RestoreBricks(Width, Height);
        }


        /// <inheritdoc />
        public void UpdateFigure(FigureMatrix matrix)
        {
            for (int i = 0; i < _bricks.Length; i++)
                _bricks[i].Active = false;
            Matrix<bool> m = _builder.Peek();
            // ReSharper disable PossibleLossOfFraction
            int offsetX = (int) (Width / m.Width * 0.5F);
            int offsetY = (int) (Height / m.Height * 0.5F);
            // ReSharper restore PossibleLossOfFraction

            for (int x = 0; x < m.Width; x++)
            {
                for (int y = 0; y < m.Height; y++)
                {
                    _bricks[offsetX + x + (offsetY + y) * Width].Active = m[x,y];
                }
            }
        }

        protected override void SetBrickPosition(Transform brick, Vector3 offset)
        {
            RectTransform rect = brick.GetComponent<RectTransform>();
            Vector2 position = new Vector2(offset.x, offset.y);
            rect.anchoredPosition = _offset + position;
        }

        //================================ Private|Protected methods ================================
        private void Awake()
        {
            _builder = GetComponent<FigureBuilder>();
            UpdateSize();
        }
    }
}