// <copyright file="PlaygroundInspector.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/07/2017 16:52</date>

using BrickGame.Scripts.Playground;
using UnityEditor;

namespace BrickGame.Editor
{
    /// <summary>
    /// PlaygroundInspector 
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PlaygroundController))]
    public class PlaygroundInspector : UnityEditor.Editor
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private SerializedProperty _width;
        private SerializedProperty _height;
        private SerializedProperty _rules;

        //================================      Public methods      =================================
        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_width);
            EditorGUILayout.PropertyField(_height);
            bool changes = EditorGUI.EndChangeCheck();
            EditorGUILayout.PropertyField(_rules);
            serializedObject.ApplyModifiedProperties();
            if(changes){
                PlaygroundController behaviour = (PlaygroundController) serializedObject.targetObject;
                if(behaviour.Width > 0 && behaviour.Height > 0)behaviour.Start();
            }
        }
        //================================ Private|Protected methods ================================
        private void OnEnable()
        {
            _width = serializedObject.FindProperty("Width");
            _height = serializedObject.FindProperty("Height");
            _rules = serializedObject.FindProperty("Rules");
        }
    }
}