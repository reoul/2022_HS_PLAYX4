using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloor : Singleton<PlayerFloor>
{

    public enum Floor { Left = 0, Center, Right }
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
        IsRayHit();
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            SetTagetFloor(Floor.Left);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            SetTagetFloor(Floor.Right);
        }
        else if (Input.GetKey(KeyCode.DownArrow)){
            SetTagetFloor(Floor.Center);
        }
    }

    private void IsRayHit()
    {
        RaycastHit hit;
        float distance = 10f;
        int layerMask = 1 << LayerMask.NameToLayer("PlayerFloor");
        if (Physics.Raycast(_camera.position,_camera.position - new Vector3(0,10,0), out hit, distance,layerMask))
        {
            hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 0.4f, 0);
        }
    }

    public void SetTagetFloor(Floor floor)  
    {
        floorTransforms[(int)floor].GetComponent<MeshRenderer>().material.color = new Color(0.7f, 0, 0);
    }
}
