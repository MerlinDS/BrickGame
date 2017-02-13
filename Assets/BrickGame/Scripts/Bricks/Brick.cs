// <copyright file="Brick.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 14:05</date>

using BrickGame.Scripts.Utils;
using UnityEngine;

namespace BrickGame.Scripts.Bricks
{
    /// <summary>
    /// Brick - interface of the brick
    /// </summary>
    public abstract class Brick : GameBehaviour
    {
        //================================       Getters|Setters       =================================
        [ShowOnly]
        [Tooltip("X coordinate in Playground matrix. If X == -1, bricks is out of playground.")]
        public int X = -1;

        [ShowOnly]
        [Tooltip("Y coordinate in Playground matrix. If Y == -1, bricks is out of playground.")]
        public int Y = -1;

        /// <summary>
        /// Size of the brick
        /// </summary>
        public abstract Vector2 Size { get; }
        /// <summary>
        /// Flag of brick activity
        /// </summary>
        public abstract bool Active { get; set; }
        //================================			 Methods		   =================================
    }
}