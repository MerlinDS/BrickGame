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
    /// RestoreModel - model that represent a data restored from cache
    /// </summary>
    public class RestoreModel : IMocaActor
    {
        public struct RestoredData
        {
            public Matrix<bool> Figure;
            public Matrix<bool> Playground;
            public GameProgress Progress;
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

        /// <summary>
        /// Add restored data to model
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public void Push(string name, RestoredData data)
        {
            _restoredDatas.Add(name, data);
        }

        /// <summary>
        /// Get restrored data form model
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public RestoredData Pop(string name)
        {
            RestoredData data;
            if (_restoredDatas.TryGetValue(name, out data))
                _restoredDatas.Remove(name);
            return data;
        }

        /// <summary>
        /// Check if model contains restored data from cache
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Has(string name)
        {
            return _restoredDatas.ContainsKey(name);
        }
        //================================ Private|Protected methods ================================

    }
}