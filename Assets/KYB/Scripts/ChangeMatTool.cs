using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChangeMatTool))]
public class ChangeMatToolEditor : Editor
{
    SerializedProperty matProperty;

    private ChangeMatTool _changeMatTool = null;

    void OnEnable ()
    {
        matProperty = serializedObject.FindProperty ("Mat");
        _changeMatTool = (ChangeMatTool) target;
    }

    public override void OnInspectorGUI ()
    {
        serializedObject.Update ();
        EditorGUILayout.PropertyField(matProperty);
        if (GUILayout.Button("적용"))
        {
            _changeMatTool.Apply();
        }

        serializedObject.ApplyModifiedProperties ();
    }
}

public class ChangeMatTool : MonoBehaviour
{
    public Material Mat;

    public void Apply()
    {
        foreach (var dissolveMatAll in GetComponentsInChildren<DissolveMatAll>(true))
        {
            dissolveMatAll.ChangeMat(Mat);
        }
    }
}
