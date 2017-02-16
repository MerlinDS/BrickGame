// <copyright file="ColorPalleteManagerInspector.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/16/2017 18:23</date>

using BrickGame.Scripts.Controllers;
using UnityEditor;

namespace BrickGame.Editor
{
    /// <summary>
    /// ColorPalleteManagerInspector
    /// </summary>
    [CustomEditor(typeof(ColorPalleteManager))]
    public class ColorPalleteManagerInspector : UnityEditor.Editor
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private SerializedProperty _color0;
        private SerializedProperty _color1;
        private SerializedProperty _color2;

        //================================      Public methods      =================================
        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_color0);
            EditorGUILayout.PropertyField(_color1);
            EditorGUILayout.PropertyField(_color2);
            bool changes = EditorGUI.EndChangeCheck();
            serializedObject.ApplyModifiedProperties();
            if (changes)
            {
                ColorPalleteManager manager = (ColorPalleteManager) serializedObject.targetObject;
                manager.UpdateColors();
            }
        }

        //================================ Private|Protected methods ================================
        private void OnEnable()
        {
            _color0 = serializedObject.FindProperty("Background");
            _color1 = serializedObject.FindProperty("Foreground");
            _color2 = serializedObject.FindProperty("Main");
        }
    }
}