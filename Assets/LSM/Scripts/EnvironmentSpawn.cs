using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawn : MonoBehaviour
{

    private float _envDelay;

    private int _rockRange;

    private bool _spawnCheck = false;
    // Start is called before the first frame update
    void Start()
    {

        RockSpawn();
    }

    void Update()
    {

        RockSpawn();
        Debug.Log(_envDelay);
    }




    private void RockSpawn()
    {
        float _delayCheck = Random.Range(1, 4);
        _envDelay += Time.deltaTime;
        if (_envDelay >= _delayCheck && _spawnCheck == false)
        {
            _rockRange = Random.Range(1, 28);
            GameObject _newRock;
            _newRock = Resources.Load("Rock/rock_" + _rockRange) as GameObject;
            Instantiate(_newRock, this.transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySound("Env2", 0.1f);
            _spawnCheck = true;
        }
    }

    //private void TreeSpawn()
    //{
    //    float _delayCheck = Random.Range(1, 4);
    //    _envDelay += Time.deltaTime;
    //    if (_envDelay >= _delayCheck && _spawnCheck == false)
    //    {
    //        _rockRange = Random.Range(2, 5);
    //        GameObject _newRock;
    //        _newRock = Resources.Load("Tree/Tree9_" + _rockRange) as GameObject;
    //        Instantiate(_newRock, this.transform.position, Quaternion.identity);
    //        SoundManager.Instance.PlaySound("Env2", 0.1f);
    //    }
    //}
}
