// <copyright file="ScoreIndicator.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/22/2017 18:18</date>

using System.Text;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace BrickGame.Scripts.UI.MainMenu
{
    /// <summary>
    /// ScoreIndicator
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class ScoreIndicator : GameBehaviour
    {
        //================================       Public Setup       =================================
        [Tooltip("Count of digits in indicator")] public int CountOfDigits;

        [Tooltip("Default digid value")] public string EmptyDigit = "@";

        //================================    Systems properties    =================================
        private Text _textField;

        private CacheModel _model;

        private StringBuilder _builder;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <summary>
        /// Inintialize conponent and add context listener
        /// </summary>
        private void Awake()
        {
            _textField = GetComponent<Text>();
            _model = Context.GetActor<CacheModel>();
            _builder = new StringBuilder(CountOfDigits);
        }

        private void Start()
        {
            if (_textField == null)
            {
                Debug.LogWarning("TextField for value representation was not set yet!");
                return;
            }
            //Creating string for textifield
            int value = _model.GetMaxSocre("Classic");
            int count = CountOfDigits - value.CountOfDigits();
            while (count-- > 0) _builder.Append(EmptyDigit);
            _builder.Append(value);
            _textField.text = _builder.ToString();
            //Clean builer for next usage
            _builder.Remove(0, _builder.Length);
        }
    }
}