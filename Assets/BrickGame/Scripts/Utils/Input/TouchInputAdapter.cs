// <copyright file="TouchInputAdapter.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/13/2017 17:55</date>

using UnityEngine;

namespace BrickGame.Scripts.Utils.Input
{
    /// <summary>
    /// TouchInputAdapter
    /// </summary>
    public class TouchInputAdapter : AbstractAdapter
    {

        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected override bool HasTouch(ref Vector2 position)
        {
            position.y = position.x = 0;
            if (UnityEngine.Input.touchCount == 0) return false;
            Touch touch = UnityEngine.Input.GetTouch(0);
            position.x = touch.position.x;
            position.y = touch.position.y;
            return true;
        }
    }
}