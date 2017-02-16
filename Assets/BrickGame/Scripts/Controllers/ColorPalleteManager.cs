// <copyright file="ColorPalleteManager.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/16/2017 18:06</date>

using BrickGame.Scripts.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace BrickGame.Scripts.Controllers
{
    /// <summary>
    /// ColorPalleteManager
    /// </summary>
    [ExecuteInEditMode]
    public class ColorPalleteManager : GameManager
    {
        //================================       Public Setup       =================================
        [Tooltip("Color of game backgorund")] public Color Background;

        [Tooltip("Foreground color, and UI color")] public Color Foreground;

        [Tooltip("Main color of the game")] public Color Main;

        //================================    Systems properties    =================================
        private Color _foreground;
        private Color _main;
        //================================      Public methods      =================================
        public void UpdateColors(bool force = false)
        {
            UpdateCameras();
            if (!_foreground.Equals(Foreground) || force)
                UpdateUIColor(_foreground = Foreground);
            if(!_main.Equals(Main) || force)
                UpdateShadows(_main = Main);
        }

        public void Reset()
        {
            Background = new Color(0.611F, 0.698F, 0.65F);
            Foreground = new Color(0.17F, 0.18F, 0.17F);
            Main = new Color(0.46F, 0.5F, 0.48F);
            _main = _foreground = Color.black;
            UpdateColors();
        }

        //================================ Private|Protected methods ================================
        private void UpdateCameras()
        {
            foreach (Camera c in Camera.allCameras)
            {
                PalleteSwappingEffectBehaviour effectBehaviour = c.GetComponent
                    <PalleteSwappingEffectBehaviour>();
                if (effectBehaviour == null)
                {
                    //Change directly on camera
                    c.backgroundColor = Background;
                    continue;
                }
                effectBehaviour.Color0 = Foreground;
                effectBehaviour.Color2 = Background;
                effectBehaviour.Color1 = Main;
            }
        }

        private void UpdateUIColor(Color color)
        {
            var textFields = FindObjectsOfType<Text>();
            foreach (Text textField in textFields)
                textField.color = color;
        }

        private void UpdateShadows(Color color)
        {
            var shadows = FindObjectsOfType<Shadow>();
            foreach (Shadow shadow in shadows)
                shadow.effectColor = color;
        }
    }
}