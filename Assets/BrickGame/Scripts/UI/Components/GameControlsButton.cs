// <copyright file="GameControlsButton.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/13/2017 11:50</date>

using UnityEngine;
using UnityEngine.UI;

namespace BrickGame.Scripts.UI.Components
{
    /// <summary>
    /// GameControlsButton
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class GameControlsButton : GameBehaviour
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        protected abstract void OnClickHandler();

        //================================ Private|Protected methods ================================
        protected virtual void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClickHandler);
        }

        /// <inheritdoc />
        protected virtual void OnDestroy()
        {
            GetComponent<Button>().onClick.RemoveListener(OnClickHandler);
        }
    }
}