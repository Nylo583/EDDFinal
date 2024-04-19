using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BirdFSM : MonoBehaviour
{
    private Vector2 minBounds, maxBounds;

    [SerializeField]
    float patrolSpeed;

    [SerializeField]
    float dashSpeed;

    [SerializeField]
    float dashWindup;

    [SerializeField]
    float dashTime;

    [SerializeField]
    float dashTimeout;

    [SerializeField]
    Sprite patrolSprite;

    [SerializeField]
    Sprite dashSprite;

    
    public bool canPatrol, canDash, isDashing;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    #nullable enable
    private GameObject? target;
    #nullable disable

    private void Start() {
        minBounds = new Vector2(-25f, -25f);
        maxBounds = new Vector2(25f, 25f);
        canPatrol = true;
        canDash = false;
        isDashing = false;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        target = null;
    }

    private void Update() {
        //Debug.Log("canPatrol: " + canPatrol + " | canDash: " + canDash + " | isDashing: " + isDashing);
        if (canPatrol && !isDashing) {
            StartCoroutine(Patrol());
        }
    }

    /* now in trigger handler
    private void OnTriggerStay2D(Collider2D collision) {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Totem") && !isDashing) {
            Debug.Log("Hit " + collision.gameObject.tag);
            target = collision.gameObject;
            StopCoroutine(Patrol());
            StartCoroutine(Dash());
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Totem")) {
            Debug.Log("Left " + collision.gameObject.tag);
        }
    }
    */

    public void SetTargetAndDash(GameObject t) {
        target = t;
        StopAllCoroutines();
        StartCoroutine(Dash());
    }

    IEnumerator Patrol() {
        rb.velocity = Vector2.zero;
        //Debug.Log("Patrolling");
        canPatrol = false;
        sr.sprite = patrolSprite;
        Vector2 destination = new Vector2(UnityEngine.Random.Range(minBounds.x, maxBounds.x),
            UnityEngine.Random.Range(minBounds.y, maxBounds.y));
        
        while (Vector2.Distance(this.transform.position, destination) > .5f) {
            yield return new WaitForSeconds(.05f);
            Vector2 direction = (destination - new Vector2(this.transform.position.x, this.transform.position.y)).normalized;
            Vector2 add = direction * patrolSpeed;
            rb.velocity = add;
        }
        canPatrol = true;
        yield break;
    }

    IEnumerator Dash() {
        //Debug.Log("Dashing to: " + target.gameObject.tag);
        isDashing = true;
        rb.velocity = new Vector2(0, 0);
        sr.sprite = dashSprite;
        yield return new WaitForSeconds(dashWindup);
        rb.velocity = (- new Vector2(this.transform.position.x, this.transform.position.y) +
            new Vector2(target.transform.position.x, target.transform.position.y)).normalized * dashSpeed;
        yield return new WaitForSeconds(dashTime);
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(dashTimeout);
        canPatrol = true;
        isDashing = false;
        yield break;
    }
}
