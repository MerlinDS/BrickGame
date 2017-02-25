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
    [AddComponentMenu("BrickGame/Effects/Palette Swapping")]
    public class PaletteSwappingEffectBehaviour: GameBehaviour
    {
        //================================       Public Setup       =================================
        public Color Color0 {
            get { return _c0; }
            set
            {
                if(_c0.Equals(value))return;
                _texUp2Data = false;
                _c0 = value;
            }
        }

        public Color Color1 {
            get { return _c1; }
            set
            {
                if(_c1.Equals(value))return;
                _texUp2Data = false;
                _c1 = value;
            }
        }

        public Color Color2 {
            get { return _c2; }
            set
            {
                if(_c2.Equals(value))return;
                _texUp2Data = false;
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
        // ReSharper disable InconsistentNaming
        const string _Intensivity = "_Intensivity";
        const string _PaletteTex = "_PaletteTex";
        // ReSharper restore InconsistentNaming

        private bool _texUp2Data;

        [SerializeField]
        private Color _c0;
        [SerializeField]
        private Color _c1;
        [SerializeField]
        private Color _c2;

        private Texture2D _texture;
        private Material _mat;
        //================================ Private|Protected methods ================================
        private void OnEnable()
        {
            if (_mat == null)
            {
                //Create texture for palette drawing
                _texture = new Texture2D(3, 1, TextureFormat.RGB24, false)
                {
                    wrapMode = TextureWrapMode.Clamp,
                    filterMode = FilterMode.Point,
                    anisoLevel = 0
                };
                Shader shader = Shader.Find(ShaderName);
                _mat = new Material(shader);
            }
            _texUp2Data = false;
        }

        private void OnDisable()
        {
            if (_mat != null)
            {
                DestroyImmediate(_texture);
                DestroyImmediate(_mat);
            }
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            if (!_texUp2Data)
            {
                //Refresh color palette befor sending to shader
                _texture.SetPixel(0, 0, _c0);
                _texture.SetPixel(1, 0, _c1);
                _texture.SetPixel(2, 0, _c2);
                _texture.Apply(false);
                _texUp2Data = true;
            }
            //Set values to shader
            _mat.SetFloat(_Intensivity, Intensivity);
            //Render effect
            _mat.SetTexture(_PaletteTex, _texture);
            Graphics.Blit(src, dst, _mat);

        }
    }
}