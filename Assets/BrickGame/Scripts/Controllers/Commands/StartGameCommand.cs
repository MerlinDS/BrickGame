// <copyright file="StartGameCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 13:07</date>

using System.Linq;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Playgrounds;
using JetBrains.Annotations;
using MiniMoca;
using UnityEngine;

namespace BrickGame.Scripts.Controllers.Commands
{
    /// <summary>
    /// StartGameCommand - execute starting of the game on specified playground.
    /// </summary>
    public class StartGameCommand : GameCommand<SessionDataProvider>
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private RestoreModel _restoreModel;

        //================================      Public methods      =================================
        /// <inheritdoc />
        public override void Prepare(string notification, DataProvider data)
        {
            base.Prepare(notification, data);
            _restoreModel = Context.GetActor<RestoreModel>();
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
            Playground playground = Playgrounds.FirstOrDefault(p => p.SessionName == Data.Session);
            if (playground == null)
            {
                Debug.LogError("Playground was not found!");
                return;
            }

            Matrix<bool> fm, pm;
            CollectData(playground, out pm, out fm);
            StartGame(playground, pm, fm);
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
            var session = playground.SessionName;
            //Has or hasn't restored data
            if (_restoreModel.Has(session))
            {
                //Restore data
                var data = _restoreModel.Pop(session);
                fm = data.Figure;
                pm = data.Playground;
                Context.Notify(GameNotification.ScoreUpdated,
                    new ScoreDataProvider(session, data.Lines, data.Score, data.Level));
                return;
            }
            //Create playground data for clean start
            pm = new PlaygroundMatrix(playground.Rules.Width, playground.Rules.Height);
            fm = new FigureMatrix();
            //Update score command
            Context.Notify(GameNotification.ScoreUpdated, new ScoreDataProvider(session, 0, 0, 1));
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