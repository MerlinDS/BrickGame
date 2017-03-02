// <copyright file="ModeControls.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/21/2017 18:01</date>

using BrickGame.Scripts.Controllers;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace BrickGame.Scripts.UI.Components
{
    /// <summary>
    /// ModeControls
    /// </summary>
    public class ModeControls : GameBehaviour
    {
        //================================       Public Setup       =================================
        public Text Label;

        public Transform Selector;
        //================================    Systems properties    =================================
        private GameModeManager _manager;

        //================================      Public methods      =================================
        [UsedImplicitly]
        public void OnClick(int index = 0)
        {
            _manager.ChangeMode(index);
            UpdateView();
        }
        //================================ Private|Protected methods ================================
        private void Start()
        {
            _manager = Context.GetActor<GameModeManager>();
            UpdateView();
        }

        private void UpdateView()
        {
            int n = Selector.childCount;
            for (int i = 0; i < n; i++)
            {
                var child = Selector.GetChild(i);
                Button button = child.GetComponent<Button>();
                if(button == null)continue;
                button.interactable = _manager.Index != i;
            }
            Label.text = _manager.CurrentRules.Name;
        }

    }
}