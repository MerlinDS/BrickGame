// <copyright file="PlaygroundController.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/19/2017 23:37</date>

using System.Collections.Generic;
using BrickGame.Scripts.Models;
using JetBrains.Annotations;

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

        /// <summary>
        ///
        /// </summary>
        [UsedImplicitly]
        public void AppendFigure()
        {
            //Find all filled lines and delete them
            List<int> lines = new List<int>();
            for (int y = 0; y < _matrix.Height; ++y)
            {
                if(_matrix.RowContainsValue(y, true))
                    lines.Add(y);
            }
            if (lines.Count == 0)
            {
                //Full lines were not found. Nothing to do.
                BroadcastMessage(MessageReceiver.ChangeFigure);
                return;
            }
            //Full lines exist, need to remove these lines.
            //TODO: Send lines to score and update figure speed
            int count = lines.Count;
            if (count <= 0) return;
            foreach (int y in lines)_matrix.FillRow(y, false);
            _matrix.MoveDownRows(lines[0] - 1, lines[lines.Count - 1]);
            BroadcastMessage(MessageReceiver.ChangeFigure);
        }
        //================================ Private|Protected methods ================================
    }
}