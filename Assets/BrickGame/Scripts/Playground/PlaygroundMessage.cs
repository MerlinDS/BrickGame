// <copyright file="PlaygroundMessage.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 18:37</date>
namespace BrickGame.Scripts.Playground
{
    /// <summary>
    /// PlaygroundMessage - internal playground message.
    /// Invoke internal methods.
    /// </summary>
    public static class PlaygroundMessage
    {
        /// <summary>
        /// Execute figureMatrix creation.
        /// </summary>
        public const string CreateFigure = "CreateFigure";

        /// <summary>
        /// Send message to IModelMessageResiver
        /// <see cref="IModelMessageResiver"/>
        /// </summary>
        public const string UpdateModel = "UpdateModel";

        public const string RestoreFigure = "RestoreFigure";
    }

    public interface IFigureMessageResiver
    {
        /// <summary>
        /// Restore figureMatrix from cache
        /// </summary>
        /// <param name="figure">array of figureMatrix cells in playground matrix</param>
        void RestoreFigure(int[] figure);
        /// <summary>
        /// Create new figureMatrix
        /// </summary>
        void CreateFigure();
    }

    public interface IModelMessageResiver
    {
        /// <summary>
        /// UpdateColors model in component
        /// </summary>
        /// <param name="model"></param>
        void UpdateModel(PlaygroundModel model);
    }
}
