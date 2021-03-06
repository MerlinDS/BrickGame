﻿// <copyright file="MessageReceiver.cs" company="Near Fancy">
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
        ///
        /// </summary>
        public const string UpdateScore = "UpdateScore";
        /// <summary>
        ///
        /// </summary>
        public const string FinishSession = "FinishSession";

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
        /// Message for updating matrix in matrix receiver.
        /// <para>
        /// This message will execute <see cref="IFigureReceiver.UpdateFigure"/> method in receiver.
        /// </para>
        /// </summary>
        public const string UpdateFigure = "UpdateFigure";
        /// <summary>
        ///
        /// </summary>
        public const string AppendFigure = "AppendFigure";
        /// <summary>
        ///
        /// </summary>
        public const string ChangeFigure = "ChangeFigure";
        /// <summary>
        ///Accelerate figure falling speed
        /// </summary>
        public const string AccelerateFigure = "AccelerateFigure";
        /// <summary>
        /// Methods for matrix receiver
        /// </summary>
        public interface IFigureReceiver
        {
            /// <summary>
            /// Update fiugre matrix in receiver
            /// </summary>
            /// <param name="matrix">Instance of the new matrix</param>
            void UpdateFigure([CanBeNull] FigureMatrix matrix);
        }
        //================================            FigureMatrix            =============================
    }
}