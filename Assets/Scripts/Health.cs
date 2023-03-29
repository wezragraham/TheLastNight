using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int healthPoints;
    public int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = healthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        healthPoints -= amount;
    }

    public void RestoreHealth()
    {
        healthPoints = maxHealth;
    }
}
