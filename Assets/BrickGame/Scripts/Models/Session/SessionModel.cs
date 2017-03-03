// <copyright file="SessionModel.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>03/03/2017 13:12</date>

using MiniMoca;

namespace BrickGame.Scripts.Models.Session
{
    /// <summary>
    /// SessionModel - contains information about game sessions.
    /// </summary>
    public class SessionModel : IMocaActor
    {
        //================================       Public Setup       =================================
        public SessionState State
        {
            get { return _state; }
        }

        //================================    Systems properties    =================================
        private SessionState _state;
        //================================      Public methods      =================================
        /// <summary>
        /// Try to update session state
        /// </summary>
        /// <param name="state">New session state</param>
        /// <returns>True is state was updated, false in other case</returns>
        public bool TryUpdateState(SessionState state)
        {
            //Update from None
            if (_state == SessionState.None)
            {
                //Only started state is correct it this case
                if (state != SessionState.Started) return false;
                //Mark session as started
                _state = SessionState.Started;
                return true;
            }
            //Update to None (clean session)
            if (state == SessionState.None)
            {
                Clean();
                _state = state;
                return true;
            }
            //Update started session
            if ((_state & state) == state)
            {
                /*
                    Session already has the new state flag.
                    If the new flag is "on pause", remove it.
                    If the new float is "start", check if session has flag "ended".
                    If session has this flag, start new session.
                    In other cases don't update the state.
                */
                if (state == SessionState.OnPause)
                {
                    _state ^= SessionState.OnPause;
                    return true;
                }
                if(state == SessionState.Started &&
                   (_state & SessionState.Ended) == SessionState.Ended)
                {
                    //recursive update and starting new session
                    TryUpdateState(SessionState.None);
                    return TryUpdateState(SessionState.Started);
                }
                return false;
            }
            //The new state flag wasn't added yet.
            _state |= state;
            return true;
        }
        //================================ Private|Protected methods ================================
        private void Clean()
        {

        }
    }
}