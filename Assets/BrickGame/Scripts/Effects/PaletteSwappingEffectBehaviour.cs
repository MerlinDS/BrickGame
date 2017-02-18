// <copyright file="PaletteSwappingEffectBehaviour.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/16/2017 15:07</date>

using UnityEngine;

namespace BrickGame.Scripts.Effects
{
    /// <summary>
    /// PaletteSwappingEffectBehaviour
    /// </summary>
    [ExecuteInEditMode]
    public class PaletteSwappingEffectBehaviour: GameBehaviour
    {
        //================================       Public Setup       =================================

        public Color Color0 {
            get { return _c0; }
            set
            {
                if(_c0.Equals(value))return;
                _matrixUp2Data = false;
                _c0 = value;
            }
        }

        public Color Color1 {
            get { return _c1; }
            set
            {
                if(_c1.Equals(value))return;
                _matrixUp2Data = false;
                _c1 = value;
            }
        }

        public Color Color2 {
            get { return _c2; }
            set
            {
                if(_c2.Equals(value))return;
                _matrixUp2Data = false;
                _c2 = value;
            }
        }

        [Header("Blackout setup")]
        [Range(0, 1)]
        public float Intensivity = 1F;
        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        const string ShaderName = "Hidden/PaleteSwapping";
        //Shader fields
        // ReSharper disable once InconsistentNaming
        const string _Intensivity = "_Intensivity";
        // ReSharper disable once InconsistentNaming
        const string _ColorMatrix = "_ColorMatrix";

        private bool _matrixUp2Data;

        [SerializeField]
        private Color _c0;
        [SerializeField]
        private Color _c1;
        [SerializeField]
        private Color _c2;

        private Matrix4x4 _matrix;
        private Material _mat;
        //================================ Private|Protected methods ================================
        private void OnEnable()
        {
            if (_mat == null)
            {
                Shader shader = Shader.Find(ShaderName);
                _mat = new Material(shader);
            }
            _matrixUp2Data = false;
        }

        private void OnDisable()
        {
            if (_mat != null)
                DestroyImmediate(_mat);
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            if (!_matrixUp2Data)
            {
                //Refresh color matrix if matrix is not up to date
                _matrix = new Matrix4x4();
                _matrix.SetRow(0, new Vector4(_c0.r, _c0.g, _c0.b, _c0.a));
                _matrix.SetRow(1, new Vector4(_c1.r, _c1.g, _c1.b, _c1.a));
                _matrix.SetRow(2, new Vector4(_c2.r, _c2.g, _c2.b, _c2.a));
                _matrix.SetRow(3, new Vector4(_c2.r, _c2.g, _c2.b, _c2.a));
                _matrixUp2Data = true;
            }
            //Set values to shader
            _mat.SetFloat(_Intensivity, Intensivity);
            _mat.SetMatrix(_ColorMatrix, _matrix);
            //Render effect
            Graphics.Blit(src, dst, _mat);
        }
    }
}