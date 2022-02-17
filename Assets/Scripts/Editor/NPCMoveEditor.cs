using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPCMoveAction))]
public class NPCMoveEditor : Editor
{
    SerializedProperty s_delay, s_targetPosition;

    private void OnEnable()
    {
        s_delay = serializedObject.FindProperty("delay");
        s_targetPosition = serializedObject.FindProperty("targetPosition");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUILayout.BeginVertical("box");

        GUILayout.BeginHorizontal();

        EditorGUILayout.PropertyField(s_delay, new GUIContent("Action Delay:"));
        EditorGUILayout.LabelField("(in sec)");

        GUILayout.EndHorizontal();

        EditorGUILayout.Vector3Field("Target Position: ", s_targetPosition.vector3Value);

        GUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        serializedObject.Update();

        s_targetPosition.vector3Value = Handles.PositionHandle(s_targetPosition.vector3Value, Quaternion.identity);
        Handles.Label(s_targetPosition.vector3Value, "NPC Target Position");

        serializedObject.ApplyModifiedProperties();
    }
}
