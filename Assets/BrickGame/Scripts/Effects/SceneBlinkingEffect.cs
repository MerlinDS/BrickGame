// <copyright file="SceneBlinkingEffect.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/20/2017 15:11</date>

using System.Collections;
using System.Linq;
using UnityEngine;

namespace BrickGame.Scripts.Effects
{
    /// <summary>
    /// SceneBlinkingEffect
    /// </summary>
    [AddComponentMenu("BrickGame/Effects/Scene Blinking")]
    public class SceneBlinkingEffect : GameBehaviour
    {
        //================================       Public Setup       =================================
        [Tooltip("Count of blincks")]
        public int Count = 5;
        [Tooltip("Total time of blincking in seconds")]
        public float Time = 2F;
        //================================    Systems properties    =================================
        private int _blincked;
        private float _time;

        private PaletteSwappingEffectBehaviour _effect;

        private Color _c0;
        private Color _c1;
        //================================      Public methods      =================================
        public float Execute()
        {
            if (!enabled || _effect == null) return 0;
            _blincked = 0;
            _c0 = _effect.Color0;
            _c1 = _effect.Color1;
            _time = Time / Count;
            StartCoroutine(Blinck());
            return Time;
        }

        private IEnumerator Blinck()
        {
            while (_blincked++ < Count)
            {
                Color temp = _effect.Color0;
                _effect.Color0 = _effect.Color1;
                _effect.Color1 = temp;
                yield return new WaitForSeconds(_time);
            }
            _effect.Color0 = _c0;
            _effect.Color1 = _c1;
            yield return null;
        }

        //================================ Private|Protected methods ================================
        private void Start()
        {
            Camera cam = Camera.allCameras.FirstOrDefault(c => c.GetComponent<PaletteSwappingEffectBehaviour>() != null);
            if (cam == null)
            {
                Debug.LogWarning("Has no cameras for this effect");
                enabled = false;
                return;
            }
            _effect = cam.GetComponent<PaletteSwappingEffectBehaviour>();
        }
    }
}