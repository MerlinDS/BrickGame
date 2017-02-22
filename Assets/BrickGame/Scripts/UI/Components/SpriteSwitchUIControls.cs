// <copyright file="SpriteSwitchUIControls.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/21/2017 18:01</date>

using BrickGame.Scripts.Controllers;
using JetBrains.Annotations;
using UnityEngine.UI;

namespace BrickGame.Scripts.UI.Components
{
    /// <summary>
    /// SpriteSwitchUIControls
    /// </summary>
    public class SpriteSwitchUIControls : GameBehaviour
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private GameModeManager _manager;

        //================================      Public methods      =================================
        [UsedImplicitly]
        public void OnClick(int index = 0)
        {
            _manager.ChangeMode(index);
            UpdateButtons();
        }
        //================================ Private|Protected methods ================================
        private void Start()
        {
            _manager = Context.GetActor<GameModeManager>();
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            int n = transform.childCount;
            for (int i = 0; i < n; i++)
            {
                var child = transform.GetChild(i);
                Button button = child.GetComponent<Button>();
                if(button == null)continue;
                button.interactable = _manager.Index != i;
            }
        }

    }
}