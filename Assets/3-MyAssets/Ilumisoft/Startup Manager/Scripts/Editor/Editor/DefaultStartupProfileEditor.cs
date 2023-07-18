namespace Ilumisoft.StartupManager.Editor
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(DefaultStartupProfile))]
    [CanEditMultipleObjects]
    public class DefaultStartupProfileEditor : Editor
    {
        private const string InfoMessage = "All added prefabs are automatically instantiated and marked as DontDestroyOnLoad when the application is started.";

        private readonly Scrollview scrollview = new();

        private PrefabList prefabList;

        private void OnEnable()
        {
            prefabList = new PrefabList(serializedObject, serializedObject.FindProperty("Prefabs"));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            scrollview.OnGUILayout(() =>
            {
                OnHelpBoxGUI();
                OnListGUI();
            });

            serializedObject.ApplyModifiedProperties();
        }

        private static void OnHelpBoxGUI()
        {
            GUILayout.Space(8);

            EditorGUILayout.HelpBox(InfoMessage, MessageType.Info, true);
        }

        private void OnListGUI()
        {
            GUILayout.Space(12);

            prefabList.OnGuiLayout();

            GUILayout.Space(20);
        }
    }
}