// <copyright file="SoundButton.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 18:17</date>

using BrickGame.Scripts.UI.Components;
using UnityEngine;

namespace BrickGame.Scripts.UI
{
    /// <summary>
    /// SoundButton
    /// </summary>
    public class SoundButton : GameControlsButton
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected override void OnClickHandler()
        {
            Debug.Log("Change sound mode");
            BroadcastNofitication(GameNotification.MuteSound);
            //TODO: Change icon
        }
    }
}