// <copyright file="StateChangeCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>03/03/2017 13:29</date>

using BrickGame.Scripts.Models.Session;
using UnityEngine;

namespace BrickGame.Scripts.Controllers.Commands
{
    /// <summary>
    /// StateChangeCommand - controls session state changing
    /// </summary>
    public class StateChangeCommand : GameCommand
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <inheritdoc />
        public override void Execute()
        {
            var sessionModel = Context.GetActor<SessionModel>();
            SessionState next;
            SessionState previous = sessionModel.State;
            switch (Notification)
            {
                case StateNotification.Start:
                    next = SessionState.Started;
                    break;
                case StateNotification.Pause:
                    next = SessionState.OnPause;
                    break;
                case StateNotification.End:
                    next = SessionState.Ended;
                    break;
                case StateNotification.Close:
                    next = SessionState.None;
                    break;
                default:
                    //Nothing to do with session model;
                    return;
            }
            if (!sessionModel.TryUpdateState(next))
            {
                Debug.LogWarningFormat("Session state {0} was not update to {1}", previous, next);
                return;
            }
            Debug.LogFormat("Current state is {0}", sessionModel.State);
        }

        //================================ Private|Protected methods ================================
    }
}