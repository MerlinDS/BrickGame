// <copyright file="CommandMap.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>01/31/2017 9:23</date>

using System;
using System.Collections.Generic;

namespace MiniMoca
{
    /// <summary>
    /// CommandMap -
    /// </summary>
    public class CommandMap
    {
        //================================       Public Setup       =================================
        
        //================================    Systems properties    =================================
        private class CommandHolder : IDisposable
        {
            public Type CommandType;
            public bool ExecuteOnce;
            public IMocaCommand Instance;

            /// <inheritdoc />
            public void Dispose()
            {
                if(Instance != null)Instance.Dispose();
                Instance = null;
                CommandType = null;
            }
        }

        private readonly Dictionary<string, CommandHolder> _commands;
        //================================      Public methods      =================================
        /// <inheritdoc />
        public CommandMap()
        {
            _commands = new Dictionary<string, CommandHolder>();
        }

        ~CommandMap()
        {
            foreach (var pair in _commands)
                pair.Value.Dispose();
            _commands.Clear();
        }

        /// <summary>
        /// Map command
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="commandType"></param>
        /// <param name="executeOnce"></param>
        /// <exception cref="ArgumentException"></exception>
        public void MapCommand(string notification, Type commandType, bool executeOnce = false)
        {
            if (HasMapping(notification, commandType))
                throw new ArgumentException("Command already mapped!");
            _commands.Add(notification, new CommandHolder
            {
                CommandType = commandType,
                ExecuteOnce = executeOnce
            });
        }

        /// <summary>
        /// Map command
        /// </summary>
        /// <param name="notification"></param>
        /// <typeparam name="T"></typeparam>
        /// <param name="executeOnce"></param>
        /// <exception cref="ArgumentException"></exception>
        public void MapCommand<T>(string notification, bool executeOnce = false) where T:IMocaCommand
        {
            MapCommand(notification, typeof(T), executeOnce);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="notification"></param>
        public void UnMapCommand(string notification)
        {
            CommandHolder holder = GetHolder(notification);
            holder.Dispose();
            _commands.Remove(notification);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public bool HasMapping(string notification, Type commandType = null)
        {
            if (commandType == null)
                return _commands.ContainsKey(notification);
            return _commands.ContainsKey(notification)
                   && _commands[notification].CommandType == commandType;
        }

        /// <summary>
        /// Execute registered command
        /// </summary>
        /// <param name="notification">Notification name</param>
        /// <param name="data">Data provider</param>
        public void Execute(string notification, DataProvider data = null)
        {
            CommandHolder holder = GetHolder(notification);
            IMocaCommand command = holder.Instance;
            if (command == null)
                command = MocaReflector.InstantiateCommand(holder.CommandType);
            command.Prepare(data);
            command.Execute();
            //Clean and save for next usage
            command.Dispose();
            /*
                Save command instance to reuse on next execution.
                If MOCA_COMMAND_REFLECTION == true doesn't save command instance to holder.
                New command instance will be created on each execution.
                PERFORMANCE AND MEMORY IMPACT!
            */
#if !MOCA_COMMAND_REFLECTION
            holder.Instance = command;
#endif
            if (!holder.ExecuteOnce) return;
            //Removal command after first execution
            UnMapCommand(notification);
        }
        //================================ Private|Protected methods ================================

        private CommandHolder GetHolder(string notification)
        {
            CommandHolder holder;
            if(!_commands.TryGetValue(notification, out holder))
                throw new ArgumentException("Command for notification " + notification +
                                            " was not mapped yet!", "notification");
            return holder;
        }
    }
}