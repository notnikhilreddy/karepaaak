using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isAttacking;
    public float leftPoint, rightPoint, speed;
    private float vel;
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        vel = speed;
        if(speed == 0) {
            leftPoint = transform.position.x - 0.01f;
            rightPoint = transform.position.x + 0.01f;
        }
    }
    // Update is called once per frame
    void Update() {
        if(!isAttacking) {
            float pos = transform.position.x;

            if(pos <= leftPoint) {
                vel = speed;
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            if(pos >= rightPoint) {
                vel = -speed;
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
            rb.velocity = new Vector2(vel, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }
}

