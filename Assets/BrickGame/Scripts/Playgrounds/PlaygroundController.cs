// <copyright file="PlaygroundController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 23:37</date>

using BrickGame.Scripts.Models;

namespace BrickGame.Scripts.Playgrounds
{
    /// <summary>
    /// PlaygroundController - controller for playground game object
    /// </summary>
    public class PlaygroundController : GameBehaviour, MessageReceiver.IPlaygroundReceiver
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private PlaygroundMatrix _matrix;
        //================================      Public methods      =================================
        /// <inheritdoc />
        public void UpdateMatix(Matrix<bool> matrix)
        {
            _matrix = (PlaygroundMatrix)matrix ?? new PlaygroundMatrix(0, 0);
        }
        //================================ Private|Protected methods ================================
    }
}