// <copyright file="MouseInputAdapter.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/13/2017 14:03</date>

using UnityEngine;

namespace BrickGame.Scripts.Utils.Input
{
    /// <summary>
    /// MouseInputAdapter - Adapting mouse input to ingame touch
    /// </summary>
    public class MouseInputAdapter : AbstractAdapter
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected override bool HasTouch(ref Vector2 position)
        {
            position.x = UnityEngine.Input.mousePosition.x;
            position.y = UnityEngine.Input.mousePosition.y;
            return UnityEngine.Input.GetMouseButton(0);
        }
    }
}