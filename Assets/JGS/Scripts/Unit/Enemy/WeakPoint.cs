using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    private Transform _target;
    private Golem _parant;

    private void Start()
    {
        _parant = transform.GetComponentInParent<Golem>();
        _target = GameObject.Find("Camera").transform;
    }

    private void Update()
    {
        transform.LookAt(_target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Arrow"))
        {
            _parant.ChangeWeakPoint();
            ScoreSystem.Score += 100;
        }
    }
}
