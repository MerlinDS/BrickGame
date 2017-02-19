// <copyright file="PlaygroundInspector.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/07/2017 16:52</date>

using BrickGame.Scripts.Bricks;
using UnityEditor;

namespace BrickGame.Editor
{
    /// <summary>
    /// PlaygroundInspector 
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PlaygroundDrawer))]
    public class PlaygroundInspector : BasePlaygroundInspector
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private SerializedProperty _figure;

        private SerializedProperty _prefab;
        private SerializedProperty _content;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected override void ConcreteOnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_prefab);
            EditorGUILayout.PropertyField(_content);
            EditorGUILayout.PropertyField(_figure);
        }

        /// <inheritdoc />
        protected override void UpdateInstance(int width, int height)
        {
            PlaygroundDrawer component = (PlaygroundDrawer) serializedObject.targetObject;
            component.UpdateSize();
        }

        protected override void ConcreteOnEnable()
        {
            _prefab = serializedObject.FindProperty("_brickPrefab");
            _content = serializedObject.FindProperty("_content");
            _figure = serializedObject.FindProperty("Figure");
        }
    }
}