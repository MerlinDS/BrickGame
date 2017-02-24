// <copyright file="RestoreGameCommand.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/15/2017 17:15</date>

using System;
using BrickGame.Scripts.Models;
using BrickGame.Scripts.Utils;
using JetBrains.Annotations;
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
            //Restore playground from cache
            try
            {
                if(compressed.Length == 0)return;
                Debug.LogFormat("{0} has compressed string {1}", Data.Mode, compressed);
                int[] header;
                compressed = DataConverter.ExtractHeader(compressed, out header);
                bool[][] matrices = DataConverter.GetArray(compressed);
                if (matrices.Length < 2)return;
                var figure = RestoreFigure(matrices[0], header);
                var playground = RestorePlayground(matrices[1], 10, 20);
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
                            Playground = playground,
                            Figure = figure
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
        [NotNull]
        [ContractAnnotation("rect:null=>stop")]
        private Matrix<bool> RestoreFigure([CanBeNull] bool[] data, [NotNull] int[] rect)
        {
            if (data == null) return new FigureMatrix();
            if (rect == null) throw new ArgumentNullException("rect");
            int len = rect[2] * rect[3];
            bool[] m = new bool[len];
            int i = 0;
            do{
                m[i] = data[i];
            } while (++i < len);
            return new FigureMatrix(m, rect[2], rect[3]) {x = rect[0], y = rect[1]};
        }

        [NotNull]
        private Matrix<bool> RestorePlayground([CanBeNull] bool[] data, int width, int height)
        {
            if(data == null || data.Length == 0)
                return new PlaygroundMatrix(width, height);

            int len = width * height;
            bool[] matrix;
            if (data.Length - len > 0)
            {
                matrix = new bool[len];
                for(int i = 0; i < len; i++)
                    matrix[i] = data[i];
            }
            else
                matrix = data;
            return new PlaygroundMatrix(matrix, width, height);
        }

        private bool[] CreateTestMatrix()
        {
            bool[] matrix = new bool[200];
            int[] linesArr = {19, 18, 17, 15};
            foreach (int y in linesArr)
            {
                for (int x = 0; x < 10; x++) matrix[x + y * 10] = true;
            }
            matrix[1 + 16 * 10] = true;
            matrix[1 + 17 * 10] = true;
            return matrix;
        }
    }
}