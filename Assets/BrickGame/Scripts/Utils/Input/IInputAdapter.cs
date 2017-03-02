// <copyright file="IInputAdapter.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/13/2017 13:46</date>

using UnityEngine;

namespace BrickGame.Scripts.Utils.Input
{
    /// <summary>
    /// Gestures that could be occures during a touch
    /// </summary>
    public enum InputGesture
    {
        /// <summary>
        /// Gesture was not detected
        /// </summary>
        None,
        /// <summary>
        /// Tap or click gesture was detected.
        /// Small delay between touch began and touch ended.
        /// Small delta betwen current position of the touch and previous position.
        /// </summary>
        Tap,
        /// <summary>
        /// Swipe down gesture was detected.
        /// Small delay between touch began and touch ended.
        /// Big delta betwee current position of the touch and previous position.
        /// VerticalDirection of gesture is down
        /// </summary>
        Swipe
    }
    /// <summary>
    /// IInputAdapter - Adapting user inputs for ingame systems usage.
    /// Provide safe and muliti-platform input for the game.
    /// </summary>
    public interface IInputAdapter
    {
        //================================       Getters|Setters       =================================

        //================================			 Methods		   =================================
        /// <summary>
        /// Detect user touch
        /// </summary>
        /// <param name="touch">Output instance of a user touch if it was detected.
        /// If touch was not detected, instance of empty touch will be returned.</param>
        /// <returns>True if user's touch was detected, false in other cases</returns>
        bool Detect(out Touch touch);

        /// <summary>
        /// Detect user touch
        /// </summary>
        /// <param name="touch">Output instance of a user touch if it was detected.
        /// If touch was not detected, instance of empty touch will be returned.</param>
        /// <param name="gesture">Flag of a gesture that was detected during a touch</param>
        /// <returns>True if user's touch was detected, false in other cases</returns>
        bool Detect(out Touch touch, out InputGesture gesture);

        /// <summary>
        /// Reset input form current touch.
        /// </summary>
        void Reset();


    }
}