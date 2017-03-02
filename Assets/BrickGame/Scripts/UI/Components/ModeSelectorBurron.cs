// <copyright file="ModeSelectorBurron.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>03/02/2017 17:56</date>

using UnityEngine;
using UnityEngine.UI;

namespace BrickGame.Scripts.UI.Components
{
    /// <summary>
    /// ModeSelectorBurron
    /// </summary>
    public class ModeSelectorBurron : Button
    {
        //================================       Public Setup       ================================

        //================================    Systems properties    =================================
        private Image[] _images;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);
            if (_images == null)
                _images = GetComponentsInChildren<Image>();
            foreach (Image img in _images)
            {
                img.color = state == SelectionState.Disabled ? Color.gray : Color.white;
                if (img.CompareTag(SRTags.Pointer))
                    img.enabled = state == SelectionState.Disabled;
            }
        }
    }
}