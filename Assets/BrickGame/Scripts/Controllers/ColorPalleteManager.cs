// <copyright file="ColorPalleteManager.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/16/2017 18:06</date>

using Assets.BrickGame.Scripts.Utils.Colors;
using BrickGame.Scripts.Effects;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace BrickGame.Scripts.Controllers
{
    /// <summary>
    /// ColorPalleteManager
    /// </summary>
    public class ColorPalleteManager : GameManager
    {
        //================================       Public Setup       =================================
        [Header("Current color palette")]
        [Tooltip("Color of game backgorund")] public Color Background;

        [Tooltip("Foreground color, and UI color")] public Color Foreground;

        [Tooltip("Main color of the game")] public Color Main;

        [Header("Color setups:")]
        [Tooltip("Index of current palette")] [SerializeField][HideInInspector]
        private int _index;
        [Tooltip("List of availabel palettes")][SerializeField]
        private ColorPalette[] _palettes;
        //================================    Systems properties    =================================
        private Color _foreground;
        private Color _main;

        public int ColorPaletteIndex { get { return _index; } }

        //================================      Public methods      =================================
        /// <summary>
        /// UpdateColors colors of components in game
        /// </summary>
        /// <param name="force"></param>
        public void UpdateColors(bool force = false)
        {
            UpdateCameras();
            if (!_foreground.Equals(Foreground) || force)
            {
                UpdateTextColor(_foreground = Foreground);
                UpdateImages(_foreground);
            }
            if(!_main.Equals(Main) || force)
                UpdateShadows(_main = Main);
            UpdateUIBricks();
        }

        /// <summary>
        /// Reset manager to default palette
        /// </summary>
        public void Reset()
        {
            Background = new Color(0.611F, 0.698F, 0.65F);
            Foreground = new Color(0.17F, 0.18F, 0.17F);
            Main = new Color(0.46F, 0.5F, 0.48F);
            _main = _foreground = Color.black;
            UpdateColors();
        }

        public void NextPalette()
        {
            ChangePalette(_index+1);
        }

        public void ChangePalette(int index)
        {
            if(index == _index)return;
            //cycling index in palette array
            if (index >= _palettes.Length) index = 0;
            else if (index < 0) index = _palettes.Length - 1;
            //UpdateColors colors from palette
            _palettes[index].UpdateColors(ref Background, ref Foreground, ref Main);
            UpdateColors(true);
            _index = index;
            BroadcastNofitication(GameNotification.ColorChanged);

        }
        //================================ Private|Protected methods ================================
        private void Start()
        {
            ChangePalette(Context.GetActor<CacheModel>().ColorPaletteIndex);
        }

        private void UpdateCameras()
        {
            foreach (Camera c in Camera.allCameras)
            {
                PaletteSwappingEffectBehaviour effectBehaviour = c.GetComponent
                    <PaletteSwappingEffectBehaviour>();
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

        private void UpdateTextColor(Color color)
        {
            var textFields = FindObjectsOfType<Text>();
            foreach (Text textField in textFields)
                textField.color = color;
        }

        private void UpdateImages(Color color)
        {
            var changable = GameObject.FindGameObjectsWithTag(SRTags.ColorChangeble);
            foreach (GameObject obj in changable)
            {
                if(obj.GetComponent<Image>() == null)continue;
                obj.GetComponent<Image>().color = color;
            }
        }

        private void UpdateShadows(Color color)
        {
            var shadows = FindObjectsOfType<Shadow>();
            foreach (Shadow shadow in shadows)
            {
                if(shadow.tag == SRTags.FixedColor)continue;
                shadow.effectColor = color;
            }
        }

        private void UpdateUIBricks()
        {
            UIBrick[] bricks = FindObjectsOfType<UIBrick>();
            foreach (UIBrick brick in bricks)
            {
                brick.ActiveColor = Foreground;
                brick.PassiveColor = Main;
                brick.Refresh();
            }
        }

    }
}