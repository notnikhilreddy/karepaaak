using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    public bool Human, Moving, Attacking;
    public float leftPoint, rightPoint, speed;
    private float vel;
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        vel = speed;
    }
    // Update is called once per frame
    void Update() {
        float pos = transform.position.x;
        // Debug.Log(pos);
        if(pos <= leftPoint) {
            vel = speed;
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        if(pos >= rightPoint) {
            vel = -speed;
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(vel, rb.velocity.y);
    }
}
