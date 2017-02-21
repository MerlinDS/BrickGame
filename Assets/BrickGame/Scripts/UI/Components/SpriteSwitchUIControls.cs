// <copyright file="SpriteSwitchUIControls.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/21/2017 18:01</date>

using BrickGame.Scripts.Controllers;

namespace BrickGame.Scripts.UI.Components
{
    /// <summary>
    /// SpriteSwitchUIControls
    /// </summary>
    public class SpriteSwitchUIControls : GameBehaviour
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        public void OnClick(int index = 0)
        {
            Context.GetActor<BricksPrefabManager>().ChangeSprite(index);
        }
        //================================ Private|Protected methods ================================

    }
}