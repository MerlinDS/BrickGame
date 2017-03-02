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

        public bool Force;
        [Header("Fish eye setup")]
        [Range(-1, 1)]
        public float Intensivity = 1F;
        [Header("Speed of colors mixing")]
        [Range(0, 4)]
        public float MixingSpeed = 1F;
        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        const string ShaderName = "BrickGame/PaletteSwapping";
        //Shader fields
        // ReSharper disable InconsistentNaming
        const string _Intensivity = "_Intensivity";
        const string _PaletteTex = "_PaletteTex";
        const string _Palette2Tex = "_Palette2Tex";
        const string _Mixing = "_Mixing";
        const string _Color = "_Color";
        // ReSharper restore InconsistentNaming

        private bool _texUp2Data;

        [SerializeField]
        private Color _c0;
        [SerializeField]
        private Color _c1;
        [SerializeField]
        private Color _c2;

        private int _firstTexutre;
        private int _secondTexutre;
        private float _mixing;
        private Texture2D[] _textures;
        private Material _mat;
        //================================ Private|Protected methods ================================
        private void OnEnable()
        {
            if (_mat == null)
            {
                Shader shader = Shader.Find(ShaderName);
                _mat = new Material(shader);
                //Create texture for palette drawing
                _textures = new[]
                {
                    SetPixels(CreateTexture(3, 1), ref _c0, ref _c1, ref _c2),
                    SetPixels(CreateTexture(3, 1), ref _c0, ref _c1, ref _c2)
                };
                _firstTexutre = 0;
                _secondTexutre = 1;
                _mixing = 1;
                _texUp2Data = true;
            }
            _texUp2Data = false;
        }

        private void OnDisable()
        {
            if (_mat != null)
            {
                foreach (Texture2D texture in _textures)
                    DestroyImmediate(texture);
                DestroyImmediate(_mat);
            }
        }

        private void Update()
        {
            if (_texUp2Data)
            {
                if(_mixing >= 1.0)return;
                _mixing = Mathf.MoveTowards(_mixing, 1, Time.deltaTime * MixingSpeed);
                return;
            }
            //Refresh color palette befor sending to shader
            if (!Force)
            {
                //swap textures
                var temp = _firstTexutre;
                _firstTexutre = _secondTexutre;
                _secondTexutre = temp;
                _mixing = 0;
            }
            else
                _mixing = 1;
            //update textures
            SetPixels(_textures[_secondTexutre], ref _c0, ref _c1, ref _c2);
            _texUp2Data = true;
            Force = false;
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            //Set values to shader
            _mat.SetFloat(_Mixing, _mixing);
            _mat.SetFloat(_Intensivity, Intensivity);
            _mat.SetColor(_Color, _c0);
            //Render effect
            _mat.SetTexture(_PaletteTex, _textures[_firstTexutre]);
            _mat.SetTexture(_Palette2Tex, _textures[_secondTexutre]);
            Graphics.Blit(src, dst, _mat);
        }

        private Texture2D SetPixels(Texture2D texture, ref Color c0, ref Color c1, ref Color c2)
        {
            texture.SetPixel(0, 0, c0);
            texture.SetPixel(1, 0, c1);
            texture.SetPixel(2, 0, c2);
            texture.Apply(false);
            return texture;
        }

        private Texture2D CreateTexture(int w, int h)
        {
            var texture = new Texture2D(w, h, TextureFormat.RGB24, false)
            {
                wrapMode = TextureWrapMode.Clamp,
                filterMode = FilterMode.Point,
                anisoLevel = 0
            };
            return texture;
        }
    }
}