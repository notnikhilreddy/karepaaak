using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float fullHealth;
    public Slider healthSlider;
    float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = fullHealth;
        healthSlider.maxValue = currentHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addDamage(float damage)
    {
        if (damage <= 0) return;
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0) makeDead();
    }
    void makeDead()
    {
        Destroy(gameObject);
    }
}
