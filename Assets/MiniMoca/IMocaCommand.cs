// <copyright file="IMocaCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>01/31/2017 9:19</date>
namespace MiniMoca
{
    /// <summary>
    /// IMocaCommand - internal interface for MiniMica commands
    /// </summary>
    public interface IMocaCommand
    {
        //================================       Getters|Setters       =================================
        //================================			 Methods		   =================================
        /// <summary>
        /// Prepare command for execution
        /// </summary>
        /// <param name="data">Data provider</param>
        void Prepare(DataProvider data);
        /// <summary>
        /// Execute concrete command
        /// </summary>
        void Execute();
        /// <summary>
        /// Dispose command after execution
        /// </summary>
        void Dispose();
    }
}