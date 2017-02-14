// <copyright file="CacheModel.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 20:14</date>

using MiniMoca;
using UnityEngine;

namespace BrickGame.Scripts.Models
{
    /// <summary>
    /// CacheModel
    /// </summary>
    public class CacheModel : IMocaActor
    {
        private const string ModeField = "_Mode";
        private const string ScoreField = "_Score";
        private const string LinesField = "_Lines";
        private const string PlaygroundField = "_Playground";
        private const string AudioField = "AudioMuted";
        //================================       Public Setup       =================================
        public bool AudioMuted
        {
            get { return PlayerPrefs.GetInt(AudioField) > 0; }
            set { PlayerPrefs.SetInt(AudioField, value ? 1 : 0); }
}
        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <inheritdoc />
        public CacheModel()
        {
        }

        public void DeleteCache(bool save = true)
        {
            if (!save)
            {
                PlayerPrefs.DeleteAll();
                return;
            }
            PlayerPrefs.DeleteKey(AudioField);
        }
        /// <summary>
        /// Update max score of spesified mode in cache.
        /// </summary>
        /// <param name="mode">Game mode</param>
        /// <param name="score">Current score</param>
        /// <param name="lines">Current lines</param>
        public void UpdateModeScore(string mode, int score, int lines)
        {
            mode += ModeField;
            //Get previous score
            int maxScore = PlayerPrefs.GetInt(mode + ScoreField);
            int maxLines = PlayerPrefs.GetInt(mode + LinesField);
            //Update score in mode
            if (score > maxScore)PlayerPrefs.GetInt(mode + ScoreField, score);
            if (score > maxLines)PlayerPrefs.GetInt(mode + LinesField, lines);

        }
        /// <summary>
        /// Clean cache of the playground for spesified mode in cache.
        /// </summary>
        /// <param name="mode">Game mode of the playground</param>
        public void CleanPlayground(string mode)
        {
            PlayerPrefs.DeleteKey(mode + ModeField + PlaygroundField);
        }
        /// <summary>
        /// Update playground cache for speified mode
        /// </summary>
        /// <param name="mode">Name of the mode</param>
        /// <param name="data">Playground cache</param>
        public void UpdatePlayground(string mode, string data)
        {
            PlayerPrefs.SetString(mode + ModeField + PlaygroundField, data);
            Debug.Log(data);
        }

        /// <summary>
        /// Get playground cache conpressed in string
        /// </summary>
        /// <param name="mode">Name of the mode</param>
        /// <returns>Playground cache</returns>
        public string GetPlaygroundCache(string mode)
        {
            return PlayerPrefs.GetString(mode + ModeField + PlaygroundField);
        }
        //================================ Private|Protected methods ================================
    }
}