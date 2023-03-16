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

    GameObject equippedObject;

    GameObject itemSlot;

    GameObject myCamera;

    bool isCrouching;

    // Start is called before the first frame update
    void Start()
    {
        itemSlot = this.transform.GetChild(2).gameObject;
        myCamera = this.transform.GetChild(1).gameObject;
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

        //object interaction stuff - might move to another script
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //door opening, closing
            if (this.gameObject.GetComponent<Collider>().bounds.Intersects(interactibleObject.GetComponent<Collider>().bounds))
            {
                if (interactibleObject.tag == "Door")
                {
                    interactibleObject.SendMessage("OpenOrClose");
                }

                if (interactibleObject.tag == "Weapon" || interactibleObject.tag == "Tool")
                {
                    if (equippedObject != null)
                    {
                        equippedObject.transform.parent = null;
                        equippedObject.transform.position = interactibleObject.transform.position;
                        equippedObject.transform.rotation = interactibleObject.transform.rotation;
                    }

                    interactibleObject.transform.position = itemSlot.transform.position;
                    interactibleObject.transform.rotation = itemSlot.transform.rotation;
                    interactibleObject.transform.parent = itemSlot.transform;
                    equippedObject = interactibleObject;

                }
            }

        }

        //crouching stuff
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isCrouching)
            {
                isCrouching = true;
                myCamera.transform.position = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y - 0.1f, myCamera.transform.position.z);
                itemSlot.transform.position = new Vector3(itemSlot.transform.position.x, itemSlot.transform.position.y - 0.1f, itemSlot.transform.position.z);
                movementSpeedMultiplier = movementSpeedMultiplier / 2;
            }
            else if (isCrouching)
            {
                isCrouching = false;
                myCamera.transform.position = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y + 0.1f, myCamera.transform.position.z);
                itemSlot.transform.position = new Vector3(itemSlot.transform.position.x, itemSlot.transform.position.y + 0.1f, itemSlot.transform.position.z);
                movementSpeedMultiplier = movementSpeedMultiplier * 2;
            }
        }


    }

    //object stuff
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door" || other.gameObject.tag == "Weapon" || other.gameObject.tag == "Tool")
        {
            interactibleObject = other.gameObject;
        }
    }
}
