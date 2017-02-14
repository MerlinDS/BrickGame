// <copyright file="UIBrick.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/09/2017 22:20</date>

using BrickGame.Scripts.Bricks;
using UnityEngine;
using UnityEngine.UI;

namespace BrickGame.Scripts.UI
{
    /// <summary>
    /// UIBrick - component for drawing brick in UI
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class UIBrick : ColoredBrick<Image>
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <inheritdoc />
        public override Vector2 Size
        {
            get { return GetComponent<RectTransform>().rect.size; }
        }

        /// <inheritdoc />
        protected override void UpdateColor(Color color)
        {
            Renderer.color = color;
        }

        //================================ Private|Protected methods ================================
    }
}