using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float vInput;
    private float hInput;
    private float mouseX;

    Footsteps myFootsteps;
    float stepTimer;

    [SerializeField]
    private float movementSpeedMultiplier;

    [SerializeField]
    private float rotationSpeedModifier;

    [SerializeField]
    int damage;

    GameObject interactibleObject;

    GameObject killer;

    GameObject equippedObject;

    GameObject itemSlot;

    GameObject myCamera;

    public bool isCrouching;



    // Start is called before the first frame update
    void Start()
    {
        itemSlot = this.transform.GetChild(2).gameObject;
        myCamera = this.transform.GetChild(1).gameObject;
        myFootsteps = this.gameObject.GetComponent<Footsteps>();
        killer = GameObject.FindGameObjectWithTag("Killer");
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && interactibleObject != null)
        {
            
            if (this.gameObject.GetComponent<Collider>().bounds.Intersects(interactibleObject.GetComponent<Collider>().bounds))
            {
                //door opening, closing
                if (interactibleObject.tag == "Door")
                {
                    interactibleObject.SendMessage("OpenOrClose");
                }
                //picking up items
                if (interactibleObject.tag == "Weapon" || interactibleObject.tag == "Tool")
                {
                    PickUpItem();

                }
            }

        }

        //crouching stuff
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isCrouching)
            {
                isCrouching = true;
                myCamera.transform.position = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y - 0.3f, myCamera.transform.position.z);
                itemSlot.transform.position = new Vector3(itemSlot.transform.position.x, itemSlot.transform.position.y - 0.3f, itemSlot.transform.position.z);
                movementSpeedMultiplier = movementSpeedMultiplier / 2;
            }
            else if (isCrouching)
            {
                isCrouching = false;
                myCamera.transform.position = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y + 0.3f, myCamera.transform.position.z);
                itemSlot.transform.position = new Vector3(itemSlot.transform.position.x, itemSlot.transform.position.y + 0.3f, itemSlot.transform.position.z);
                movementSpeedMultiplier = movementSpeedMultiplier * 2;
            }
        }

        //attacking
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (this.gameObject.GetComponent<Collider>().bounds.Intersects(interactibleObject.GetComponent<Collider>().bounds))
            {
                if (equippedObject.tag == "Weapon")
                {
                    equippedObject.GetComponent<Weapon>().Attack(killer);
                    Debug.Log(killer.GetComponent<Health>().healthPoints);
                }
                else
                {
                    Attack(killer);
                }
            }

        }

        //footsteps for enemy tracking
        if (!isCrouching)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= 5)
            {
                myFootsteps.TakeStep();
                stepTimer = 0;
            }
        }


    }

    void PickUpItem()
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

    //object stuff
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door" || other.gameObject.tag == "Weapon" || other.gameObject.tag == "Tool")
        {
            interactibleObject = other.gameObject;
        }
    }

    void Attack(GameObject target)
    {
        target.GetComponent<Health>().TakeDamage(damage);
    }
}
