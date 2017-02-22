// <copyright file="CacheModel.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 20:14</date>

using System.Text;
using MiniMoca;
using UnityEngine;

namespace BrickGame.Scripts.Models
{
    /// <summary>
    /// CacheModel - proxy access to the application's cache
    /// </summary>
    public class CacheModel : IMocaActor
    {
        private const string ModePostfix = "_Mode";
        private const string ScorePostfix = "_Score";
        private const string LinesPostfix = "_Lines";
        private const string LevelPostfix = "_Level";
        private const string PlaygroundPostfix = "_Playground";
        private const string AudioField = "AudioMuted";
        private const string ColorField = "ColorPaletteIndex";
        private const string ModeIndexField = "ModeIndex";

        //================================       Public Setup       =================================
        public bool AudioMuted
        {
            get { return PlayerPrefs.GetInt(AudioField) > 0; }
            set { PlayerPrefs.SetInt(AudioField, value ? 1 : 0); }
        }

        public int ColorPaletteIndex
        {
            get { return PlayerPrefs.GetInt(ColorField); }
            set { PlayerPrefs.SetInt(ColorField, value); }
        }

        public int ModeIndex
        {
            get { return PlayerPrefs.GetInt(ModeIndexField); }
            set{ PlayerPrefs.SetInt(ModeIndexField, value);}
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
        /// UpdateColors score of spesified mode in cache.
        /// </summary>
        /// <param name="mode">Game mode</param>
        /// <param name="session">Name of the session</param>
        /// <param name="score">Current score</param>
        /// <param name="lines">Current lines</param>
        /// <param name="level"></param>
        public void UpdateScore(string mode, string session, int score, int lines, int level)
        {
            StringBuilder sb = new StringBuilder(mode);
            sb.Append(ModePostfix);
            //Get previous score
            int maxScore = PlayerPrefs.GetInt(sb + ScorePostfix);
            if (score > maxScore) PlayerPrefs.SetInt(sb + ScorePostfix, score);
            //UpdateColors session
            sb.Append('_');
            sb.Append(session);
            PlayerPrefs.SetInt(sb + ScorePostfix, score);
            PlayerPrefs.SetInt(sb + LinesPostfix, lines);
            PlayerPrefs.SetInt(sb + LevelPostfix, level);
        }

        /// <summary>
        /// Get score of the sesssion
        /// </summary>
        /// <param name="mode">Game mode</param>
        /// <param name="session">Name of the session</param>
        /// <param name="score">Current score</param>
        /// <param name="lines">Current lines</param>
        /// <param name="level">Current level</param>
        public void GetScore(string mode, string session, out int score, out int lines, out int level)
        {
            mode += ModePostfix + '_' + session;
            score = PlayerPrefs.GetInt(mode + ScorePostfix);
            lines = PlayerPrefs.GetInt(mode + LinesPostfix);
            level = PlayerPrefs.GetInt(mode + LevelPostfix);
        }

        public int GetMaxSocre(string mode)
        {
            return PlayerPrefs.GetInt(mode + ModePostfix + ScorePostfix);
        }

        /// <summary>
        /// Get score of the mode
        /// </summary>
        /// <param name="mode">Game mode</param>
        /// <param name="score">Current score</param>
        /// <param name="lines">Current lines</param>
        public void GetScore(string mode, out int score, out int lines)
        {
            mode += ModePostfix;
            score = PlayerPrefs.GetInt(mode + ScorePostfix);
            lines = PlayerPrefs.GetInt(mode + LinesPostfix);
        }

        /// <summary>
        /// Clean cache of the playground for spesified mode in cache.
        /// </summary>
        /// <param name="mode">Game mode of the playground</param>
        /// <param name="session"></param>
        public void CleanPlayground(string mode, string session)
        {
            PlayerPrefs.DeleteKey(mode + ModePostfix + session + PlaygroundPostfix);
        }

        /// <summary>
        /// UpdateColors playground cache for speified mode
        /// </summary>
        /// <param name="mode">Name of the mode</param>
        /// <param name="session"></param>
        /// <param name="data">Playground cache</param>
        public void UpdatePlayground(string mode, string session, string data)
        {
            PlayerPrefs.SetString(mode + ModePostfix + session + PlaygroundPostfix, data);
        }

        /// <summary>
        /// Get playground cache conpressed in string
        /// </summary>
        /// <param name="mode">Game mode of the playground</param>
        /// <param name="session"></param>
        /// <returns>Playground cache</returns>
        public string GetPlaygroundCache(string mode, string session)
        {
            return PlayerPrefs.GetString(mode + ModePostfix + session + PlaygroundPostfix);
        }

        //================================ Private|Protected methods ================================
    }
}