using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animTest : MonoBehaviour
{
    public Animation DAnim;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            DAnim.Play("fire_roar");
        if (Input.GetKeyDown(KeyCode.Space))
            DAnim.Play("flying_idle");
       
    }
}
