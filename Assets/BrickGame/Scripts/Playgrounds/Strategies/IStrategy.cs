// <copyright file="IStrategy.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/24/2017 20:25</date>
namespace BrickGame.Scripts.Playgrounds.Strategies
{
    /// <summary>
    /// IStrategy
    /// </summary>
    public interface IStrategy
    {
        //================================       Getters|Setters       =================================
        bool OnPasue { get; }
        //================================			 Methods		   =================================
        void Pause();
        void Resume();
    }
}