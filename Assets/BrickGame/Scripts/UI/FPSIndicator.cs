// <copyright file="FPSIndicator.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 18:31</date>
// ReSharper disable InconsistentNaming

using BrickGame.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace BrickGame.Scripts.UI
{
    /// <summary>
    /// FPSIndicator
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class FPSIndicator : GameBehaviour
    {
        private const string Display = "{0} FPS";
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private int _pastFrame;
        private Text _testField;
        private IFPSCounter _fpsCounter;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        private void Start()
        {
            _testField = GetComponent<Text>();
            _fpsCounter = Context.GetActor<FPSManager>();
        }

        private void LateUpdate()
        {
            if (_pastFrame == _fpsCounter.FPS) return;
            _testField.text = string.Format(Display, _fpsCounter.FPS);
            _pastFrame = _fpsCounter.FPS;
        }
    }
}