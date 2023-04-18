using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float mouseY;
    Vector3 angles;

    [SerializeField]
    private float rotationSpeedModifier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.right * -mouseY * rotationSpeedModifier * Time.deltaTime);
        angles = transform.rotation.eulerAngles;

        if (angles.x < 320 && angles.x > 270)
        {
            transform.rotation = Quaternion.Euler(320.0f, angles.y, angles.z);
        }
        if (angles.x > 40 && angles.x < 90)
        {
            transform.rotation = Quaternion.Euler(40.0f, angles.y, angles.z);
        }
    }
}
