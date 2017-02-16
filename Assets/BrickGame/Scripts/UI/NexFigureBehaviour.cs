// <copyright file="NexFigureBehaviour.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/09/2017 22:15</date>

using BrickGame.Scripts.Bricks;
using BrickGame.Scripts.Figures;
using UnityEngine;

namespace BrickGame.Scripts.UI
{
    /// <summary>
    /// NexFigureBehaviour - Component for drawing next figure of the game
    /// </summary>
    public class NexFigureBehaviour : BricksDrawer
    {
        //================================       Public Setup       =================================
        [Tooltip("Factory of figures that provide next figure to show")]
        public FigureFactory Factory;
        [Tooltip("Width in brick, count of columns")]
        public int Width = 4;
        [Tooltip("Height in bricks, count of rows")]
        public int Height = 4;

        //================================    Systems properties    =================================
        private Vector2 _offset;
        [HideInInspector][SerializeField]private Brick[] _bricks;
        private RectTransform _rectTransform;
        //================================      Public methods      =================================

        public void Rebuild()
        {
            if(!Validate())return;
            //offset
            _rectTransform = GetComponent<RectTransform>();
            _offset = _rectTransform.rect.size * 0.5F;
            _offset -= _brickPrefab.Size * 0.5F;
            _offset.x *= -1;
            _bricks = RestoreBricks(Width, Height);
        }
        //================================ Private|Protected methods ================================
        /// <summary>
        /// Initialize component and add context listeners
        /// </summary>
        private void Start()
        {
            if(!Validate())return;
            Rebuild();
            if(!Application.isPlaying)return;
            Context.AddListener(FigureNotification.Changed, GameNotificationHandler);
        }

        /// <summary>
        /// Remove listeners
        /// </summary>
        private void OnDestroy()
        {
            Context.RemoveListener(FigureNotification.Changed, GameNotificationHandler);
        }

        /// <inheritdoc />
        protected override void SetBrickPosition(Transform brick, Vector3 offset)
        {
            RectTransform rect = brick.GetComponent<RectTransform>();
            Vector2 position = new Vector2(offset.x, offset.y);
            rect.anchoredPosition = _offset + position;
        }

        /// <summary>
        /// Game notifaication handler
        /// </summary>
        /// <param name="notification">Name of the notification</param>
        private void GameNotificationHandler(string notification)
        {
            for (int i = 0; i < _bricks.Length; i++)
                _bricks[i].Active = false;
            bool[,] figure = Factory.NextFigure();
            int w = figure.GetLength(0);
            int h = figure.GetLength(1);

            int offsetX =  Width / w - 1;
            int offsetY =  1;

            //Set new
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    _bricks[offsetX + x + (offsetY + y) * Width].Active = figure[x,y];
                }
            }
        }
    }
}