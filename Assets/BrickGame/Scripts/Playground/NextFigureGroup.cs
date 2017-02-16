// <copyright file="NextFigureGroup.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/16/2017 19:32</date>

using BrickGame.Scripts.Bricks;
using UnityEngine;

namespace Assets.BrickGame.Scripts.Playground
{
    /// <summary>
    /// NextFigureGroup
    /// </summary>
    [ExecuteInEditMode]
    public class NextFigureGroup : BricksDrawer
    {
        //================================       Public Setup       =================================
        public int Width = 4;

        public int Height = 4;
        //================================    Systems properties    =================================
        private Brick[] _bricks;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        public void Start()
        {
            if(Width <= 0 || Height <= 0)return;
            if(!Validate())return;
            _bricks = DrawBricks(Width, Height, transform.localScale);
        }

        private void Update()
        {

        }
    }
}