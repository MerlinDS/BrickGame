// <copyright file="DataProvider.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>01/31/2017 10:05</date>

using System;

namespace MiniMoca
{
    /// <summary>
    /// DataProvider - abstract class for data or arguments providing
    /// for MiniMoca notifications and commands
    /// </summary>
    public abstract class DataProvider : IDisposable
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <summary>
        /// Dispose data provider
        /// </summary>
        public abstract void Dispose();

        //================================ Private|Protected methods ================================
    }
}