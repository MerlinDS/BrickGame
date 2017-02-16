// <copyright file="FPSManager.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 18:22</date>

using MiniMoca;
using UnityEngine;
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace BrickGame.Scripts.Utils
{
    public interface IFPSCounter : IMocaActor
    {
        /// <summary>
        /// Get current FPS
        /// </summary>
        int FPS { get; }
    }
    /// <summary>
    /// FPSManager - Simple fps counter
    /// </summary>
    public class FPSManager : GameManager, IFPSCounter
    {
        //================================       Public Setup       =================================
        /// <inheritdoc />
        public int FPS
        {
            get { return _currentFps; }
        }

        //================================    Systems properties    =================================
        const float FPSMeasurePeriod = 0.5f;
        private int _fpsAccumulator;
        private float _fpsNextPeriod;
        private int _currentFps;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        private void Start()
        {
            _fpsNextPeriod = Time.realtimeSinceStartup + FPSMeasurePeriod;
        }

        private void Update()
        {
            // measure average frames per second
            _fpsAccumulator++;
            if (Time.realtimeSinceStartup > _fpsNextPeriod)
            {
                _currentFps = (int) (_fpsAccumulator/FPSMeasurePeriod);
                _fpsAccumulator = 0;
                _fpsNextPeriod += FPSMeasurePeriod;
            }
}
    }
}