﻿// <copyright file="AbstractFigureController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 20:00</date>

using System.Collections.Generic;
using BrickGame.Scripts.Model;
using UnityEngine;

namespace BrickGame.Scripts.Figures
{
    /// <summary>
    /// AbstractFigureController
    /// </summary>
    public abstract class AbstractFigureController : GameBehaviour, IFigureController
    {
        //================================       Public Setup       =================================
        [Tooltip("Cooldown of figure moving down")]
        public float Cooldown = 1;
        [Tooltip("List of linked figure glyphs")]
        public Glyph[] Glyphs;
        [Tooltip("Step of chances")]
        public int Step = 5;
        //================================    Systems properties    =================================
        private int _previousIndex;
        private float _repeatChance;
        protected bool[,] Figure;
        private int[] _chances;
        private Stack<bool[,]> _figures;
        //================================      Public methods      =================================
        /// <inheritdoc />
        public abstract bool OutOfBounds { get; }

        /// <inheritdoc />
        public abstract bool Turn();

        /// <inheritdoc />
        public abstract bool MoveLeft();

        /// <inheritdoc />
        public abstract bool MoveRight();

        /// <inheritdoc />
        public abstract bool MoveDown();

        /// <inheritdoc />
        public abstract void Remove();

        //================================ Private|Protected methods ================================
        private void Awake()
        {
            _previousIndex = -1;
            _repeatChance = 0.5f;
            _figures = new Stack<bool[,]>();
            ChancesRowCalculation();
        }

        protected void PopFigure()
        {
            if(_figures.Count == 0)GenerateFigures();
            Figure = _figures.Pop();
        }

        private void ChancesRowCalculation()
        {
            int n = Glyphs.Length;
            _chances = new int[n];
            float @base = ( (100 / n) * 2 - Step * (n - 1) ) * 0.5F;
            for (int i = 0; i < n; i++)
            {
                _chances[i] = (int)(@base + Step * i);
                if(i > 0)_chances[i] += _chances[i - 1];
            }
        }

        private int RandomIndex()
        {
            int index = 0;
            int chance = Random.Range(0, 99);
            while (chance > _chances[index])index++;
            return index;
        }

        private void GenerateFigures()
        {
            if (_figures.Count < 3)
            {
                int index = RandomIndex();
                if(index == _previousIndex)
                {
                    if (Random.value < _repeatChance)
                        _repeatChance *= 0.5F;
                    else
                    {
                        if (++index >= Glyphs.Length) index = 0;
                        _repeatChance = 0.5F;
                    }
                }
                _previousIndex = index;
                //Create figure
                Glyph glyph = Glyphs[index];
                _figures.Push(Random.value > 0.5F ? glyph.GetFigureMatrix() : glyph.GetFlippedFigureMatrix());
            }
        }

        public bool[,] NextFigure()
        {
            if(_figures.Count == 0)GenerateFigures();
            return _figures.Peek();
        }

    }
}