using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private bool isGrounded;
    public float playerSpeed, jumpForce;
    public LayerMask wallsLayer;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        // isGrounded = true;
    }

    // Update is called once per frame
    void Update() {
        isGrounded = rb.IsTouchingLayers(wallsLayer);
        if(Input.GetAxis("horizontal") != 0) {
            rb.velocity = new Vector2(playerSpeed * Input.GetAxis("horizontal"), rb.velocity.y);
        }
        if(Input.GetAxis("vertical") > 0 && isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void FixedUpdate() {
        
    }
}
