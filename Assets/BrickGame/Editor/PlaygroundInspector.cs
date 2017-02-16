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
    public class PlaygroundInspector : BasePlaygroundInspector
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private SerializedProperty _rules;

        //================================      Public methods      =================================

        //================================ Private|Protected methods ================================
        /// <inheritdoc />
        protected override void ConcreteOnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_rules);
        }

        /// <inheritdoc />
        protected override void UpdateInstance(int width, int height)
        {
            Component component = (Component) serializedObject.targetObject;
            if (component.GetComponent<PlaygroundBehaviour>() == null) return;
            component.GetComponent<PlaygroundBehaviour>().UpdateModel(
                new PlaygroundModel(width, height));
        }

        protected override void ConcreteOnEnable()
        {
            _rules = serializedObject.FindProperty("Rules");
        }
    }
}