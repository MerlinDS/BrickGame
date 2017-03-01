// <copyright file="PlayAudioComponent.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>03/01/2017 10:04</date>

using BrickGame.Scripts.Models;
using UnityEngine;

namespace BrickGame.Scripts.Utils
{
    /// <summary>
    /// PlayAudioComponent
    /// </summary>
    public class PlayAudioComponent : GameBehaviour
    {
        //================================       Public Setup       =================================
        public enum ESound
        {
            Click,
            Move
        }

        [Tooltip("Sound to play")]
        public ESound Sound = ESound.Click;
        //================================    Systems properties    =================================
        //================================      Public methods      =================================
        public void Play()
        {
            Context.Notify(Sound == ESound.Click ? AudioNotification.Click : AudioNotification.Move);
        }
        //================================ Private|Protected methods ================================
    }
}