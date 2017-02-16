// <copyright file="ColoredBrick.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 13:42</date>

using UnityEngine;

namespace BrickGame.Scripts.Bricks
{
    /// <summary>
    /// ColoredBrick - class for a brick with changeable color.
    /// </summary>
    /// <typeparam name="T">Type of the brick renderer</typeparam>
    public abstract class ColoredBrick<T> : Brick where T : Component
    {
        //================================       Public Setup       =================================
        [Header("Colour setup")]
        [Tooltip("Color of active brick")]
        public Color ActiveColor = Color.black;
        [Tooltip("Color of passive brick")]
        public Color PassiveColor = Color.gray;
        /// <inheritdoc />
        public sealed override bool Active
        {
            get { return _active; }
            set
            {
                if (_active == value) return;
                UpdateColor( value ? ActiveColor : PassiveColor );
                _active = value;
            }
        }

        //================================    Systems properties    =================================
        /// <summary>
        /// Get instance of the brick renderer
        /// </summary>
        protected T Renderer
        {
            get
            {
                if (_renderer == null)
                    _renderer = GetComponent<T>();
                return _renderer;
            }
        }
        private bool _active;
        private T _renderer;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <summary>
        /// Initialize brick component.
        /// </summary>
        private void Awake()
        {
            UpdateColor( PassiveColor );
        }

        /// <summary>
        /// Update color of the brick
        /// </summary>
        /// <param name="color">New color</param>
        protected abstract void UpdateColor(Color color);
    }
}