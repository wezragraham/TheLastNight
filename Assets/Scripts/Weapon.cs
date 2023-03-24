using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    int damage;

    bool attacking;

    Vector3 startPosition;

    float attackTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking == true)
        {
            attackTimer += Time.deltaTime;
            MoveKnife();
        }
    }

    public void Attack(GameObject target)
    {
        startPosition = this.transform.position;
        target.GetComponent<Health>().TakeDamage(damage);
        attacking = true;
    }

    void MoveKnife()
    {
        
        if (attackTimer < 0.2f)
        {
            transform.Translate(Vector3.forward * 2 * Time.deltaTime);
        }
        else if (attackTimer > 0.2f && attackTimer < 0.4f)
        {
            transform.Translate(Vector3.back * 2 * Time.deltaTime);
        }
        else if (attackTimer >= 0.4f)
        {
            attackTimer = 0;
            attacking = false;
        }
    }
}
