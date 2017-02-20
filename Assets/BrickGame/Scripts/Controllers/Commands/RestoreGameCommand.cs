// <copyright file="RestoreGameCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 17:15</date>

using System;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Playgrounds;
using BrickGame.Scripts.Utils;
using UnityEngine;

namespace BrickGame.Scripts.Controllers.Commands
{
    /// <summary>
    /// RestoreGameCommand 
    /// </summary>
    public class RestoreGameCommand : GameCommand<SessionDataProvider>
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <inheritdoc />
        public override void Execute()
        {
            CacheModel cacheModel = Context.GetActor<CacheModel>();
            string compressed = cacheModel.GetPlaygroundCache(Data.Rules.name, Data.Name);
            if (compressed.Length == 0)
            {
                Context.Notify(GameState.Start);
                return; //Nothing to restore
            }
            Debug.LogFormat("{0} has compressed string {1}", Data.Name, compressed);
            //Restore playground from cache
            try
            {
                bool[] matrix;
                int[] figure;
                DataConverter.GetMatrix(compressed, out matrix, out figure);
                //Restore score
                int score, lines;
                cacheModel.GetScore(Data.Rules.name, Data.Name, out score, out lines);
                int level = Data.Rules.GetLevel(lines);
                Debug.LogFormat("Restore game {0} for {1}: score = {2}, lines = {3}, level = {4}",
                    Data.Rules.name, Data.Name, score, lines, level);
                Context.GetActor<RestoreModel>()
                    .Push(
                        Data.Name, new RestoreModel.RestoredData
                        {
                            Level = level,
                            Score = score,
                            Lines = lines,
                            Figure = figure,
                            Matrix = matrix
                        });
            }
            catch (Exception exception)
            {
                Debug.LogErrorFormat("Error was occure while restoring: {0}", exception);
                //Delete corupted cache
                cacheModel.CleanPlayground(Data.Rules.name, Data.Name);
            }
            finally
            {
                Context.Notify(GameState.Start);
            }
        }

        //================================ Private|Protected methods ================================
    }
}