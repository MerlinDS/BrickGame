// <copyright file="NextFigureInspector.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/16/2017 19:51</date>

using BrickGame.Scripts.UI;
using UnityEditor;

namespace BrickGame.Editor
{
    /// <summary>
    /// NextFigureInspector
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(NexFigureBehaviour), true)]
    public class NextFigureInspector : BasePlaygroundInspector
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private SerializedProperty _factory;
        private SerializedProperty _prefab;
        private SerializedProperty _content;

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected override void ConcreteOnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_factory);
            EditorGUILayout.PropertyField(_prefab);
            EditorGUILayout.PropertyField(_content);
        }

        /// <inheritdoc />
        protected override void UpdateInstance(int width, int height)
        {
            NexFigureBehaviour @group = (NexFigureBehaviour) serializedObject.targetObject;
            group.Rebuild();
        }

        protected override void ConcreteOnEnable()
        {
            _factory = serializedObject.FindProperty("Factory");
            _prefab = serializedObject.FindProperty("_brickPrefab");
            _content = serializedObject.FindProperty("_content");
        }
    }
}