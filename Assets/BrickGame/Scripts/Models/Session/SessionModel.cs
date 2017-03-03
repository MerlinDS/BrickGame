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
        /// <summary>
        /// Get current state of the session.
        /// </summary>
        public SessionState State
        {
            get { return _state; }
        }

        //================================    Systems properties    =================================
        private SessionState _state;
        //================================      Public methods      =================================
        /// <summary>
        /// Try to update session state.
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

        /// <summary>
        /// Check if session model has states
        /// </summary>
        /// <param name="states">states to check</param>
        /// <returns>True if has all states, false in other case</returns>
        public bool All(params SessionState[] states)
        {
            SessionState state = SessionState.None;
            for (int i = 0; i < states.Length; i++)
            {
                if (i == 0) state = states[i];
                else state |= states[i];
            }
            return (_state & state) == state;
        }
        /// <summary>
        /// Check if one of the session state flag contins in this model
        /// </summary>
        /// <param name="states">States to check</param>
        /// <returns>True if has one of the state, false in other cases</returns>
        public bool Any(params SessionState[] states)
        {
            for (int i = 0; i < states.Length; i++)
            {
                if ((_state & states[i]) == states[i]) return true;
            }
            return false;
        }
        /// <summary>
        /// Check if model has state
        /// </summary>
        /// <param name="state">State to check</param>
        /// <returns>True if has, false if has not.</returns>
        public bool Has(SessionState state)
        {
            return (_state & state) == state;
        }
        //================================ Private|Protected methods ================================
        private void Clean()
        {

        }


    }
}