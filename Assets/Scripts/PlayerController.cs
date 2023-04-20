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

    [SerializeField]
    GameObject killer;

    GameObject equippedObject;

    GameObject itemSlot;

    GameObject myCamera;

    public bool isCrouching;

    Health myHealth;

    ParticleSystem myParticles;

    bool dead;

    float healthRestoreTimer;


    // Start is called before the first frame update
    void Start()
    {
        itemSlot = this.transform.GetChild(2).gameObject;
        myCamera = this.transform.GetChild(1).gameObject;
        myFootsteps = this.gameObject.GetComponent<Footsteps>();
        myHealth = this.gameObject.GetComponent<Health>();
        myParticles = this.gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");
        mouseX = Input.GetAxis("Mouse X");

        if (dead == false)
        {
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
                    if (interactibleObject.tag == "Phone")
                    {
                        if (interactibleObject.GetComponent<Phone>().answered == false && interactibleObject.GetComponent<Phone>().isRinging == true)
                        {
                            interactibleObject.SendMessage("PickUp");
                        }

                    }
                }

            }

            //item use
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (this.gameObject.GetComponent<Collider>().bounds.Intersects(killer.GetComponent<Collider>().bounds))
                {
                    if (equippedObject != null && equippedObject.tag == "Weapon")
                    {
                        equippedObject.GetComponent<Weapon>().Attack(killer);
                    }
                }
                if (equippedObject != null && equippedObject.tag == "Tool")
                {
                    equippedObject.GetComponent<Tool>().Use();
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

            //blood effect plays when health is low
            if (myHealth.healthPoints <= myHealth.maxHealth / 2)
            {
                myParticles.Play();

                if (Vector3.Distance(this.transform.position, killer.transform.position) > 5 && isCrouching == true)
                {
                    healthRestoreTimer += Time.deltaTime;
                }
            }

            if (healthRestoreTimer > 2)
            {
                myHealth.RestoreHealth(2);
                healthRestoreTimer = 0;
            }

            if (myHealth.healthPoints > myHealth.maxHealth / 2 && myParticles.isPlaying)
            {
                myParticles.Stop();
            }

            //end game if health is empty
            if (myHealth.healthPoints <= 0 && dead == false)
            {
                Die();
            }

            if (equippedObject != null && GameManager.gmInstance.playerHasFlashlight == false && equippedObject.tag == "Tool")
            {
                GameManager.gmInstance.playerHasFlashlight = true;
            }

            if (equippedObject != null && GameManager.gmInstance.playerHasKnife == false && equippedObject.tag == "Weapon")
            {
                GameManager.gmInstance.playerHasKnife = true;
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
        if (other.gameObject.tag == "Door" || other.gameObject.tag == "Weapon" || other.gameObject.tag == "Tool" || other.gameObject.tag == "Phone")
        {
            interactibleObject = other.gameObject;
        }
    }

    void Attack(GameObject target)
    {
        target.GetComponent<Health>().TakeDamage(damage);
    }

    void Die()
    {
        if (equippedObject != null)
        {
            Destroy(equippedObject);
        }
        dead = true;
        transform.Rotate(new Vector3(-90, 0, 0));
        transform.Translate(new Vector3(0, -2, 0), Space.Self);
        GameManager.gmInstance.EndGame(false);
    }
}
