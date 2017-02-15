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
        /// Execute figure creation.
        /// </summary>
        public const string CreateFigure = "CreateFigure";

        /// <summary>
        /// Send message to IModelMessageResiver
        /// <see cref="IModelMessageResiver"/>
        /// </summary>
        public const string UpdateModel = "UpdateModel";
    }

    public interface IModelMessageResiver
    {
        /// <summary>
        /// Update model in component
        /// </summary>
        /// <param name="model"></param>
        void UpdateModel(PlaygroundModel model);
    }
}
