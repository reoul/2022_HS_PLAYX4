using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;


#if UNITY_EDITOR
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
        if (GUILayout.Button("랜더러 ShadowCast OFF"))
        {
            _changeMatTool.Apply2();
        }
        if (GUILayout.Button("박스 콜리더 삭제"))
        {
            _changeMatTool.Apply3();
        }
        

        serializedObject.ApplyModifiedProperties ();
    }
}
#endif

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

    public void Apply2()
    {
        foreach (var Renderer in GetComponentsInChildren<Renderer>(true))
        {
            Renderer.shadowCastingMode = ShadowCastingMode.Off;
        }
    }

    public void Apply3()
    {
        foreach (var boxCollider in GetComponentsInChildren<BoxCollider>(true))
        {
            DestroyImmediate(boxCollider);
        }
    }
}
