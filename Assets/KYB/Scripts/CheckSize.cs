using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            GetComponent<DissolveMat>().StartCreateDissolve();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<DissolveMat>().StartDestroyDissolve();
        }
        
    }
}
