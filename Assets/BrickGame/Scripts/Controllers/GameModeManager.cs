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

        public GameRules CurrentRules { get; private set; }

        public int PreviousIndex { get; set; }

        //================================    Systems properties    =================================
        [SerializeField] [ShowOnly] private int _index;

        [Tooltip("List of game rules")] [SerializeField] private GameRules[] _ruleses;

        private CacheModel _cacheModel;

        //================================      Public methods      =================================
        public void ChangeMode(int index)
        {
            if (_ruleses == null)
            {
                Debug.LogError("Ruleses list is empty ");
                return;
            }
            if (index < 0 && index >= _ruleses.Length)
            {
                Debug.LogError("Index coudn't be less than 0 and bigger then ruleses count.");
                return;
            }
            PreviousIndex = _index;
            _index = index;
            _cacheModel.ModeIndex = index;
            CurrentRules = _ruleses[index];
            var bricksPools = GetComponentsInChildren<IBricksSpriteChanger>();
            foreach (IBricksSpriteChanger pool in bricksPools)
                pool.ChangeSprite(pool.Image ? CurrentRules.BricksImage : CurrentRules.BricksSprite);
            BroadcastNofitication(GameNotification.ModeChanged);
        }

        //================================ Private|Protected methods ================================
        private void Awake()
        {
            _cacheModel = Context.GetActor<CacheModel>();
            ChangeMode(_cacheModel.ModeIndex);
        }
    }
}