using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class test : MonoBehaviour
{
    Mesh mesh;
    Vector3[] verts;
    Vector3 vertPos;
    GameObject[] handles;

    void OnEnable()
    {
        GameObject[] handles = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject handle in handles)
        {
            Destroy(handle);    
        }
        mesh = GetComponent<MeshFilter>().mesh;
        verts = mesh.vertices;
        foreach(Vector3 vert in verts)
        {
            vertPos = transform.TransformPoint(vert);
            GameObject handle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            handle.transform.localScale = Vector3.one * 0.1f;
            handle.transform.position = vertPos;
            handle.transform.parent = transform;
            handle.tag = "Player";
            //handle.AddComponent<Gizmo_Sphere>();
            print(vertPos);
        }
        print(verts.Length);
    }
     
    void OnDisable()
    {
        GameObject[] handles = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject handle in handles)
        {
            DestroyImmediate(handle);    
        }
    }
     
    void Update()
    {
        handles = GameObject.FindGameObjectsWithTag ("Player");
        for(int i = 0; i < verts.Length; i++)
        {
            verts[i] = handles[i].transform.localPosition;    
        }
        mesh.vertices = verts;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}
