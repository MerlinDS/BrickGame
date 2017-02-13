// <copyright file="AssetWizard.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/09/2017 20:26</date>

using System.IO;
using UnityEditor;
using UnityEngine;

namespace BrickGame.Editor
{
    /// <summary>
    /// AssetWizard
    /// </summary>
    public class AssetWizard : ScriptableWizard
    {
        //================================       Public Setup       =================================
        [Tooltip("Name of the asset")]
        public string Name;
        //================================    Systems properties    =================================

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        protected void CreateAsset(Object asset)
        {
            string path = GetPath();
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + Name + ".asset");
            AssetDatabase.CreateAsset(asset, assetPathAndName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
            Debug.LogFormat("Asset {0} created", Name);
        }

        private string GetPath()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(
                    Selection.activeObject)), "");
            }
            return path;
        }
    }
}