﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public string shotBy;
    public float damage;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        string otherTag = other.gameObject.tag;
        if(shotBy.Equals("Player") && !otherTag.Equals("Player")) {
            if(otherTag.Equals("Enemy")) {
                other.GetComponent<EnemyHealth>().addDamage(damage);
            }
            Destroy(gameObject);
        }
        if(shotBy.Equals("Enemy") && !otherTag.Equals("Enemy")) {
            if(otherTag.Equals("Player")) {
                other.GetComponent<PlayerHealth>().addDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
