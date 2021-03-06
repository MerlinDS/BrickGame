﻿// <copyright file="ColorPaletteManager.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/16/2017 18:06</date>

using Assets.BrickGame.Scripts.Utils.Colors;
using BrickGame.Scripts.Effects;
using BrickGame.Scripts.Models;
using UnityEngine;

namespace BrickGame.Scripts.Controllers
{
    /// <summary>
    /// ColorPaletteManager
    /// </summary>
    public class ColorPaletteManager : GameManager
    {
        //================================       Public Setup       =================================
        /// <summary>
        /// Current palette name
        /// </summary>
        public string PaletteName
        {
            get
            {
                return _index >= _palettes.Length ? "Default" : _palettes[_index].name;
            }
        }
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

        public int ColorPaletteIndex { get { return _index; } }

        //================================      Public methods      =================================
        /// <summary>
        /// UpdateColors colors of components in game
        /// </summary>
        /// <param name="force"></param>
        public void UpdateColors(bool force = false)
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
                effectBehaviour.Force = force;
            }
        }

        /// <summary>
        /// Reset manager to default palette
        /// </summary>
        public void Reset()
        {
            Background = new Color(0.611F, 0.698F, 0.65F, 1F);
            Foreground = new Color(0.17F, 0.18F, 0.17F, 1F);
            Main = new Color(0.46F, 0.5F, 0.48F, 1F);
            UpdateColors();
        }

        public void NextPalette()
        {
            ChangePalette(_index+1);
        }

        public void ChangePalette(int index, bool force = false)
        {
            if(index == _index)return;
            //cycling index in palette array
            if (index >= _palettes.Length) index = 0;
            else if (index < 0) index = _palettes.Length - 1;
            //UpdateColors colors from palette
            _palettes[index].UpdateColors(ref Background, ref Foreground, ref Main);
            UpdateColors(force);
            _index = index;
            BroadcastNofitication(GameNotification.ColorChanged);
        }
        //================================ Private|Protected methods ================================
        private void Start()
        {
            ChangePalette(Context.GetActor<CacheModel>().ColorPaletteIndex, true);
        }
    }
}