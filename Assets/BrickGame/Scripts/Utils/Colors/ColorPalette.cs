// <copyright file="ColorPalette.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/17/2017 17:47</date>

using UnityEngine;

namespace Assets.BrickGame.Scripts.Utils.Colors
{
    /// <summary>
    /// ColorPalette - color palette that contians basic application colors.
    /// </summary>
    public class ColorPalette : ScriptableObject
    {
        //================================       Public Setup       =================================
        [Tooltip("Color of game backgorund")] public Color Background;

        [Tooltip("Foreground color, and UI color")] public Color Foreground;

        [Tooltip("Main color of the game")] public Color Main;
        //================================    Systems properties    =================================

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        public void UpdateColors(ref Color background, ref Color foreground, ref Color main)
        {
            background = Background;
            foreground = Foreground;
            main = Main;
        }
    }
}