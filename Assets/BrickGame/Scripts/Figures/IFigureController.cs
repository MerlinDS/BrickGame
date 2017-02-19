// <copyright file="IFigureController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 18:57</date>

using System;

namespace BrickGame.Scripts.Figures
{
    /// <summary>
    /// IFigureController - interface of the figureMatrix controls.
    /// </summary>
    [Obsolete("Interface is deprecated.")]
    public interface IFigureController
    {
        //================================       Getters|Setters       =================================
        //================================			 Methods		   =================================
        /// <summary>
        /// Remove figureMatrix from contoroller
        /// </summary>
        void Remove();
        /// <summary>
        /// Rotation of a figureMatrix of matrix to 90 deg.
        /// </summary>
        /// <returns>True if figureMatrix matrix was turned, false in other cases</returns>
        bool Turn();
        /// <summary>
        /// Move figureMatrix matrix to left
        /// </summary>
        /// <returns></returns>
        bool MoveLeft();
        /// <summary>
        /// Move figureMatrix matrix to write
        /// </summary>
        /// <returns></returns>
        bool MoveRight();
        /// <summary>
        /// Move figureMatrix to bottom border
        /// </summary>
        /// <returns></returns>
        bool MoveDown();
        /// <summary>
        /// Is figureMatrix out of playground bound
        /// </summary>
        bool OutOfBounds { get; }
    }
}