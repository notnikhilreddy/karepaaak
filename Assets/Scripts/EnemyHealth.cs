using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float enemyMaxHealth;
    float currentHealth;
    void Start()
    {
        currentHealth = enemyMaxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) makeDead();
    }

    public void addDamage(float damage)
    {
        currentHealth -= damage;
    }

    void makeDead()
    {
        Destroy(gameObject);
    }
}
