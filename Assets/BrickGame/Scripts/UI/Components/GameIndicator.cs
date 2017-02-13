// <copyright file="GameIndicator.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/13/2017 11:16</date>

using System.Text;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace BrickGame.Scripts.UI.Components
{
    /// <summary>
    /// GameIndicator
    /// </summary>

    public class GameIndicator : GameBehaviour
    {
        //================================       Public Setup       =================================
        [Tooltip("Name of the value that needs to be indicated")]
        public ScoreModel.FieldName Value = ScoreModel.FieldName.Score;

        [Tooltip("Count of digits in indicator")] public int CountOfDigits;
        [Tooltip("Default digid value")] public string EmptyDigit = "@";

        [Tooltip("TextField for value representation")] public Text ValueTextField;

        //================================    Systems properties    =================================
        private ScoreModel _model;
        private StringBuilder _builder;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        private void Awake()
        {
            _model = Context.GetActor<ScoreModel>();
            _builder = new StringBuilder(CountOfDigits);
            Context.AddListener(GameNotification.ScoreUpdated, GameNotificationHandler);
            //first update to initialize component
            GameNotificationHandler(GameNotification.ScoreUpdated);
        }

        private void OnDestroy()
        {
            Context.RemoveListener(GameNotification.ScoreUpdated, GameNotificationHandler);
        }

        private void GameNotificationHandler(string notification)
        {
            if (ValueTextField == null)
            {
                Debug.LogWarning("TextField for value representation was not set yet!");
                return;
            }
            //Creating string for textifield
            int value = _model[Value];
            int count = CountOfDigits - value.CountOfDigits();
            while (count-- > 0)_builder.Append(EmptyDigit);
            _builder.Append(value);
            ValueTextField.text = _builder.ToString();
            //Clean builer for next usage
            _builder.Remove(0, _builder.Length);


        }
    }
}