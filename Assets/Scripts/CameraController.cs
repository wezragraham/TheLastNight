using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float mouseY;

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
    }
}
