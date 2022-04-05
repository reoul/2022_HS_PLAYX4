using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloor : Singleton<PlayerFloor>
{

    public enum Floor { Left, Center, Right }
    public Transform[] floorTransforms;

    private Color _floorDefaultColor;
    private Transform _camera;

    private void Start()
    {
        floorTransforms = new Transform[3];
        for (int i = 0; i < 3; i++)
        {
            floorTransforms[i] = transform.GetChild(i);
        }
        _floorDefaultColor = floorTransforms[0].GetComponent<MeshRenderer>().material.color;
        _camera = GameObject.Find("Camera").transform;
    }

    private void Update()
    {
        foreach(Transform obj in floorTransforms)
        {
            obj.GetComponent<MeshRenderer>().material.color = _floorDefaultColor;
        }

        if (_camera.position.x < -0.5f)
        {
            floorTransforms[(int)Floor.Left].GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else if (_camera.position.x > 0.5f)
        {
            floorTransforms[(int)Floor.Right].GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            floorTransforms[(int)Floor.Center].GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

}
