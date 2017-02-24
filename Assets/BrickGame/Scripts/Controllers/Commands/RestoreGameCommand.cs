// <copyright file="RestoreGameCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 17:15</date>

using System;
using BrickGame.Scripts.Models;
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
            string compressed = cacheModel.GetPlaygroundCache(Data.Mode, Data.Session);
            if (compressed.Length < 3)
            {
                Context.Notify(GameState.Start);
                return; //Nothing to restore
            }
            Debug.LogFormat("{0} has compressed string {1}", Data.Mode, compressed);
            //Restore playground from cache
            try
            {
                int x, y;
                bool[] figure;
                DataConverter.ExtractFigure(ref compressed, out figure, out x, out y);
                bool[] matrix = DataConverter.GetMatrix(compressed);
                Debug.LogFormat("Restore figure x = {0} y = {1}: {2}", x, y, figure);
                //Restore score
                int score, lines, level;
                cacheModel.GetScore(Data.Mode, Data.Session, out score, out lines, out level);
                Debug.LogFormat("Restore game {0} for {1}: score = {2}, lines = {3}, level = {4}",
                    Data.Mode, Data.Session, score, lines, level);
                Context.GetActor<RestoreModel>()
                    .Push(
                        Data.Session, new RestoreModel.RestoredData
                        {
                            Level = level,
                            Score = score,
                            Lines = lines,
                            Matrix = matrix,
                            Figure = figure,
                            FigureX = x,
                            FigureY = y
                        });
            }
            catch (Exception exception)
            {
                Debug.LogErrorFormat("Error was occure while restoring: {0}", exception);
                //Delete corupted cache
                cacheModel.CleanPlayground(Data.Mode, Data.Session);
            }
            finally
            {
                Context.Notify(GameState.Start);
            }
        }

        //================================ Private|Protected methods ================================

        private bool[] CreateTestMatrix()
        {
            bool[] matrix = new bool[200];
            int[] linesArr = {19, 18,17, 15};
            foreach (int y in linesArr)
            {
                for (int x = 0; x < 10; x++)matrix[x + y * 10] = true;
            }
            matrix[1 + 16 * 10] = true;
            matrix[1 + 17 * 10] = true;
            return matrix;
        }
    }
}