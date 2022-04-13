using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObj : MonoBehaviour
{
    public Transform Target;
    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(Target.transform.position, this.transform.position, 0.1f);
    }
}
