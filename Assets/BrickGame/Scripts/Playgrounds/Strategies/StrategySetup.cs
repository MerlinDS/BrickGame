// <copyright file="StrategySetup.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/24/2017 20:37</date>

using System;
using UnityEngine;

namespace BrickGame.Scripts.Playgrounds.Strategies
{
    /// <summary>
    /// StrategySetup - setup for game mode strategy
    /// </summary>
    [Serializable]
    public struct StrategySetup
    {
        /// <summary>
        /// Implemented strategies.
        /// </summary>
        public enum SMode
        {
            /// <summary>
            /// A strategy for bubble mode behaviour.
            /// <seealso cref="BubbleStrategy"/>
            /// </summary>
            Bubbles = 1,
            /// <summary>
            /// A strategy for watter fall mode behaviour.
            /// <seealso cref="WatterFallStrategy"/>
            /// </summary>
            Slider,
            /// <summary>
            /// A strategy for slider mode behaviour.
            /// <seealso cref="SliderStrategy"/>
            /// </summary>
            WatterFall
        }

        public SMode Mode;
        [Tooltip("A speed of a strategy update")]
        public float Speed;
        [Tooltip("Increaser for the speed of a strategy update")]
        public float SpeedIncrease;
    }
}