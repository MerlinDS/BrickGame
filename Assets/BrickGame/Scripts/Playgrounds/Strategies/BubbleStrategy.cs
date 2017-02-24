// <copyright file="BubbleStrategy.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/23/2017 19:34</date>

using BrickGame.Scripts.Figures;
using BrickGame.Scripts.Models;
using UnityEngine;

namespace BrickGame.Scripts.Playgrounds.Strategies
{
    /// <summary>
    /// BubbleStrategy
    /// </summary>
    public sealed class BubbleStrategy : AbstractStrategy
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================

        /// <inheritdoc />
        protected override void Apply(Playground playground, Figure figure)
        {
            float maxSpeed = playground.Rules.GetSpeedByLines(playground.TotalLines);
            if(figure.Speed > maxSpeed + playground.Rules.SpeedIncreaser)return;
            figure.SendMessage(MessageReceiver.AccelerateFigure, figure.Speed + 0.1F,
                SendMessageOptions.DontRequireReceiver);
        }
    }
}