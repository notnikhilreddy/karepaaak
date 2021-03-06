﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float enemyMaxHealth;
    public Slider enemySlider;
    private float currentHealth;
    void Start()
    {
        currentHealth = enemyMaxHealth;
        enemySlider.gameObject.SetActive(false);
        enemySlider.maxValue = currentHealth;
        enemySlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x >= 0f) {
            enemySlider.SetDirection(Slider.Direction.LeftToRight, false);
        } else {
            enemySlider.SetDirection(Slider.Direction.RightToLeft, false);
        }
        if (currentHealth <= 0) makeDead();
    }

    public void addDamage(float damage)
    {
        if (damage <= 0) return;
        enemySlider.gameObject.SetActive(true);
        currentHealth -= damage;
        enemySlider.value = currentHealth;
    }

    void makeDead()
    {
        Destroy(gameObject);
    }
}
