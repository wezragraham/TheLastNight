using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerMovement : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    Vector3 playerStepLocation;

    [SerializeField]
    float movementSpeedMultiplier;

    [SerializeField]
    float rotationSpeedMultiplier;

    Animator myAnimator;
    AudioSource mySound;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = this.GetComponent<Animator>();
        mySound = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        playerStepLocation = player.GetComponent<Footsteps>().stepLocation;

        if ((Vector3.Distance(player.transform.position, this.transform.position) < 3))
        {
            myAnimator.SetBool("IsWalking", true);
            transform.LookAt(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z));
            transform.Translate(Vector3.forward * (movementSpeedMultiplier / 2) * Time.deltaTime);
        }
        else if ((Vector3.Distance(player.transform.position, this.transform.position) > 3))
        {
            if (Vector3.Distance(new Vector3(playerStepLocation.x, this.transform.position.y, player.transform.position.z), this.transform.position) > 2)
            {
                myAnimator.SetBool("IsWalking", true);
                transform.LookAt(new Vector3(playerStepLocation.x, this.transform.position.y, player.transform.position.z));
                transform.Translate(Vector3.forward * movementSpeedMultiplier * Time.deltaTime);
            }
            else
            {
                myAnimator.SetBool("IsWalking", false);
            }

        }

        if (myAnimator.GetBool("IsWalking") == true && mySound.isPlaying == false)
        {
            mySound.Play();
        }
        else if (myAnimator.GetBool("IsWalking") == false && mySound.isPlaying == true)
        {
            mySound.Stop();
        }
    }
}
