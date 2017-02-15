// <copyright file="CacheModel.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 20:14</date>

using System.Text;
using MiniMoca;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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
        /// Update score of spesified mode in cache.
        /// </summary>
        /// <param name="mode">Game mode</param>
        /// <param name="session">Name of the session</param>
        /// <param name="score">Current score</param>
        /// <param name="lines">Current lines</param>
        public void UpdateScore(string mode, string session, int score, int lines)
        {
            StringBuilder sb = new StringBuilder(mode);
            sb.Append(ModeField);
            //Get previous score
            int maxScore = PlayerPrefs.GetInt(sb + ScoreField);
            int maxLines = PlayerPrefs.GetInt(sb + LinesField);
            //Update score in mode
            if (score > maxScore)PlayerPrefs.SetInt(sb + ScoreField, score);
            if (lines > maxLines)PlayerPrefs.SetInt(sb + LinesField, lines);
            //Update session
            sb.Append('_');
            sb.Append(session);
            PlayerPrefs.SetInt(sb + ScoreField, score);
            PlayerPrefs.SetInt(sb + LinesField, lines);

        }

        /// <summary>
        /// Get score of the sesssion
        /// </summary>
        /// <param name="mode">Game mode</param>
        /// <param name="session">Name of the session</param>
        /// <param name="score">Current score</param>
        /// <param name="lines">Current lines</param>
        public void GetScore(string mode, string session, out int score, out int lines)
        {
            mode += ModeField + '_' + session;
            score = PlayerPrefs.GetInt(mode + ScoreField);
            lines = PlayerPrefs.GetInt(mode + LinesField);
        }

        /// <summary>
        /// Get score of the mode
        /// </summary>
        /// <param name="mode">Game mode</param>
        /// <param name="score">Current score</param>
        /// <param name="lines">Current lines</param>
        public void GetScore(string mode, out int score, out int lines)
        {
            mode += ModeField;
            score = PlayerPrefs.GetInt(mode + ScoreField);
            lines = PlayerPrefs.GetInt(mode + LinesField);
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