// <copyright file="SessionDataProvider.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 17:15</date>

using MiniMoca;

namespace BrickGame.Scripts.Models
{
    /// <summary>
    /// SessionDataProvider
    /// </summary>
    public class SessionDataProvider : DataProvider
    {
        //================================       Public Setup       =================================
        /// <summary>
        /// Session name
        /// </summary>
        public readonly string Mode;

        public readonly string Session;
        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <inheritdoc />
        public override void Dispose()
        {

        }

        /// <inheritdoc />
        public SessionDataProvider(string mode, string session)
        {
            Mode = mode;
            Session = session;
        }

        //================================ Private|Protected methods ================================
    }
}