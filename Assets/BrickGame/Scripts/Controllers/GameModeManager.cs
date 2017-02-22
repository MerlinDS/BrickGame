// <copyright file="GameModeManager.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/22/2017 18:37</date>

using BrickGame.Scripts.Bricks;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Utils;
using UnityEngine;

namespace BrickGame.Scripts.Controllers
{
    /// <summary>
    /// GameModeManager
    /// </summary>
    public class GameModeManager : GameManager
    {
        //================================       Public Setup       =================================

        public int Index
        {
            get { return _index; }
        }

        //================================    Systems properties    =================================
        [SerializeField] [ShowOnly] private int _index;

        private CacheModel _cacheModel;

        //================================      Public methods      =================================
        public void ChangeMode(int index)
        {
            if (index < 0)
            {
                Debug.LogError("Index coudn't be less than 0");
                return;
            }
            var bricksPools = GetComponentsInChildren<IBricksSpriteChanger>();
            foreach (IBricksSpriteChanger pool in bricksPools)
                pool.ChangeSprite(index);
            //Sace index
            _cacheModel.ModeIndex = index;
            _index = index;
        }

        //================================ Private|Protected methods ================================
        private void Start()
        {
            _cacheModel = Context.GetActor<CacheModel>();
            ChangeMode(_cacheModel.ModeIndex);
        }
    }
}