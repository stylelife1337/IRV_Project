using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerSpawnPosition))]
public class PlayerSpawnEditor : Editor
{
    SerializedProperty spawnerEntries;

    private void OnEnable()
    {
        spawnerEntries = serializedObject.FindProperty("spawnEntries");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //draw the spawner entry
        DrawSpawnerEntries(spawnerEntries);

        serializedObject.ApplyModifiedProperties();
    }

    void DrawSpawnerEntries(SerializedProperty spawners)
    {
        GUILayout.BeginVertical("box");

        EditorGUILayout.LabelField("Spawner Entries:");

        if (spawners.arraySize == 0)
        {
            if (GUILayout.Button("Add Entries"))
            {
                spawners.InsertArrayElementAtIndex(spawners.arraySize);
            }
        }

        for (int i = 0; i < spawners.arraySize; i++)
        {
            GUILayout.BeginHorizontal("box");
            //draw the spawner settings
            GUILayout.BeginVertical();

            EditorGUILayout.PropertyField(spawners.GetArrayElementAtIndex(i).FindPropertyRelative("prevSceneName"));
            EditorGUILayout.PropertyField(spawners.GetArrayElementAtIndex(i).FindPropertyRelative("spawnPos"));
            EditorGUILayout.PropertyField(spawners.GetArrayElementAtIndex(i).FindPropertyRelative("spawnDir"));

            GUILayout.EndVertical();

            GUILayout.BeginVertical(GUILayout.Width(20f));

            if (GUILayout.Button("X", GUILayout.Width(20f)))
            {
                spawners.DeleteArrayElementAtIndex(i);
            }

            if (i == spawners.arraySize - 1)
            {
                if (GUILayout.Button("+", GUILayout.Width(20f)))
                {
                    spawners.InsertArrayElementAtIndex(spawners.arraySize);
                }
            }

            GUILayout.EndVertical();

            GUILayout.Space(5f);

            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();
    }

    private void OnSceneGUI()
    {
        serializedObject.Update();

        for (int i = 0; i < spawnerEntries.arraySize; i++)
        {
            spawnerEntries.GetArrayElementAtIndex(i).FindPropertyRelative("spawnPos").vector3Value =
                Handles.PositionHandle(spawnerEntries.GetArrayElementAtIndex(i).FindPropertyRelative("spawnPos").vector3Value, 
                Quaternion.LookRotation(spawnerEntries.GetArrayElementAtIndex(i).FindPropertyRelative("spawnDir").vector3Value));

            Handles.Label(spawnerEntries.GetArrayElementAtIndex(i).FindPropertyRelative("spawnPos").vector3Value + Vector3.up * 0.5f, "Spawn from: " +
                spawnerEntries.GetArrayElementAtIndex(i).FindPropertyRelative("prevSceneName").stringValue);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
