// <copyright file="BackToMenuButton.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/13/2017 11:52</date>

using BrickGame.Scripts.Controllers;
using BrickGame.Scripts.UI.Components;

namespace BrickGame.Scripts.UI
{
    /// <summary>
    /// BackToMenuButton
    /// </summary>
    public class BackToMenuButton : GameControlsButton
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected override void OnClickHandler()
        {
            Context.GetActor<GameModesController>().Exit2Menu();
        }
    }
}