// <copyright file="GlyphWizard.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 17:15</date>

using BrickGame.Scripts.Figures;
using UnityEditor;
using UnityEngine;

namespace BrickGame.Editor
{
    /// <summary>
    /// GlyphWizard
    /// </summary>
    public class GlyphWizard : AssetWizard
{
        private const int Width = 4;
        private const int Height = 2;

        private bool[,] _view;

        [MenuItem("Assets/Create/BrickGame/Create Glyph")]
        static void CreateWizard()
        {
            DisplayWizard<GlyphWizard>("Create Glyph", "Create");
        }

        void OnWizardCreate()
        {
            Glyph glyph = CreateInstance<Glyph>();
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                    glyph[x, y] = _view[x, y];
            }
            CreateAsset(glyph);
        }

        void OnWizardUpdate()
        {
            helpString = "Please set name of a glyph and it's view matrix!";
        }

        /// <inheritdoc />
        protected override bool DrawWizardGUI()
        {
            if (_view == null)
                _view = new bool[Width,Height];
            GUILayout.BeginVertical();
            for (int y = 0; y < Height; ++y)
            {
                GUILayout.BeginHorizontal();
                for (int x = 0; x < Width; ++x)
                    _view[x, y] = GUILayout.Toggle(_view[x, y], "");
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
            return base.DrawWizardGUI();
        }
    }
}