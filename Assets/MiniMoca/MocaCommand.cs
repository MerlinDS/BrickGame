// <copyright file="MocaCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>01/31/2017 10:10</date>

using System;

namespace MiniMoca
{
    /// <summary>
    /// MocaCommand - Abstract MiniMoca command
    /// </summary>
    /// <typeparam name="T">Type of the concrete context that inherited MocaContext</typeparam>
    public abstract class MocaCommand<T> : IMocaCommand where T : MocaContext
    {
        //================================       Public Setup       =================================
        /// <summary>
        /// Access to concrete context
        /// </summary>
        protected T Context { get; private set; }

        //================================    Systems properties    =================================
        private readonly Type _contextType;

        //================================      Public methods      =================================
        /// <inheritdoc />
        public virtual void Prepare(DataProvider data)
        {
            Context = (T) ContextProvider.Retrieve(_contextType);
        }

        /// <inheritdoc />
        public virtual void Dispose()
        {
            Context = null;
        }

        /// <inheritdoc />
        public abstract void Execute();

        //================================ Private|Protected methods ================================
        /// <summary>
        /// Internal command constuctor
        /// </summary>
        protected MocaCommand()
        {
            _contextType = typeof(T);
        }
    }

    /// <summary>
    /// MocaCommand - Abstract MiniMoca command that needs
    /// <see cref="DataProvider"/>
    /// </summary>
    /// <typeparam name="T">Type of the concrete context that inherited MocaContext</typeparam>
    /// <typeparam name="T1">Type of the DataProvider</typeparam>
    public abstract class MocaCommand<T, T1> : MocaCommand<T> where T : MocaContext where T1 : DataProvider
    {
        //================================       Public Setup       =================================
        /// <summary>
        /// Access to notification data provider
        /// </summary>
        protected T1 Data { get; private set; }

        //================================    Systems properties    =================================
        //================================      Public methods      =================================
        /// <inheritdoc />
        public override void Prepare(DataProvider data)
        {
            base.Prepare(data);
            Data = (T1) data;
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            Data = null;
            base.Dispose();
        }

        //================================ Private|Protected methods ================================
    }
}