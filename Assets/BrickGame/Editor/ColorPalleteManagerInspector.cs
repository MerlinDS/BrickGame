// <copyright file="ColorPalleteManagerInspector.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/16/2017 18:23</date>

using BrickGame.Scripts.Controllers;
using UnityEditor;
using UnityEngine;

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
        private SerializedProperty _palettes;
        private SerializedProperty _index;

        //================================      Public methods      =================================
        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawPaletteSelector();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_color0);
            EditorGUILayout.PropertyField(_color1);
            EditorGUILayout.PropertyField(_color2);
            bool changes = EditorGUI.EndChangeCheck();
            EditorGUILayout.PropertyField(_palettes, true);
            serializedObject.ApplyModifiedProperties();
            if (changes)
            {
                ColorPalleteManager manager = (ColorPalleteManager) serializedObject.targetObject;
                manager.UpdateColors();
            }
        }

        //================================ Private|Protected methods ================================
        private void DrawPaletteSelector()
        {
            if(!_palettes.isArray || _palettes.arraySize == 0)return;
            int palleteLength = _palettes.arraySize;
            string[] options = new string[palleteLength];
            for (int i = 0; i < options.Length; i++)
            {
                var elment = _palettes.GetArrayElementAtIndex(i);
                if(elment.objectReferenceValue == null)continue;
                options[i] = elment.objectReferenceValue.name;
            }
            int index = EditorGUILayout.Popup("Current palette", _index.intValue, options);
            if (index != _index.intValue)
            {
                ColorPalleteManager manager = (ColorPalleteManager) serializedObject.targetObject;
                manager.ChangePalette(index);
            }
        }

        private void OnEnable()
        {
            _color0 = serializedObject.FindProperty("Background");
            _color1 = serializedObject.FindProperty("Foreground");
            _color2 = serializedObject.FindProperty("Main");
            _index = serializedObject.FindProperty("_index");
            _palettes = serializedObject.FindProperty("_palettes");
        }
    }
}