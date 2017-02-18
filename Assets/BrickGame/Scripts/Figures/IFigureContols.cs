﻿// <copyright file="IFigureContols.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/18/2017 21:05</date>
namespace BrickGame.Scripts.Figures
{
    /// <summary>
    /// IFigureContols - Provide the public interface for controls of the matrix of a figure.
    /// </summary>
    public interface IFigureContols
    {
        //================================       Getters|Setters       =================================
        //================================			 Methods		   =================================
        /// <summary>
        /// Rotate a matrix of the figure to 90 degrees.
        /// If figure can't be rotated, rotation will not proceed (without errors or warnings).
        /// <param name="clockwise">If true clockwise rotation will happen,
        /// in other cases counterclockwise rotation will happen.</param>
        /// </summary>
        void Rotate(bool clockwise = true);
        //================================			 Movement		   =================================
        /// <summary>
        /// Chech if figure can moved horizontal in the playground matrix
        /// </summary>
        /// <param name="xShift">Shift by x coordinate</param>
        /// <returns>True if can, false if can't</returns>
        bool CanMoveHorizontal(int xShift);
        /// <summary>
        /// Move figure horizontal in the playground matrix
        /// </summary>
        /// <param name="xShift">Shift by x coordinate</param>
        void MoveHorizontal(int xShift);
        /// <summary>
        /// Chech if figure can moved vertical in the playground matrix
        /// </summary>
        /// <param name="yShift">Shift by y coordinate</param>
        /// <returns>True if can, false if can't</returns>
        bool CanMoveVertical(int yShift);
        /// <summary>
        /// Move figure vertical in the playground matrix
        /// </summary>
        /// <param name="yShift">Shift by y coordinate</param>
        void MoveVertical(int yShift);
    }
}