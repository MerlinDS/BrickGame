// <copyright file="PlaygroundInspector.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/07/2017 16:52</date>

using BrickGame.Scripts.Playground;
using UnityEditor;
using UnityEngine;

namespace BrickGame.Editor
{
    /// <summary>
    /// PlaygroundInspector 
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PlaygroundController), true)]
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
                Component component = (Component) serializedObject.targetObject;
                if (_width.intValue > 0 && _height.intValue > 0)
                {
                    if (component.GetComponent<PlaygroundBehaviour>() == null) return;
                    component.GetComponent<PlaygroundBehaviour>().UpdateModel(
                        new PlaygroundModel(_width.intValue, _height.intValue));
                }
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