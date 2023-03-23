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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerStepLocation = player.GetComponent<Footsteps>().stepLocation;

        if ((Vector3.Distance(player.transform.position, this.transform.position) < 3))
        {
            transform.LookAt(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z));
            transform.Translate(Vector3.forward * (movementSpeedMultiplier / 2) * Time.deltaTime);
        }
        else if ((Vector3.Distance(player.transform.position, this.transform.position) > 3))
        {
            if (Vector3.Distance(new Vector3(playerStepLocation.x, this.transform.position.y, player.transform.position.z), this.transform.position) > 2)
            {
                transform.LookAt(new Vector3(playerStepLocation.x, this.transform.position.y, player.transform.position.z));
                transform.Translate(Vector3.forward * movementSpeedMultiplier * Time.deltaTime);
            }
            else
            {
                transform.Rotate(Vector3.up * rotationSpeedMultiplier * Time.deltaTime);
            }

        }


    }
}
