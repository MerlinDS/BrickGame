// <copyright file="Playground.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 21:16</date>

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
        //================================       Public Setup       =================================
        [Tooltip("Playground matrix width in cells")]
        public int Width = 10;
        [Tooltip("Playground matrix height in cells")]
        public int Height = 20;
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
            _matrix = matrix ?? new PlaygroundMatrix(10, 20);
        }

        //================================ Private|Protected methods ================================
    }
}