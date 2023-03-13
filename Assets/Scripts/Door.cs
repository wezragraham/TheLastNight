using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject myHinge;
    bool open;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenOrClose()
    {
        if (open == false)
        {
            transform.RotateAround(myHinge.transform.position, Vector3.up, -50);
            open = true;
        }
        else if (open == true)
        {
            transform.RotateAround(myHinge.transform.position, Vector3.up, 50);
            open = false;
        }

    }
}
