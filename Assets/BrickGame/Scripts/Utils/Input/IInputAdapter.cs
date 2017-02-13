// <copyright file="IInputAdapter.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/13/2017 13:46</date>

using MiniMoca;
using UnityEngine;

namespace BrickGame.Scripts.Utils.Input
{
    public enum InputGesture
    {
        None,
        Tap,
        Swipe
    }
    /// <summary>
    /// IInputAdapter
    /// </summary>
    public interface IInputAdapter : IMocaActor
    {
        //================================       Getters|Setters       =================================

        //================================			 Methods		   =================================
        bool Detect(out Touch touch);
        bool Detect(out Touch touch, out InputGesture gesture);
        void Reset();
    }
}