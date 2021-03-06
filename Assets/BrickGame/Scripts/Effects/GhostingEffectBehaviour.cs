﻿// <copyright file="GhostingEffectBehaviour.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/10/2017 14:22</date>

using UnityEngine;

namespace BrickGame.Scripts.Effects
{
    /// <summary>
    /// GhostingEffectBehaviour - component for GhostingEffect
    /// </summary>
//    [ExecuteInEditMode]
    [AddComponentMenu("BrickGame/Effects/Ghosting")]
    public class GhostingEffectBehaviour : GameBehaviour
    {
        //================================       Public Setup       =================================
        [Range(0, 0.99f)]
        public float Intensivity = 0.99F;
        //================================    Systems properties    =================================
        // ReSharper disable once InconsistentNaming
        private const string _Intensivity = "_Intensivity";
        // ReSharper disable once InconsistentNaming
        private const string _BTex = "_BTex";
        private Material _material;
        //static - hack for smooth transition between scenes
        private static RenderTexture _previousFrame;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <summary>
        /// Initialize component
        /// </summary>
        private void OnEnable()
        {
            _material = new Material(Shader.Find("Hidden/GhostingEffectShader"));
            if(_previousFrame == null)
                _previousFrame = new RenderTexture(Screen.width, Screen.height, 0);
        }

        /// <summary>
        /// Render image effect
        /// </summary>
        /// <param name="src">Source texture</param>
        /// <param name="dest">Resulting texture</param>
        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            _material.SetFloat(_Intensivity, Intensivity);
            _material.SetTexture(_BTex, _previousFrame);
            Graphics.Blit(src, dest, _material);
            Graphics.Blit(RenderTexture.active, _previousFrame);
        }
    }
}