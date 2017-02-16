// <copyright file="PalleteSwappingEffectBehaviour.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/16/2017 15:07</date>

using UnityEngine;

namespace BrickGame.Scripts.Effects
{
    /// <summary>
    /// PalleteSwappingEffectBehaviour
    /// </summary>
    [ExecuteInEditMode]
    public class PalleteSwappingEffectBehaviour: GameBehaviour
    {
        //================================       Public Setup       =================================
        public Color Color0;
        public Color Color1;
        public Color Color2;

        [Header("Blackout setup")]
        [Range(0, 1)]
        public float Intensivity = 1F;
        //================================    Systems properties    =================================
        private Matrix4x4 ColorMatrix
        {
            get
            {
                Matrix4x4 mat = new Matrix4x4();
                mat.SetRow(0, ColorToVec(Color0));
                mat.SetRow(1, ColorToVec(Color1));
                mat.SetRow(2, ColorToVec(Color2));
                mat.SetRow(3, ColorToVec(Color2));
                return mat;
            }
        }

        private Material _mat;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        private void OnEnable()
        {
            Shader shader = Shader.Find("Hidden/PalleteSwappingShader");
            if (_mat == null)
                _mat = new Material(shader);
        }

        private void OnDisable()
        {
            if (_mat != null)
                DestroyImmediate(_mat);
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            _mat.SetMatrix("_ColorMatrix", ColorMatrix);
            _mat.SetFloat("_Intensivity", Intensivity);
            Graphics.Blit(src, dst, _mat);
        }



        private Vector4 ColorToVec(Color color)
        {
            return new Vector4(color.r, color.g, color.b, color.a);
        }
    }
}