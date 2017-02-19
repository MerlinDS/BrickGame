// <copyright file="MessageReceiver.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 16:07</date>

using JetBrains.Annotations;

namespace BrickGame.Scripts.Models
{
    /// <summary>
    /// MessageReceiver - interfaces for game receiver
    /// </summary>
    public static class MessageReceiver
    {
        //================================       Playground       =================================

        /// <summary>
        /// Message for updating matrix in  playground receiver.
        /// <para>
        /// This message will execute <see cref="IPlaygroundReceiver.UpdateMatix"/> method in receiver.
        /// </para>
        /// </summary>
        public const string UpdateMatix = "UpdateMatix";
        /// <summary>
        /// Methods for playground receiver
        /// </summary>
        public interface IPlaygroundReceiver
        {
            /// <summary>
            /// Update playground matrix in receiver
            /// </summary>
            /// <param name="matrix">Instance of the new matrix</param>
            void UpdateMatix([CanBeNull] Matrix<bool> matrix);
        }

        /// <summary>
        /// Message for updating figure in figure receiver.
        /// <para>
        /// This message will execute <see cref="IFigureReceiver.UpdateFigure"/> method in receiver.
        /// </para>
        /// </summary>
        public const string UpdateFigure = "UpdateFigure";
        /// <summary>
        /// Methods for figure receiver
        /// </summary>
        public interface IFigureReceiver
        {
            /// <summary>
            /// Update fiugre matrix in receiver
            /// </summary>
            /// <param name="figure">Instance of the new figure</param>
            void UpdateFigure([CanBeNull] Figure figure);
        }
        //================================            Figure            =============================
    }
}