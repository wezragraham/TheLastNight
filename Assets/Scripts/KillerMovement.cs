using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerMovement : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z));
    }
}
