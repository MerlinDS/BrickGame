// <copyright file="GameControlsButton.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/13/2017 11:50</date>

using BrickGame.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;

namespace BrickGame.Scripts.UI.Components
{
    /// <summary>
    /// GameControlsButton - abstract class for a game buttons
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class GameControlsButton : GameBehaviour
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <summary>
        /// Will be executed on button click.
        /// Execute concrete action of the component.
        /// </summary>
        protected abstract void OnClickHandler();

        //================================ Private|Protected methods ================================
        /// <summary>
        /// Add onClick listener fo the component
        /// </summary>
        protected virtual void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        /// <summary>
        /// Remove onClick listener from the component
        /// </summary>
        protected virtual void OnDestroy()
        {
            GetComponent<Button>().onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            Context.Notify(AudioNotification.Click);
            OnClickHandler();
        }
    }
}