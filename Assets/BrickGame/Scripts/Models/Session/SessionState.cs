// <copyright file="SessionState.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>03/03/2017 13:13</date>

using System;

namespace BrickGame.Scripts.Models.Session
{
    /// <summary>
    /// SessionState - game session state
    /// </summary>
    [Flags]
    public enum SessionState
    {
        /// <summary>
        /// Session has not state.
        /// </summary>
        None = 0x1,
        /// <summary>
        /// Session was started
        /// </summary>
        Started = 0x2,
        /// <summary>
        /// Session was ended
        /// </summary>
        Ended = 0x4,
        /// <summary>
        /// Session holds on pause
        /// </summary>
        OnPause = 0x8
    }
}