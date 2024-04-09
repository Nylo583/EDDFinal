using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    //states
    public bool isMoveable, isDamageable, isInAir;

    [SerializeField]
    float walkSpeed;

    [SerializeField]
    [Range(0f, 1f)]
    float deadzone;

    [SerializeField]
    float jumpImpulse;

    [SerializeField]
    [Range(1, 5)]
    int maxJumps;

    private int currentJumps;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isMoveable = true;
        isDamageable = true;
        isInAir = false;
        currentJumps = 0;
    }

    void Update() {
        Jump();
        ModulateGravity();
    }

    void FixedUpdate()
    {
        Walk();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag.Equals("Platform")) {
            isInAir = false;
            currentJumps = maxJumps;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag.Equals("Platform")) {
            isInAir = true;
        }
    }

    private void Walk() {
        float hInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(hInput) > deadzone && isMoveable) {
            rb.velocity = new Vector2(hInput * walkSpeed, rb.velocity.y);
        } else if (Mathf.Abs(hInput) <= deadzone) {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void Jump() {

        if (Input.GetKeyDown(KeyCode.Space) && (!isInAir || currentJumps > 0)) {
            currentJumps--;

            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    private void ModulateGravity() {
        if (isInAir) { 
            if (Input.GetKey(KeyCode.Space) && rb.velocity.y >= 0.2f) {
                rb.gravityScale = 1;
            } else {
                rb.gravityScale = 2.25F;
            }
        }
    }
}
