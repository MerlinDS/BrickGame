// <copyright file="ExitButton.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 18:15</date>

using BrickGame.Scripts.UI.Components;
using UnityEngine;

namespace BrickGame.Scripts.UI
{
    /// <summary>
    /// ExitButton
    /// </summary>
    public class ExitButton : GameControlsButton
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected override void OnClickHandler()
        {
            //TODO: Save user progress
            Application.Quit();
        }
    }
}