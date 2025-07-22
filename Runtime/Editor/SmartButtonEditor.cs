using UnityEditor;

#if UNITY_EDITOR
namespace com.yak.ui
{
    [CustomEditor(typeof(SmartButton), true)]
    [CanEditMultipleObjects]
    public class SmartButtonEditor : Editor
    {
        private SerializedProperty _interactableProp;
        private SerializedProperty _navigationProp;
        private SerializedProperty _onClickProp;

        void OnEnable()
        {
            _interactableProp = serializedObject.FindProperty("m_Interactable");
            _navigationProp = serializedObject.FindProperty("m_Navigation");
            _onClickProp = serializedObject.FindProperty("m_OnClick");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_interactableProp);
            EditorGUILayout.PropertyField(_navigationProp);
            EditorGUILayout.PropertyField(_onClickProp);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif