// <copyright file="Figure.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/18/2017 19:23</date>

using BrickGame.Scripts.Figures;
using JetBrains.Annotations;

namespace BrickGame.Scripts.Models
{
    /// <summary>
    /// Figure - class that represent figures in the game as the rectangular matrix.
    /// </summary>
    public class Figure : Matrix<bool>
    {
        //================================       Public Setup       =================================
        // ReSharper disable InconsistentNaming
        public int x;
        public int y;
        // ReSharper restore InconsistentNaming
        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <summary>
        /// Constructor for epmty figure
        /// </summary>
        public Figure()
        {

        }
        /// <summary>
        /// Create new figure from the template
        /// </summary>
        /// <param name="glyph">tamplate of a figure</param>
        public Figure([NotNull] Glyph glyph):base(glyph.GetFigureMatrix(), true)
        {

        }
        //================================ Private|Protected methods ================================
    }
}