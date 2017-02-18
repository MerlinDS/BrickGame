﻿// <copyright file="FigureMatrix.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/18/2017 19:23</date>

using JetBrains.Annotations;

namespace BrickGame.Scripts.Models
{
    /// <summary>
    /// FigureMatrix
    /// </summary>
    public class FigureMatrix : Matrix<bool>
{
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <inheritdoc />
        public FigureMatrix(int width, int height) : base(width, height, true)
        {
        }

        /// <inheritdoc />
        public FigureMatrix([NotNull] bool[] matrix, int width, int height) : base(matrix, width, height, true, true)
        {
        }

        //================================ Private|Protected methods ================================
}
}