using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFSM : MonoBehaviour
{
    private float xBound;
    private float yLevel;

    public bool canPatrol, canShoot, isShooting;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField]
    float patrolSpeed;

    [SerializeField]
    Sprite passiveSprite;

    [SerializeField]
    Sprite aggroSprite;

    [SerializeField]
    GameObject pBullet;

    private void Start() {
        xBound = 25f;
        canPatrol = true;
        canShoot = false;
        isShooting = false;
        rb = GetComponent<Rigidbody2D>();
        sr = rb.GetComponent<SpriteRenderer>();
        yLevel = UnityEngine.Random.Range(-xBound, xBound);
    }

    private void Update() {
        if (canPatrol && !isShooting) {
            StartCoroutine(Patrol());
        }
    }

    IEnumerator Patrol() {
        rb.velocity = Vector2.zero;
        //Debug.Log("Patrolling");
        canPatrol = false;
        sr.sprite = passiveSprite;
        Vector2 destination = new Vector2(UnityEngine.Random.Range(-xBound, xBound),
            yLevel);

        while (Vector2.Distance(this.transform.position, destination) > .5f) {
            yield return new WaitForSeconds(.05f);
            Vector2 direction = new Vector2(destination.x - this.transform.position.x, yLevel - this.transform.position.y).normalized;
            direction *= patrolSpeed;
            direction.y += 6 * Mathf.Sin(this.transform.position.x / 3);
            rb.velocity = direction;
        }
        canPatrol = true;
        yield break;
    }

}
