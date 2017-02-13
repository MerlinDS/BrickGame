// <copyright file="MocaManager.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>01/31/2017 12:52</date>

namespace MiniMoca.Helpers
{
    /// <summary>
    /// MocaManager - abstract helper for managers
    /// </summary>
    public abstract class MocaManager<T> : MocaBehaviour<T>, IMocaActor  where T : MocaContext
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
    }
}