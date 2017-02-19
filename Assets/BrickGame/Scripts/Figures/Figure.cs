// <copyright file="Figure.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 19:41</date>

using BrickGame.Scripts.Models;
using JetBrains.Annotations;

namespace BrickGame.Scripts.Figures
{
    /// <summary>
    /// Figure
    /// </summary>
    public abstract class Figure : GameBehaviour, MessageReceiver.IFigureReceiver
    {
        //================================       Public Setup       =================================
        [NotNull]
        public FigureMatrix Matrix { get { return _matrix; } }
        //================================    Systems properties    =================================
        private FigureMatrix _matrix = new FigureMatrix();
        //================================      Public methods      =================================
        /// <inheritdoc />
        public void UpdateFigure(FigureMatrix matrix)
        {
            _matrix = matrix ?? new FigureMatrix();
        }

        //================================ Private|Protected methods ================================
    }
}