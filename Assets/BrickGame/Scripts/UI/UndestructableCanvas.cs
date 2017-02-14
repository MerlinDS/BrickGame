// <copyright file="UndestructableCanvas.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/14/2017 18:05</date>
namespace BrickGame.Scripts.UI
{
    /// <summary>
    /// UndestructableCanvas
    /// </summary>
    public class UndestructableCanvas : GameBehaviour
    {
        private static UndestructableCanvas _instance;
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        private void Awake()
        {

            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
                Destroy(gameObject);
        }
    }
}