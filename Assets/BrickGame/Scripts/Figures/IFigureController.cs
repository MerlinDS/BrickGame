// <copyright file="IFigureController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 18:57</date>
namespace BrickGame.Scripts.Figures
{
    /// <summary>
    /// IFigureController - interface of the figure controls.
    /// </summary>
    public interface IFigureController
    {
        //================================       Getters|Setters       =================================
        //================================			 Methods		   =================================
        /// <summary>
        /// Remove figure from contoroller
        /// </summary>
        void Remove();
        /// <summary>
        /// Rotation of a figure of matrix to 90 deg.
        /// </summary>
        /// <returns>True if figure matrix was turned, false in other cases</returns>
        bool Turn();
        /// <summary>
        /// Move figure matrix to left
        /// </summary>
        /// <returns></returns>
        bool MoveLeft();
        /// <summary>
        /// Move figure matrix to write
        /// </summary>
        /// <returns></returns>
        bool MoveRight();
        /// <summary>
        /// Move figure to bottom border
        /// </summary>
        /// <returns></returns>
        bool MoveDown();
        /// <summary>
        /// Is figure out of playground bound
        /// </summary>
        bool OutOfBounds { get; }
    }
}