// <copyright file="FigureMatrix.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/18/2017 19:23</date>

using JetBrains.Annotations;

namespace BrickGame.Scripts.Models
{
    /// <summary>
    /// FigureMatrix - class that represent figures in the game as the rectangular matrix.
    /// </summary>
    public class FigureMatrix : Matrix<bool>
    {
        //================================       Public Setup       =================================
        // ReSharper disable InconsistentNaming
        public int x;
        public int y;
        // ReSharper restore InconsistentNaming
        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <inheritdoc />
        public FigureMatrix([NotNull] bool[] matrix, int width, int height, bool isStrict = false, bool isReadOnly = true) :
            base(matrix, width, height, isStrict, isReadOnly)
        {
        }

        /// <summary>
        /// Constructor for epmty figureMatrix
        /// </summary>
        public FigureMatrix()
        {

        }
        /// <summary>
        /// Create new figureMatrix from the template
        /// </summary>
        /// <param name="glyph">tamplate of a figureMatrix</param>
        public FigureMatrix([NotNull] Glyph glyph):base(glyph.GetFigureMatrix(), true)
        {

        }
        //================================ Private|Protected methods ================================
    }
}