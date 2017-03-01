// <copyright file="SoundButton.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 18:17</date>

using BrickGame.Scripts.Controllers;
using BrickGame.Scripts.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace BrickGame.Scripts.UI
{
    /// <summary>
    /// SoundButton
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class SoundButton : GameControlsButton
    {
        //================================       Public Setup       =================================
        public Sprite On;
        public Sprite Off;
        //================================    Systems properties    =================================
        private Image _image;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        private void Start()
        {
            _image = GetComponent<Image>();
            if (Context.GetActor<AudioController>().Muted)
                _image.sprite = Off;
        }

        /// <inheritdoc />
        protected override void OnClickHandler()
        {
            Debug.Log("Change sound mode");
            BroadcastNofitication(GameNotification.MuteSound);
            _image.sprite = _image.sprite == On ? Off : On;

        }
    }
}