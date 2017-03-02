// <copyright file="ShakeScreenEffect.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>03/02/2017 20:01</date>

using System;
using UnityEngine;

namespace BrickGame.Scripts.Effects
{
    /// <summary>
    /// ShakeScreenEffect
    /// </summary>
    [ExecuteInEditMode]
    public class ShakeScreenEffect : GameBehaviour
    {
        //================================       Public Setup       =================================
        public Vector2 Intensivity;

        private Material _mat;
        //================================    Systems properties    =================================
        private const string ShaderName = "BrickGame/ShakeScreenEffect";
        // ReSharper disable InconsistentNaming
        private const string _Intensivity = "_Intensivity";
        // ReSharper restore InconsistentNaming
        //================================      Public methods      =================================
        public void Execute(float xIntensivity = 0, float yIntensivity = 0)
        {
            Execute(new Vector2(xIntensivity, yIntensivity));
        }
        public void Execute(Vector2 intesivity)
        {
            Intensivity = intesivity;
            enabled = true;
        }
        //================================ Private|Protected methods ================================
        private void OnDestroy()
        {
            if (_mat != null)
            {
                DestroyImmediate(_mat);
            }
        }

        private void OnEnable()
        {
            if (_mat == null)
            {
                _mat = new Material(Shader.Find(ShaderName));
            }
        }

        private void Update()
        {
            if (Math.Abs(Intensivity.x) < 0.0001 && Math.Abs(Intensivity.y) < 0.0001)
            {
                Intensivity = Vector2.zero;
                enabled = false;
                return;
            }
            Intensivity = Vector2.Lerp(Intensivity, Vector2.zero, Time.deltaTime * 3);
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            _mat.SetVector(_Intensivity, Intensivity);
            Graphics.Blit(src, dest, _mat);
        }
    }
}