using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerController : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    Vector3 playerStepLocation;

    [SerializeField]
    float movementSpeedMultiplier;

    [SerializeField]
    float rotationSpeedMultiplier;

    float attackTimer;

    Animator myAnimator;
    AudioSource mySound;

    Health myHealth;

    [SerializeField]
    int damage;

    bool dead;

    ParticleSystem myParticles;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = this.GetComponent<Animator>();
        mySound = this.GetComponent<AudioSource>();
        myHealth = this.GetComponent<Health>();
        myParticles = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        playerStepLocation = player.GetComponent<Footsteps>().stepLocation;


        if (myHealth.healthPoints > 0)
        {
            ChasePlayer();



            if (myAnimator.GetBool("IsWalking") == true && mySound.isPlaying == false)
            {
                mySound.Play();
            }
            else if (myAnimator.GetBool("IsWalking") == false && mySound.isPlaying == true)
            {
                mySound.Stop();
            }


            //while player is in range, attack every second
            if (this.gameObject.GetComponent<Collider>().bounds.Intersects(player.GetComponent<Collider>().bounds))
            {
                attackTimer += Time.deltaTime;
                if (attackTimer > 1)
                {
                    Attack(player);
                }
            }


            //blood effect plays when health is low
            if (myHealth.healthPoints <= myHealth.maxHealth / 2)
            {
                myParticles.Play();
            }
        }
        else
        {
            if (myHealth.healthPoints <= 0 && dead == false)
            {
                Die();
            }
        }






    }

    //locate and follow player
    void ChasePlayer()
    {
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
    }

    void Die()
    {
        dead = true;
        myParticles.Stop();
        myAnimator.SetTrigger("Die");
        GameManager.gmInstance.EndGame(true);

    }
    void Attack(GameObject target)
    {
        myAnimator.SetTrigger("Attack");
        target.GetComponent<Health>().TakeDamage(damage);
        attackTimer = 0;
        
    }
}
