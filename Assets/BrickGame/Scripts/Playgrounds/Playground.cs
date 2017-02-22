// <copyright file="Playground.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 21:16</date>

using System;
using BrickGame.Scripts.Models;
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
        [CanBeNull]
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
            if (Rules == null)
                Debug.LogWarning("Playground has no rulles. Game will be proceed without rules!");
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
            if(count == 0 || Rules == null)return;
            TotalLines += count;
            BroadcastNofitication(GameNotification.ScoreUpdated,
                new ScoreDataProvider( SessionName, count));
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
            if(Rules == null)return;
            BroadcastNofitication(GameState.Restore,
                new SessionDataProvider(Rules.name, SessionName));
        }
    }
}