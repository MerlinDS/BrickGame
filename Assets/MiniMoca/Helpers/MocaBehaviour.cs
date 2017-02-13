// <copyright file="MocaBehaviour.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>01/31/2017 11:32</date>

using UnityEngine;

namespace MiniMoca.Helpers
{
    /// <summary>
    /// MocaBehaviour - base MonoBahviour of the MiniMoca.
    /// Added syntax sugar to Unity MonoBehaviour
    /// </summary>
    public class MocaBehaviour<T> : MonoBehaviour where T : MocaContext
    {
        //================================       Public Setup       =================================
        //================================    Systems properties    =================================
        /// <summary>
        /// Cached context
        /// </summary>
        private static T _context;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <summary>
        /// Provide access to valid and concrete context
        /// </summary>
        protected static T Context
        {
            get
            {
                /*
                    Get new context from provider if context wasn't retrived
                    or context cache need to be updated.
                */
                if (_context == null || !_context.IsValid)
                    _context = (T) ContextProvider.Retrieve(typeof(T));
                return _context;
            }
        }

        /// <summary>
        /// Broadcast notification to context
        /// </summary>
        /// <param name="notification">Notification name</param>
        public void BroadcastNofitication(string notification)
        {
            BroadcastNofitication(notification, null);
        }

        /// <summary>
        /// Broadcast notification to context
        /// </summary>
        /// <param name="notification">Notification name</param>
        /// <param name="dataProvider">Data provider</param>
        public void BroadcastNofitication(string notification, DataProvider dataProvider)
        {
            Context.Notify(notification, dataProvider);
        }
    }
}