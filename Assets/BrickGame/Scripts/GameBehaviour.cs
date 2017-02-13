// <copyright file="GameBehaviour.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 13:15</date>

using System;
using Lean;
using MiniMoca.Helpers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BrickGame.Scripts
{
    /// <summary>
    /// GameBehaviour - basic game components class.
    /// Use this class insted of MonoBehaviour!
    /// </summary>
    public abstract class GameBehaviour : MocaBehaviour<GameContext>
    {
        /// <summary>
        ///   <para>Clones the object original and returns the clone.</para>
        /// </summary>
        /// <param name="original">An existing object that you want to make a copy of.</param>
        /// <returns>
        ///   <para>The instantiated clone.</para>
        /// </returns>
        protected GameObject Instantiate(GameObject original)
        {
            CheckNullArgument(original, "The Object you want to instantiate is null.");
            return !Application.isPlaying ? GameObject.Instantiate(original) : LeanPool.Spawn(original);
        }

        /// <summary>
        ///   <para>Removes a gameobject, component or asset.</para>
        /// </summary>
        /// <param name="obj">The object to destroy.</param>
        public new void Destroy(Object obj)
        {
            if (!Application.isPlaying)
                DestroyImmediate(obj);
            else
            {
                if(obj is GameObject)LeanPool.Despawn((GameObject)obj);
                else Object.Destroy(obj);
            }
        }

        private static void CheckNullArgument(object arg, string message)
        {
            if (arg == null)
                throw new ArgumentException(message);
        }
    }
}