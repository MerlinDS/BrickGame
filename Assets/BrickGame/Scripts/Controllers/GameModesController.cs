// <copyright file="GameModesController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/11/2017 14:26</date>

using System.Linq;
using BrickGame.Scripts.Bricks;
using BrickGame.Scripts.Utils;
using Lean;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = TypeSafe.Scene;

namespace BrickGame.Scripts.Controllers
{
    /// <summary>
    /// GameModesController - controlling game modes.
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

        /// <summary>
        /// Rebuild game mode: load a scene with a required game mode and execute starting of this mode.
        /// </summary>
        /// <param name="name">Name of the required scene</param>
        /// ReSharper disable once ParameterHidesMember
        public void StartMode(string name)
        {
            Scene scene = SRScenes.All.FirstOrDefault(s=>s.name == name);
            if (scene == null)
            {
                Debug.LogError("Unknow mode: " + name);
                return;
            }
            ChangeCurrentSceneTo(scene);
        }

        /// <summary>
        /// Exit to main menu from game mode.
        /// </summary>
        public void Exit2Menu()
        {
            //despwan bricks (to pool)
            var drawers = FindObjectsOfType<BricksDrawer>();
            foreach (var drawer in drawers)drawer.DestroyBricks();
            //provokes cache updating
            BroadcastNofitication(GameState.Pause);
            //Load menu
            ChangeCurrentSceneTo(SRScenes.MainMenuScene);
        }
        //================================ Private|Protected methods ================================
        /// <summary>
        /// Cahnge current scene to other one.
        /// If scenes are equals, will not chages
        /// </summary>
        /// <param name="scene">TypeSafe scene</param>
        private void ChangeCurrentSceneTo(Scene scene)
        {
            if (SceneManager.GetActiveScene().name == scene.name)
            {
                Debug.LogWarning("Scene are equals and not to be changed!");
                return;
            }
            Debug.LogFormat("Change scene {0} to {1}", SceneManager.GetActiveScene().name,  scene.name);
            Context.GetActor<AudioController>().StopSfx();
            SceneManager.sceneLoaded += OnSceneLoaded;
            scene.LoadAsync();
        }

        /// <summary>
        /// Handler for loading of a scene
        /// </summary>
        /// <param name="scene">Unity scene that was loaded</param>
        /// <param name="loadSceneMode">Loading mode</param>
        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Scene link = SRScenes.All.FirstOrDefault(s => s.name == scene.name);
            if (link == null) return;
            Debug.LogFormat("Scene {0} loaded", scene.name);
            CurrentScene = scene.name;
            Context.GetActor<UserControlsManager>().RefreshControllers();
            Context.GetActor<ColorPalleteManager>().UpdateColors(true);
        }

    }
}