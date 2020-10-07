using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Vehicle))]
public class VehicleEditor : Editor
{
    private bool showPosition = true;
    string status = "VehicleData";

    public override void OnInspectorGUI()
    {
        showPosition = EditorGUILayout.BeginFoldoutHeaderGroup(showPosition, status);

        if (showPosition)
        {
            if (Application.isPlaying)
            {
                Type t = typeof(VehicleData);
                PropertyInfo[] properties = t.GetProperties(
                    BindingFlags.NonPublic | // Include protected and private properties
                    BindingFlags.Public |    // Also include public properties
                    BindingFlags.Instance    // Specify to retrieve non static properties
                );

                FieldInfo[] fields = t.GetFields(
                    BindingFlags.NonPublic | // Include protected and private properties
                    BindingFlags.Public |    // Also include public properties
                    BindingFlags.Instance    // Specify to retrieve non static properties
                );

                foreach (var item in properties)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label($"{item.Name}");
                    GUILayout.Label($"{item.GetValue(FindObjectOfType<Vehicle>().VehicleDataProvider.Data, null)}", new GUIStyle { alignment = TextAnchor.MiddleRight });
                    EditorGUILayout.EndHorizontal();
                }

                foreach (var item in fields)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label($"{item.Name}");
                    GUILayout.Label($"{item.GetValue(FindObjectOfType<Vehicle>().VehicleDataProvider.Data)}", new GUIStyle { alignment = TextAnchor.MiddleRight });
                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                EditorGUILayout.LabelField($"Start the game to reveal data.");
            }
        }

        EditorGUILayout.EndFoldoutHeaderGroup();
        
        base.OnInspectorGUI();
    }
}
