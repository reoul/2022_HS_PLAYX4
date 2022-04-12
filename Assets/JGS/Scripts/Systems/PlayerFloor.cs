using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloor : Singleton<PlayerFloor>
{

    public enum Floor { Left = 0, Center, Right }
    public Transform[] floorTransforms;

    private Color _floorDefaultColor;
    private Transform _camera;

    private int _playerFloor = 1;
    private Vector3 _measureStartPos;
    private float _measureWidth;

    private bool[] _isAttack;

    private void Start()
    {
        floorTransforms = new Transform[3];
        for (int i = 0; i < 3; i++)
        {
            floorTransforms[i] = transform.GetChild(i);
        }
        _floorDefaultColor = floorTransforms[0].GetComponent<MeshRenderer>().material.color;
        _camera = GameObject.Find("Camera").transform;


        _isAttack = new bool[3];
    }

    private void Update()
    {
        foreach(Transform obj in floorTransforms)
        {
            //obj.GetComponent<MeshRenderer>().material.color = _floorDefaultColor;
        }
        IsRayHit();
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartMeasure();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            StopMeasure();
        }
    }

    private void AAAAA()
    {
        List<int> target = new List<int>();
        target.Add(0);
        target.Add(1);
        target.Add(2);
        target = Utility.ShuffleList(target);
        Debug.Log($"{target[0].ToString()} {target[1].ToString()} {target[2].ToString()}");
        if (_playerFloor == target[0])
        {
            ScoreSystem.Score -= 100;
        }
    }

    private void IsRayHit()
    {
        RaycastHit hit;
        float distance = 10f;
        int layerMask = 1 << LayerMask.NameToLayer("PlayerFloor");
        if (Physics.Raycast(_camera.position,_camera.position - new Vector3(0,10,0), out hit, distance,layerMask))
        {
            for (int i = 0; i < floorTransforms.Length; i++)
            {
                if (hit.collider.transform == floorTransforms[i])
                {
                    _playerFloor = i;
                }
            }
            //hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0, 0.4f, 0);
        }
    }

    public void SetTagetFloor(Floor floor)  
    {
        floorTransforms[(int)floor].GetComponent<MeshRenderer>().material.color = new Color(0.7f, 0, 0);
    }

    public void StartMeasure()
    {
        _measureStartPos = _camera.position;
        foreach (Transform transform in floorTransforms)
        {
            transform.position = floorTransforms[(int)Floor.Center].position;
        }
    }

    public void StopMeasure()
    {
        Vector3 _measureEndPos;
        _measureEndPos = _camera.position;
        _measureWidth = Vector2.Distance(new Vector2(_measureEndPos.x,_measureEndPos.z), new Vector2(_measureStartPos.x, _measureStartPos.z));
        floorTransforms[(int)Floor.Left].position -= new Vector3(_measureWidth, 0,0);
        floorTransforms[(int)Floor.Right].position += new Vector3(_measureWidth, 0,0);
        floorTransforms[(int)Floor.Left].localScale = new Vector3(_measureWidth, 0.1f, 3.68f);
        floorTransforms[(int)Floor.Center].localScale = new Vector3(_measureWidth, 0.1f, 3.68f);
        floorTransforms[(int)Floor.Right].localScale = new Vector3(_measureWidth, 0.1f, 3.68f);
    }

    public IEnumerator Hit(int cnt)
    {
        for(int i = 0; i < cnt; i++)
        {
            float tmp = 0.2f;
            int count = 1;
            int _floor = Random.Range(0, 3);
            while (tmp > 0)
            {
                if (count++ % 2 != 0)
                {
                    floorTransforms[_floor].GetComponent<MeshRenderer>().material.color = new Color(0.7f, 0, 0);
                }
                else
                {
                    floorTransforms[_floor].GetComponent<MeshRenderer>().material.color = _floorDefaultColor;
                }
                if(tmp > 0.01)
                {
                    tmp -= 0.01f;
                }
                yield return new WaitForSeconds(tmp);
            }
            floorTransforms[_floor].GetComponent<MeshRenderer>().material.color = _floorDefaultColor;

            if (_playerFloor == _floor)
            {
                ScoreSystem.Score -= 100;
            }
        }

        yield return null;
    }


    public IEnumerator StartAttack(int _floor)
    {
        if (!_isAttack[_floor])
        {
            _isAttack[_floor] = true;
            float tmp = 0.2f;
            int count = 1;
            while (_isAttack[_floor])
            {
                if (count++ % 2 != 0)
                {
                    floorTransforms[_floor].GetComponent<MeshRenderer>().material.color = new Color(0.7f, 0, 0);
                }
                else
                {
                    floorTransforms[_floor].GetComponent<MeshRenderer>().material.color = _floorDefaultColor;
                }
                tmp -= 0.01f;
                yield return new WaitForSeconds(tmp);
            }
            floorTransforms[_floor].GetComponent<MeshRenderer>().material.color = _floorDefaultColor;

            if (_playerFloor == _floor)
            {
                ScoreSystem.Score -= 100;
            }
        }
        yield return null;
    }

    public void StopAttack(int _floor)
    {
        if (_isAttack[_floor])
        {
            _isAttack[_floor] = false;
        }
    }
}
