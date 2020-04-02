using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float enemyMaxHealth;
    public Slider enemySlider;
    float currentHealth;
    void Start()
    {
        currentHealth = enemyMaxHealth;
        enemySlider.maxValue = currentHealth;
        enemySlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0) makeDead();
    }

    public void addDamage(float damage)
    {
        enemySlider.gameObject.SetActive(true);
        currentHealth -= damage;
    }

    void makeDead()
    {
        Destroy(gameObject);
    }
}
