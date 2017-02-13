// <copyright file="IFigureController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 18:57</date>
namespace BrickGame.Scripts.Figures
{
    /// <summary>
    /// IFigureController
    /// </summary>
    public interface IFigureController
    {
        //================================       Getters|Setters       =================================
        //================================			 Methods		   =================================
        void Remove();
        bool Turn();
        bool MoveLeft();
        bool MoveRight();
        bool MoveDown();
        bool OutOfBounds { get; }
    }
}