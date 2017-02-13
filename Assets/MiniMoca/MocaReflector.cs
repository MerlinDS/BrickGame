// <copyright file="MocaReflector.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/01/2017 13:38</date>

using System;

namespace MiniMoca
{
    /// <summary>
    /// MocaReflector - responsible for instantiations of MiniMoca internal classes
    /// </summary>
    public static class MocaReflector
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private static readonly string CommandInterface = typeof(IMocaCommand).ToString();
        private static readonly string ActorInterface = typeof(IMocaActor).ToString();

        /*
        //TODO Instantiate through Exprition
        static readonly Func<Type, IMocaCommand> CommandExpression = Expression.Lambda<Func<Type, IMocaCommand>>(
            Expression.New(  ),//type.GetConstructor(Type.EmptyTypes)
        ).Compile();
        */
        //================================      Public methods      =================================

        /// <summary>
        /// Instantiate object without arguments
        /// </summary>
        /// <param name="type">Type of a class that need to be instantiated</param>
        /// <returns>Instance of type</returns>
        public static object Instantiate(Type type)
        {
            return Activator.CreateInstance(type);
        }
        /// <summary>
        /// Instantiate object without arguments
        /// </summary>
        /// <typeparam name="T">Type of a class that need to be instantiated</typeparam>
        /// <returns>Instance of type</returns>
        public static T Instantiate<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
        /// <summary>
        /// Instantiate object with arguments
        /// </summary>
        /// <param name="args">Constructor arguments</param>
        /// <typeparam name="T">Type of a class that need to be instantiated</typeparam>
        /// <returns>Instance of type</returns>
        public static T Instantiate<T>(params object[] args)
        {
            if (args.Length == 0) return Instantiate<T>();
            return (T)Activator.CreateInstance(typeof(T), args: args);
        }

        /// <summary>
        /// Instantiate MiniMoca command
        /// </summary>
        /// <typeparam name="T">Command type must implenents IMocaCommand interface</typeparam>
        /// <returns>New instance of the command</returns>
        public static T InstantiateCommand<T>() where T : IMocaCommand
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
        /// <summary>
        /// Instantiate MiniMoca command
        /// </summary>
        /// <param name="type">Command type must implenents IMocaCommand interface</param>
        /// <returns>New instance of the command</returns>
        /// /// <exception cref="ArgumentException">Type must implenent IMocaCommand interface</exception>
        public static IMocaCommand InstantiateCommand(Type type)
        {
            if (type.GetInterface(CommandInterface) == null)
                throw new ArgumentException("Type must implenent IMocaCommand interface", "type");
            return (IMocaCommand)Activator.CreateInstance(type);
        }

        /// <summary>
        /// Instantiate MiniMoca actor
        /// </summary>
        /// <param name="type">Type of the actor that implenents IMocaActor interface</param>
        /// <returns>New Instance of the actor</returns>
        /// <exception cref="ArgumentException">Type must implenent IMocaActor interface</exception>
        public static IMocaActor InstantiateActor(Type type)
        {
            if (type.GetInterface(ActorInterface) == null)
                throw new ArgumentException("Type must implenent IMocaActor interface", "type");
            return (IMocaActor)Activator.CreateInstance(type);
        }
        /// <summary>
        /// Instantiate MiniMoca actor
        /// </summary>
        /// <typeparam name="T">Type of the actor that implenents IMocaActor interface</typeparam>
        /// <returns>New Instance of the actor</returns>
        public static T InstantiateActor<T>() where T : IMocaActor
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
        //================================ Private|Protected methods ================================
    }
}