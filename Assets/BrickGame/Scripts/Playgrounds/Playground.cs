// <copyright file="Playground.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 21:16</date>

using System;
using BrickGame.Scripts.Controllers;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Playgrounds.Strategies;
using JetBrains.Annotations;
using UnityEngine;

namespace BrickGame.Scripts.Playgrounds
{
    /// <summary>
    /// Playground - main playground class, provides access to public API of playground.
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("BrickGame/Game Components/Playground")]
    [RequireComponent(typeof(PlaygroundController))]
    public class Playground : GameBehaviour, MessageReceiver.IPlaygroundReceiver
    {
        private const string WithoutRullesPostfix = "without_rulles";
        //================================       Public Setup       =================================
        [Tooltip("Game rules for current playground")]
        public GameRules Rules;

        /// <summary>
        /// Name of the game session on current playground
        /// </summary>
        [NotNull]
        public string SessionName
        {
            get
            {
                if (Rules == null)
                    return gameObject.name + WithoutRullesPostfix;
                return gameObject.name + Rules.name;
            }
        }

        /// <summary>
        /// Total lines removed
        /// </summary>
        public int TotalLines { get; set; }

        /// <summary>
        /// Access to playground matrix
        /// </summary>
        [NotNull]public Matrix<bool> Matrix{get{return _matrix;}}
        //================================    Systems properties    =================================
        [NotNull]private Matrix<bool> _matrix = new PlaygroundMatrix(10, 20);
        //================================      Public methods      =================================
        /// <inheritdoc />
        public void UpdateMatix(Matrix<bool> matrix)
        {
            int width = 10, height = 20;
            if (Rules != null)
            {
                width = Rules.Width;
                height = Rules.Height;
            }
            _matrix = matrix ?? new PlaygroundMatrix(width, height);
        }

        /// <summary>
        /// Update score of the session
        /// </summary>
        /// <param name="count">Count of removed lines</param>
        [UsedImplicitly]
        public void UpdateScore(int count)
        {
            if (count > 0)
            {
                TotalLines += count;
                BroadcastNofitication(GameNotification.ScoreUpdated,
                    new ScoreDataProvider( SessionName, count));
            }//Update figure score
            BroadcastMessage(MessageReceiver.AccelerateFigure,
                Rules.GetSpeedByLines(TotalLines));
        }

        [UsedImplicitly]
        public void FinishSession()
        {
            _matrix = new PlaygroundMatrix(0, 0);
            BroadcastNofitication(GameState.End);
        }
        //================================ Private|Protected methods ================================

        private void Start()
        {
            Rules = Context.GetActor<GameModeManager>().CurrentRules;
            foreach (StrategySetup setup in Rules.Strategies)
                AddStrategy(setup);
            //Try to restore game data from the cache
            BroadcastNofitication(GameState.Restore,
                new SessionDataProvider(Rules.name, SessionName));
            //Start new game
            BroadcastNofitication(GameState.Start,
                new SessionDataProvider(Rules.name, SessionName));
        }

        private void AddStrategy(StrategySetup setup)
        {
            Debug.LogFormat("Add strategy {0} to {1}", setup.Mode, SessionName );
            Type type;
            switch (setup.Mode)
            {
                case StrategySetup.SMode.Bubbles:
                    type = typeof(BubbleStrategy);
                    break;
                case StrategySetup.SMode.WatterFall:
                    type = typeof(WatterFallStrategy);
                    break;
                case StrategySetup.SMode.Slider:
                    type = typeof(SliderStrategy);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            IStrategy strategy = (IStrategy) gameObject.AddComponent(type);
            strategy.Initialize(setup);
        }
    }
}