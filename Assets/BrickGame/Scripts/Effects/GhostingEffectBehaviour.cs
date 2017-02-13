// <copyright file="GhostingEffectBehaviour.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/10/2017 14:22</date>

using UnityEngine;

namespace BrickGame.Scripts.Effects
{
    /// <summary>
    /// GhostingEffectBehaviour
    /// </summary>
    [AddComponentMenu("BrickGame/Effects/GhostingEffect")]
    public class GhostingEffectBehaviour : GameBehaviour
    {
        //================================       Public Setup       =================================
        [Range(0, 0.99f)]
        public float Intensivity = 0.99F;
        //================================    Systems properties    =================================
        // ReSharper disable once InconsistentNaming
        private const string _Intensity = "_Intensity";
        // ReSharper disable once InconsistentNaming
        private const string _BTex = "_BTex";
        private Material _material;
        private RenderTexture _previousFrame;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        [ExecuteInEditMode]
        private void Start()
        {
            _material = new Material(Shader.Find("BrickGames/Effects/GhostyEffectShader"));
            _previousFrame = new RenderTexture(Screen.width, Screen.height, 16);
        }

        [ExecuteInEditMode]
        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            _material.SetFloat(_Intensity, Intensivity);
            _material.SetTexture(_BTex, _previousFrame);
            Graphics.Blit(src, dest, _material);
            Graphics.Blit(RenderTexture.active, _previousFrame);
        }
    }
}