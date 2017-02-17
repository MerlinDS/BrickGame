// <copyright file="ColorSwitchButton.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/17/2017 19:14</date>

using BrickGame.Scripts.Controllers;
using BrickGame.Scripts.UI.Components;

namespace BrickGame.Scripts.UI
{
    /// <summary>
    /// ColorSwitchButton
    /// </summary>
    public class ColorSwitchButton : GameControlsButton
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected override void OnClickHandler()
        {
            Context.GetActor<ColorPalleteManager>().NextPalette();
        }
    }
}