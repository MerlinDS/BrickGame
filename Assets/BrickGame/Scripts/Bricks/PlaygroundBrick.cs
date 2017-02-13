// <copyright file="PlaygroundBrick.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 13:57</date>

using UnityEngine;

namespace BrickGame.Scripts.Bricks
{
    /// <summary>
    /// PlaygroundBrick - class for brick in playground
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlaygroundBrick : ColoredBrick<SpriteRenderer>
    {
        //================================       Public Setup       =================================
        /// <inheritdoc />
        public override Vector2 Size
        {
            get { return Renderer.bounds.size; }
        }
        //================================    Systems properties    =================================

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected override void UpdateColor(Color color)
        {
            Renderer.color = color;
        }
    }
}