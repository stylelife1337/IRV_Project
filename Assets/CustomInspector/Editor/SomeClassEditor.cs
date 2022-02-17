using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SomeClass))]
public class SomeClassEditor : Editor
{
    SomeClass source;
    SerializedProperty playerName, speed, playerPosition, playerPrefabs;

    private void OnEnable()
    {
        source = (SomeClass)target;
        playerName = serializedObject.FindProperty("s_playerName");
        speed = serializedObject.FindProperty("s_speed");
        playerPosition = serializedObject.FindProperty("s_playerPosition");
        playerPrefabs = serializedObject.FindProperty("s_playerPrefabs");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        GUILayout.BeginVertical("box");
        source.playerName = EditorGUILayout.TextField("Player Name: ",source.playerName);
        source.speed = EditorGUILayout.FloatField(source.speed);
        source.playerPosition = EditorGUILayout.Vector3Field("Player Position: ",source.playerPosition);
        source.playerPrefabs = (GameObject)EditorGUILayout.ObjectField(source.playerPrefabs, typeof(GameObject), true);
        GUILayout.EndVertical();

        GUILayout.BeginVertical("box");
        EditorGUILayout.PropertyField(playerName, new GUIContent("Player Name: "));
        EditorGUILayout.PropertyField(speed, new GUIContent("Player Speed: "));
        EditorGUILayout.PropertyField(playerPosition, new GUIContent("Player Position: "));
        EditorGUILayout.PropertyField(playerPrefabs, new GUIContent("Player Prefabs: "));
        GUILayout.EndVertical();

        if (GUILayout.Button("Randomize Speed"))
        {
            speed.floatValue = Random.Range(5f, 25f);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
