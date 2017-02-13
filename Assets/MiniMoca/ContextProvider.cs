// <copyright file="ContextProvider.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>01/31/2017 9:25</date>

using System;
using System.Collections.Generic;

namespace MiniMoca
{
    /// <summary>
    /// ContextProvider - provide access to concrete context of application
    /// </summary>
    public static class ContextProvider
    {
        //================================       Public Setup       =================================
        public static bool HasContexts
        {
            get
            {
#if MOCA_CONTEXT_REFLECTION
                return Contexts.Count > 0 || PostponedTypes.Count > 0;
#else
                return Contexts.Count > 0;
#endif
            }
        }

        //================================    Systems properties    =================================
#if MOCA_CONTEXT_REFLECTION
        private static readonly List<Type> PostponedTypes = new List<Type>();
#endif
        /// <summary>
        /// List of created contexts.
        /// </summary>
        private static readonly List<MocaContext> Contexts = new List<MocaContext>();
        //================================      Public methods      =================================
        /// <summary>
        /// Register context if context provider
        /// </summary>
        /// <param name="context">Instance of context</param>
        public static void Register(MocaContext context)
        {
            Type contextType = context.GetType();
            int n = Contexts.Count;
            MocaContext previous = null;
            for (int i = 0; i < n; ++i)
            {
                previous = Contexts[i];
                //Context with a same type was found
                if (previous.GetType() == contextType) break;
                previous = null;
            }
            /*
                Context with a same type was already registered.
                Set a previous context as invalide and change it to new context instance
            */
            if (previous != null)
            {
                previous._isValid = false;
                Contexts.Remove(previous);
            }
            //Set new context as valid one and save it
            context._isValid = true;
            context.Initialize();
            Contexts.Add(context);
        }

#if MOCA_CONTEXT_REFLECTION
        /// <summary>
        /// Register context in context provider with posponed creation
        /// </summary>
        /// <param name="contextType">Type of the context</param>
        public static void Register(Type contextType)
        {
            if (PostponedTypes.Contains(contextType)) return;//Type already saved
            if(contextType.BaseType != typeof(MocaContext))
                throw new ArgumentException("ContextType mast inherited MocaContext abstraction", "contextType");
            PostponedTypes.Add(contextType);
        }
#endif

        /// <summary>
        /// Provide access to concrete context by context type
        /// </summary>
        /// <param name="contextType">Type of the context</param>
        /// <returns>Instance of the context</returns>
        public static MocaContext Retrieve(Type contextType)
        {
#if MOCA_CONTEXT_REFLECTION
            if (PostponedTypes.Count > 0 && PostponedTypes.Contains(contextType))
            {
                PostponedTypes.Removal(contextType);
                MocaContext context = (MocaContext) MocaReflector.Instantiate(contextType);
                if (context == null)
                    throw new Exception("Posponed creation failed!");
                Register(context);
            }
#endif
            int i, n = Contexts.Count;
            for (i = 0; i < n; ++i)
            {
                if (Contexts[i].GetType() == contextType)
                    return Contexts[i];
            }
            throw new ArgumentException("Context wasn't registered yet!", "contextType");
        }

        /// <summary>
        /// Removal context from provider and dispose it
        /// </summary>
        /// <param name="contextType">Type of the context</param>
        public static void Remove(Type contextType)
        {
            int i, n = Contexts.Count;
            for (i = 0; i < n; ++i)
            {
                if (Contexts[i].GetType() != contextType) continue;
                //Set context as invalid and remove context from provider
                Contexts[i]._isValid = false;
                Contexts.RemoveAt(i);
                return;
            }
        }
        //================================ Private|Protected methods ================================
    }
}