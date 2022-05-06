using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloor : Singleton<PlayerFloor>
{

    public enum FloorType { Left = 0, Center, Right }
    public Transform[] floorTransforms;
    public Transform[] attackTrans;

    private Color _floorDefaultColor;
    private Color _warningColor = new Color(0.7f, 0, 0, 0.8f);
    private Transform _camera;

    private int _playerFloor = 1;
    public int PlayerCurFloor => _playerFloor;
    private Vector3 _measureStartPos;
    private float _measureWidth;

    private bool[] _isAttack;

    private void Start()
    {
        _measureWidth = DataManager.Instance.Data.FloorWidth;
        transform.localScale = new Vector3(_measureWidth, transform.localScale.y, transform.localScale.z);
        _camera = Camera.main.transform;
        _isAttack = new bool[3];
    }

    private void Update()
    {
        foreach (Transform obj in floorTransforms)
        {
            //obj.GetComponent<MeshRenderer>().material.color = _floorDefaultColor;
        }
        _floorDefaultColor = new Color(0.2f, 0.2f, 0.2f, 0.8f);
        IsRayHit();
        if (Input.GetKeyDown(KeyCode.I))
        {
            InitFloors();
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
        foreach (Transform floor in floorTransforms)
        {
            floor.GetComponent<Floor>().ChangeLineColor(new Color(0.48f, 0.48f, 0.48f));
        }

        if (Physics.Raycast(_camera.position, _camera.position - new Vector3(0, 10, 0), out hit, distance, layerMask))
        {
            for (int i = 0; i < floorTransforms.Length; i++)
            {
                if (hit.collider.transform == floorTransforms[i])
                {
                    _playerFloor = i;
                }
            }
            hit.transform.gameObject.GetComponent<Floor>().ChangeLineColor(new Color(0.6f, 0.8f, 0.5f));
        }
    }

    public void SetTagetFloor(FloorType floor)
    {
        floorTransforms[(int)floor].GetComponent<SpriteRenderer>().color = _warningColor;
    }

    public void StartMeasure()
    {
        _measureStartPos = _camera.position;
        //foreach (Transform transform in floorTransforms)
        //{
        //    transform.position = floorTransforms[(int)FloorType.Center].position;
        //}
    }

    public void StopMeasure()
    {
        Vector3 _measureEndPos;
        _measureEndPos = _camera.position;
        _measureWidth = Vector2.Distance(new Vector2(_measureEndPos.x, _measureEndPos.z), new Vector2(_measureStartPos.x, _measureStartPos.z));
        transform.localScale = new Vector3(_measureWidth, transform.localScale.y, transform.localScale.z);
        //floorTransforms[(int)FloorType.Left].position -= new Vector3(_measureWidth, 0,0);
        //floorTransforms[(int)FloorType.Right].position += new Vector3(_measureWidth, 0,0);
        //floorTransforms[(int)FloorType.Left].localScale = new Vector3(_measureWidth, 0.1f, 3.68f);
        //floorTransforms[(int)FloorType.Center].localScale = new Vector3(_measureWidth, 0.1f, 3.68f);
        //floorTransforms[(int)FloorType.Right].localScale = new Vector3(_measureWidth, 0.1f, 3.68f);
    }

    public IEnumerator Hit(int cnt)
    {
        for (int i = 0; i < cnt; i++)
        {
            float tmp = 0.2f;
            int count = 1;
            int _floor = Random.Range(0, 3);
            while (tmp > 0)
            {
                if (count++ % 2 != 0)
                {
                    floorTransforms[_floor].GetComponent<SpriteRenderer>().color = _warningColor;
                }
                else
                {
                    floorTransforms[_floor].GetComponent<SpriteRenderer>().color = _floorDefaultColor;
                }
                if (tmp > 0.01)
                {
                    tmp -= 0.01f;
                }
                yield return new WaitForSeconds(tmp);
            }
            floorTransforms[_floor].GetComponent<SpriteRenderer>().color = _floorDefaultColor;

            if (_playerFloor == _floor)
            {
                ScoreSystem.Score -= 100;
            }
        }

        yield return null;
    }


    public IEnumerator StartAttack(int _floor, int damage)
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
                    floorTransforms[_floor].GetComponent<SpriteRenderer>().color = _warningColor;
                }
                else
                {
                    floorTransforms[_floor].GetComponent<SpriteRenderer>().color = _floorDefaultColor;
                }
                tmp -= 0.01f;
                yield return new WaitForSeconds(tmp);
            }
            floorTransforms[_floor].GetComponent<SpriteRenderer>().color = _floorDefaultColor;

            if (_playerFloor == _floor)
            {
                ScoreSystem.Score -= damage;
                HealthBarManager.Instance.DistractPlayerDamage();
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

    public void InitFloors()
    {
        StopAllCoroutines();
        _isAttack = new bool[3];
        foreach (Transform obj in floorTransforms)
        {
            obj.GetComponent<SpriteRenderer>().color = _floorDefaultColor;
        }
    }
}
