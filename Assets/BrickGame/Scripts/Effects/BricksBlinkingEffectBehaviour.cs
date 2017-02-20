﻿// <copyright file="BricksBlinkingEffectBehaviour.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/20/2017 13:35</date>

using System.Collections;
using System.Collections.Generic;
using BrickGame.Scripts.Bricks;
using JetBrains.Annotations;
using UnityEngine;

namespace BrickGame.Scripts.Effects
{
    /// <summary>
    /// BricksBlinkingEffectBehaviour
    /// </summary>
    [AddComponentMenu("BrickGame/Effects/Bricks Blinking")]
    public class BricksBlinkingEffectBehaviour : GameBehaviour
    {
        //================================       Public Setup       =================================
        [Tooltip("Count of blincks")]
        public int Count = 5;
        [Tooltip("Total time of blincking in seconds")]
        public float Time = 2F;
        //================================    Systems properties    ==================================
        private int _blincked;
        private float _time;
        //================================      Public methods      =================================
        /// <summary>
        /// Execute blinking of the bricks list
        /// </summary>
        /// <param name="bricks">List of bricks to blink</param>
        /// <returns>total time of blinking</returns>
        public float Execute([NotNull] IEnumerable<Brick> bricks)
        {
            if (!enabled) return 0;
            _blincked = 0;
            StartCoroutine(Blinck(bricks));
            return Time;
        }
        //================================ Private|Protected methods ================================
        private IEnumerator Blinck(IEnumerable<Brick> bricks)
        {
            while (_blincked++ < Count)
            {
                foreach (Brick brick in bricks)
                    brick.Active = !brick.Active;
                yield return new WaitForSeconds(_time);
            }
            yield return null;
        }

        private void Start()
        {
            _time = Time / Count;
        }
    }
}