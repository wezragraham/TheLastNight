using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    Light myLight;
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.name == "FlashlightContainer")
        {
            myLight = transform.GetChild(0).GetComponent<Light>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()
    {
        if (this.gameObject.name == "FlashlightContainer")
        {
            if (myLight.enabled == false)
            {
                myLight.enabled = true;
            }
            else if (myLight.enabled == true)
            {
                myLight.enabled = false;
            }

        }
    }
}
