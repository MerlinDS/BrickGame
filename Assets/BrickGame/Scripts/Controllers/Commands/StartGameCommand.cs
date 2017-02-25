// <copyright file="StartGameCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 13:07</date>

using BrickGame.Scripts.Models;
using BrickGame.Scripts.Playgrounds;
using JetBrains.Annotations;
using MiniMoca;
using UnityEngine;

namespace BrickGame.Scripts.Controllers.Commands
{
    /// <summary>
    /// StartGameCommand - execute starting of the game mode.
    /// </summary>
    public class StartGameCommand : GameCommand
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private RestoreModel _restoreModel;

        //================================      Public methods      =================================
        /// <inheritdoc />
        public override void Prepare(string notification, DataProvider data)
        {
            _restoreModel = Context.GetActor<RestoreModel>();
            base.Prepare(notification, data);
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            _restoreModel = null;
            base.Dispose();
        }

        /// <inheritdoc />
        public override void Execute()
        {
            if (Playgrounds == null || Playgrounds.Length == 0)
            {
                Debug.LogError("Playground was not found!");
                return;
            }

            //In common cases will be one playground on the stage
            foreach (var playground in Playgrounds)
            {
                Matrix<bool> fm, pm;
                CollectData(playground, out fm, out pm);
                StartGame(playground, pm, fm);

            }
        }

        //================================ Private|Protected methods ================================
        /// <summary>
        /// Collect data for specified playground
        /// </summary>
        /// <param name="playground">Playground instance</param>
        /// <param name="pm">Playground matrix</param>
        /// <param name="fm">Figure matrix</param>
        private void CollectData([NotNull] Playground playground, out Matrix<bool> pm, out Matrix<bool> fm)
        {
            ScoreDataProvider sdp;
            var session = playground.SessionName;
            //Has or hasn't restored data
            if (_restoreModel.Has(session))
            {
                //Restore data
                var data = _restoreModel.Pop(session);
                fm = data.Figure;
                pm = data.Playground;
                sdp = new ScoreDataProvider(session, data.Lines, data.Score, data.Level);
                return;
            }
            //Create playground data for clean start
            sdp = new ScoreDataProvider(session, 0, 0, 1);
            pm = new PlaygroundMatrix(playground.Rules.Width, playground.Rules.Height);
            fm = new FigureMatrix();
            //Update score command
            Context.Notify(GameNotification.ScoreUpdated, sdp);
        }

        /// <summary>
        /// Start the game
        /// </summary>
        /// <param name="playground">Playground instance</param>
        /// <param name="pm">Playground matrix</param>
        /// <param name="fm">Figure matrix</param>
        private void StartGame([NotNull] Playground playground, Matrix<bool> pm, Matrix<bool> fm)
        {
            Debug.LogFormat("Start new game session: {0}", playground.SessionName);
            //Send starting messages
            playground.SendMessage(MessageReceiver.UpdateMatix, pm,
                SendMessageOptions.DontRequireReceiver);
            //Send models to children
            int n = playground.transform.childCount;
            for (int i = 0; i < n; i++)
            {
                var child = playground.transform.GetChild(i);
                child.SendMessage(MessageReceiver.UpdateMatix, pm,
                    SendMessageOptions.DontRequireReceiver);
                child.SendMessage(MessageReceiver.UpdateFigure, fm,
                    SendMessageOptions.DontRequireReceiver);
            }
        }

    }
}