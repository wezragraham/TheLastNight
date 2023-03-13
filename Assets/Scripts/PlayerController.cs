using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float vInput;
    private float hInput;
    private float mouseX;


    [SerializeField]
    private float movementSpeedMultiplier;

    [SerializeField]
    private float rotationSpeedModifier;

    GameObject interactibleObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");
        mouseX = Input.GetAxis("Mouse X");


        transform.Translate(Vector3.forward * vInput * movementSpeedMultiplier * Time.deltaTime);
        transform.Translate(Vector3.right * hInput * movementSpeedMultiplier * Time.deltaTime);

        transform.Rotate(Vector3.up * mouseX * rotationSpeedModifier * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (this.gameObject.GetComponent<Collider>().bounds.Intersects(interactibleObject.GetComponent<Collider>().bounds))
            {
                if (interactibleObject.tag == "Door")
                {
                    interactibleObject.SendMessage("OpenOrClose");
                }
            }


        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            interactibleObject = other.gameObject;
        }
    }
}
