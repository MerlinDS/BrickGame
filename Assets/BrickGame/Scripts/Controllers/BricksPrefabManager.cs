// <copyright file="BricksPrefabManager.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/21/2017 17:45</date>

using BrickGame.Scripts.Bricks;
using UnityEngine;

namespace BrickGame.Scripts.Controllers
{
    /// <summary>
    /// BricksPrefabManager
    /// </summary>
    public class BricksPrefabManager : GameManager
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================

        public void ChangeSprite(int index)
        {
            if (index < 0)
            {
                Debug.LogError("Index coudn't be less than 0");
                return;
            }
            var bricksPools = GetComponentsInChildren<IBricksSpriteChanger>();
            foreach (IBricksSpriteChanger pool in bricksPools)
                pool.ChangeSprite(index);
        }
    }
}