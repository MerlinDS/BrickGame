// <copyright file="PaletteWizard.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/17/2017 17:50</date>

using Assets.BrickGame.Scripts.Utils.Colors;
using UnityEditor;
using UnityEngine;

namespace BrickGame.Editor
{
    /// <summary>
    /// PaletteWizard
    /// </summary>
    public class PaletteWizard : AssetWizard
    {
        //================================       Public Setup       =================================
        [Tooltip("Color of game backgorund")] public Color Background = new Color(0.611F, 0.698F, 0.65F);

        [Tooltip("Foreground color, and UI color")] public Color Foreground = new Color(0.17F, 0.18F, 0.17F);

        [Tooltip("Main color of the game")] public Color Main = new Color(0.46F, 0.5F, 0.48F);
        //================================    Systems properties    =================================

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        [MenuItem("Assets/Create/BrickGame/Create Color Palette")]
        static void CreateWizard()
        {
            DisplayWizard<PaletteWizard>("Create Color Palette", "Create");
        }

        void OnWizardCreate()
        {
            ColorPalette palette = CreateInstance<ColorPalette>();
            palette.Background = Background;
            palette.Foreground = Foreground;
            palette.Main = Main;
            CreateAsset(palette);
        }

        void OnWizardUpdate()
        {
            helpString = "Please set colors to palette!";
        }

    }
}