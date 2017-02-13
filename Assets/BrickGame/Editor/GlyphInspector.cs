// <copyright file="GlyphInspector.cs" company="Near Fancy">
// Copyright (c) 2017 All Rights Reserved
// </copyright>
// <author>Andrew Salomatin</author>
// <date>02/08/2017 17:16</date>

using BrickGame.Scripts.Model;
using UnityEditor;
using UnityEngine;

namespace BrickGame.Editor
{
    /// <summary>
    /// GlyphInspector
    /// </summary>
    [CustomEditor(typeof(Glyph))]
    public class GlyphInspector : UnityEditor.Editor
    {
        //================================       Public Setup       =================================

        //================================    Systems properties    =================================
        private Glyph _target;

        void OnEnable()
        {
            _target = (Glyph) target;
}
        //================================      Public methods      =================================
        public override void OnInspectorGUI()
        {
//            base.OnInspectorGUI();
            serializedObject.Update();
            int w = _target.Width;
            int h = _target.Height;
            GUILayout.BeginVertical();
            for (int y = 0; y < h; ++y)
            {
                GUILayout.BeginHorizontal();
                for (int x = 0; x < w; ++x)
                    _target[x, y] = GUILayout.Toggle(_target[x, y], "");
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
            EditorUtility.SetDirty(_target);
            serializedObject.ApplyModifiedProperties();
}
        //================================ Private|Protected methods ================================
    }
}