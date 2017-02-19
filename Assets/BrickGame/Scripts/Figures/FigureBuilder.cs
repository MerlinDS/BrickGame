// <copyright file="FigureBuilder.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 16:25</date>

using System.Collections.Generic;
using BrickGame.Scripts.Models;
using UnityEngine;

namespace BrickGame.Scripts.Figures
{
    /// <summary>
    /// FigureBuilder - builder of game figures (specific matrices).
    /// This class build figures from the list of glyphs, depend on rules of creation.
    /// Also, this class provide access to the next figure matrix.
    /// </summary>
    public class FigureBuilder : GameBehaviour
    {
        /// <summary>
        /// Repeat chance reducer
        /// </summary>
        private const float Reducer = 0.5F;
        //================================       Public Setup       =================================
        [Tooltip("Min count of figures in the stack.")]
        public int Count = 3;
        [Tooltip("Creation seed. Affects on chances of figures creation.")]
        public int Seed = 5;
        [Tooltip("Available glyphs for current builder")]
        public Glyph[] Glyphs;
        //================================    Systems properties    =================================
        /// <summary>
        /// An index of previously created figure.
        /// </summary>
        private int _previousIndex;
        /// <summary>
        /// The chance to create a same figure as previous.
        /// </summary>
        private float _repeatChance;
        /// <summary>
        /// List of creation chances
        /// </summary>
        private int[] _chances;
        /// <summary>
        /// Stack of created figures.
        /// </summary>
        private readonly Stack<Figure> _figures = new Stack<Figure>();

        /// <summary>
        /// Random index of a glyph
        /// </summary>
        private int RandomIndex
        {
            get
            {
                int index = 0;
                int chance = Random.Range(0, 99);
                while (chance > _chances[index]) index++;
                return index;
            }
        }
        //================================      Public methods      =================================
        /// <summary>
        /// Removing the first figure from builder and return it.
        /// </summary>
        /// <returns>Instance of the figure matrix</returns>
        public Figure Pop()
        {
            FillStack();
            return _figures.Pop();
        }
        /// <summary>
        /// Get copy of the first figure matrix in the builder stack.
        /// </summary>
        /// <returns>Copy of the first figure matrix in the builder stack</returns>
        public Matrix<bool> Peek()
        {
            FillStack();
            return Figure.Clone(_figures.Peek(), true, true);
        }
        //================================ Private|Protected methods ================================
        /// <summary>
        /// Initialize component
        /// </summary>
        private void Awake()
        {
            _previousIndex = -1;
            _chances = CalculateChances(Glyphs.Length, Seed);
        }

        /// <summary>
        /// Fill stack with new figures while the stack length less than minimum count of figures.
        /// </summary>
        private void FillStack()
        {
            while (_figures.Count < Count)
            {
                int index = RandomIndex;
                float chance = Random.value;
                if (index == _previousIndex)
                {
                    if (chance < _repeatChance)
                        _repeatChance *= Reducer;
                    else
                    {
                        if (--index < 0) index = Glyphs.Length - 1;
                        _repeatChance = Reducer;
                    }
                }
                _previousIndex = index;
                Figure figure = new Figure(Glyphs[index]);
                if(chance > 0.5F)figure.Flip();
                if(Random.value > 0.5)figure.Rotate();
                _figures.Push(figure);
            }
        }

        /// <summary>
        /// Calculate list of chances
        /// </summary>
        /// <param name="length">count of cells</param>
        /// <param name="seed">seed</param>
        /// <returns>list of chances</returns>
        private int[] CalculateChances(int length, int seed)
        {
            if (seed < 1)
            {
                Debug.LogError("Seed could not be less that 1!");
                return new int[0];
            }
            if (length < 1)
            {
                Debug.LogWarning("Length is less that 1!");
                return new int[0];
            }

            int[] chances = new int[length];
            float @base = ((100 / length) * 2 - seed * (length - 1)) * 0.5F;
            for (int i = 0; i < length; i++)
            {
                chances[i] = (int) (@base + seed * i);
                if (i > 0) chances[i] += chances[i - 1];
            }
            return chances;
        }
    }
}