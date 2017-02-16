// <copyright file="NextFigureInspector.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/16/2017 19:51</date>

using Assets.BrickGame.Scripts.Playground;
using UnityEditor;

namespace BrickGame.Editor
{
    /// <summary>
    /// NextFigureInspector
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(NextFigureGroup), true)]
    public class NextFigureInspector : BasePlaygroundInspector
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private SerializedProperty _prefab;
        private SerializedProperty _content;
        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected override void ConcreteOnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_prefab);
            EditorGUILayout.PropertyField(_content);
        }

        /// <inheritdoc />
        protected override void UpdateInstance(int width, int height)
        {
            /*Component component = (Component) serializedObject.targetObject;
            if (component.GetComponent<PlaygroundBehaviour>() == null) return;
            component.GetComponent<PlaygroundBehaviour>().UpdateModel(
                new PlaygroundModel(width, height));*/
            NextFigureGroup @group = (NextFigureGroup) serializedObject.targetObject;
            group.Start();
        }

        protected override void ConcreteOnEnable()
        {
            _prefab = serializedObject.FindProperty("_brickPrefab");
            _content = serializedObject.FindProperty("_content");
        }
    }
}