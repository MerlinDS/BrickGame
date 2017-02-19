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
    /// Playground
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("BrickGame/Game Components/Playground")]
    public class Playground : GameBehaviour
    {
        //================================       Public Setup       =================================
        [NotNull]public Matrix<bool> Matrix{get{return _matrix;}}
        //================================    Systems properties    =================================
        private PlaygroundMatrix _matrix = new PlaygroundMatrix(10, 20);
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
    }
}