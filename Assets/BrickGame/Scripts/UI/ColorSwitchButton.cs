// <copyright file="ColorSwitchButton.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/17/2017 19:14</date>

using BrickGame.Scripts.Controllers;
using BrickGame.Scripts.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace BrickGame.Scripts.UI
{
    /// <summary>
    /// ColorSwitchButton
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class ColorSwitchButton : GameControlsButton
    {
        //================================       Public Setup       =================================
        [Tooltip("Prefix for button label.")]
        public string Prefix = "COLOR: ";
        //================================    Systems properties    =================================
        private Text _label;
        private ColorPaletteManager _manager;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected override void OnClickHandler()
        {
            _manager.NextPalette();
        }

        private void Start()
        {
            _label = GetComponent<Text>();
            Context.AddListener(GameNotification.ColorChanged, Handler);
            _manager = Context.GetActor<ColorPaletteManager>();
            Handler(string.Empty);
        }

        protected override void OnDestroy()
        {
            Context.RemoveListener(GameNotification.ColorChanged, Handler);
            base.OnDestroy();
        }

        private void Handler(string s)
        {
            _label.text = Prefix + _manager.PaletteName;
        }
    }
}