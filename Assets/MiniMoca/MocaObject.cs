// <copyright file="MocaObject.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>01/31/2017 9:26</date>

namespace MiniMoca
{
    /// <summary>
    /// MocaObject - base oobject of MiniMoca framework
    /// </summary>
    /// <typeparam name="T">Concrete context type that inherited MocaContext</typeparam>
    public abstract class MocaObject<T> where T : MocaContext
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
                    _context = (T) ContextProvider.Retrieve( typeof(T) );
                return _context;
            }
        }
    }
}