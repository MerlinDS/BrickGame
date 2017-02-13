// <copyright file="GameModesController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/11/2017 14:26</date>

using System.Linq;
using BrickGame.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BrickGame.Scripts.Controllers
{
    /// <summary>
    /// GameModesController
    /// </summary>
    public class GameModesController : GameManager
    {
        //================================       Public Setup       ================================
        [ShowOnly]
        public string CurrentScene;

        [Tooltip("Cooldwon before game auto starting")]
        public float StartCooldown = 0.3F;
        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        // ReSharper disable once ParameterHidesMember
        public void StartMode(string name)
        {
            TypeSafe.Scene scene = SRScenes.All.FirstOrDefault(s=>s.name == name);
            if (scene == null)
            {
                Debug.LogError("Unknow mode: " + name);
                return;
            }

            Debug.LogFormat("Starting {0} mode", name);
            SceneManager.sceneLoaded += OnSceneLoaded;
            scene.LoadAsync();
        }

        /// <summary>
        /// Exit to main menu from game mode
        /// </summary>
        public void Exit2Menu()
        {
            SRScenes.MainMenuScene.Load();
        }
        //================================ Private|Protected methods ================================

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            TypeSafe.Scene link = SRScenes.All.FirstOrDefault(s => s.name == scene.name);
            if (link == null) return;
            Debug.LogFormat("Scene {0} loaded", scene.name);
            CurrentScene = scene.name;
            if (link == SRScenes.MainMenuScene) return;
            //Auto start of the game
            Invoke("SendStartNotification", StartCooldown);
        }

        private void SendStartNotification()
        {
            Context.Notify(GameNotification.Start);
        }

    }
}