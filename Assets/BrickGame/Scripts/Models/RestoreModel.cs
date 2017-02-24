// <copyright file="RestoreModel.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 20:15</date>

using System.Collections.Generic;
using MiniMoca;

namespace BrickGame.Scripts.Models
{
    /// <summary>
    /// RestoreModel 
    /// </summary>
    public class RestoreModel : IMocaActor
    {
        public struct RestoredData
        {
            public int Score;
            public int Lines;
            public int Level;
            public int FigureX;
            public int FigureY;
            public bool[] Figure;
            public bool[] Matrix;
        }

        //================================       Public Setup       =================================
        
        //================================    Systems properties    =================================
        private readonly Dictionary<string, RestoredData> _restoredDatas;
        //================================      Public methods      =================================
        /// <inheritdoc />
        public RestoreModel()
        {
            _restoredDatas = new Dictionary<string, RestoredData>();
        }

        public void Push(string name, RestoredData data)
        {
            _restoredDatas.Add(name, data);
        }

        public RestoredData Pop(string name)
        {
            RestoredData data;
            if (_restoredDatas.TryGetValue(name, out data))
                _restoredDatas.Remove(name);
            return data;
        }

        public bool Has(string name)
        {
            return _restoredDatas.ContainsKey(name);
        }
        //================================ Private|Protected methods ================================

    }
}