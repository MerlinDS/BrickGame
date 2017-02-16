using UnityEditor;

namespace BrickGame.Editor
{
    /// <summary>
    /// Base class for playground inspectors
    /// </summary>
    public abstract class BasePlaygroundInspector : UnityEditor.Editor
    {
        private SerializedProperty _width;
        private SerializedProperty _height;

        private void OnEnable()
        {
            _width = serializedObject.FindProperty("Width");
            _height = serializedObject.FindProperty("Height");
            ConcreteOnEnable();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_width);
            EditorGUILayout.PropertyField(_height);
            bool changes = EditorGUI.EndChangeCheck();
            ConcreteOnInspectorGUI();
            serializedObject.ApplyModifiedProperties();
            if(changes && _width.intValue > 0 && _height.intValue > 0)
                UpdateInstance(_width.intValue, _height.intValue);
        }

        // ReSharper disable once InconsistentNaming
        protected virtual void ConcreteOnInspectorGUI()
        {

        }

        protected virtual void ConcreteOnEnable()
        {

        }

        protected abstract void UpdateInstance(int width, int height);
    }
}