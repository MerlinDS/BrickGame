// <copyright file="MocaContextPropertyDrawer.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/02/2017 9:44</date>

using UnityEditor;
using UnityEngine;

namespace MiniMoca.Editor
{
    /// <summary>>
    /// MocaContextPropertyDrawerr
    /// </summary>>
    [CustomPropertyDrawer(typeof(MocaContext), true)]
    public class MocaContextPropertyDrawer : PropertyDrawer
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================

        //================================      Public methods      =================================
        /// <inheritdoc />
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);
        }

        //================================ Private|Protected methods ================================
    }
}