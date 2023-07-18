using UnityEditor;

namespace Ilumisoft.StartupManager.Editor
{
    [CustomEditor(typeof(StartupConfiguration))]
    [CanEditMultipleObjects]
    public class StartupConfigurationEditor : UnityEditor.Editor
    {
        SerializedProperty startupProfileProperty;

        UnityEditor.Editor startupProfileEditor = null;

        void OnEnable()
        {
            startupProfileProperty = serializedObject.FindProperty("startupProfile");
        }

        private void OnDisable()
        {
            if (startupProfileEditor != null)
            {
                DestroyImmediate(startupProfileEditor);
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(startupProfileProperty);
            serializedObject.ApplyModifiedProperties();

            var startupProfile = startupProfileProperty.objectReferenceValue;

            if (startupProfileEditor == null && startupProfile != null)
            {
                startupProfileEditor = CreateEditor(startupProfile);
            }
            else if(startupProfileEditor != null && startupProfile != startupProfileEditor.target)
            {
                DestroyImmediate(startupProfileEditor);
                startupProfileEditor = CreateEditor(startupProfile);
            }

            if(startupProfileEditor != null)
            {
                startupProfileEditor.OnInspectorGUI();
            }
        }
    }
}