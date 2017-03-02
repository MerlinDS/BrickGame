// <copyright file="InputManager.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>03/02/2017 22:21</date>

using MiniMoca;

namespace BrickGame.Scripts.Utils.Input
{
    //TODO remove this after moca will be changes
    /// <summary>
    /// InputManager -
    /// </summary>
    public class InputManager : IMocaActor
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        public IInputAdapter GetInputAdapter()
        {
            IInputAdapter adapter = UnityEngine.Input.touchSupported
                ?  (IInputAdapter) new TouchInputAdapter()
                : new MouseInputAdapter();
            return adapter;
        }
        //================================ Private|Protected methods ================================
    }
}