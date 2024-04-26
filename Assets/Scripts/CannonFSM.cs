using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CannonFSM : MonoBehaviour
{
    private float patrolRadius;

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

    [SerializeField]
    float fireDelay;

    [SerializeField]
    float fireTimeout;

    [SerializeField]
    float bulletSpeed;

    [SerializeField]
    float bulletDmg;

    private GameObject player, totem, closest;
    private float theta;
    private bool isAtRadius;

    private void Start() {
        //UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        canPatrol = true;
        canShoot = false;
        isShooting = false;
        rb = GetComponent<Rigidbody2D>();
        sr = rb.GetComponent<SpriteRenderer>();
        patrolRadius = UnityEngine.Random.Range(25, 35);
        player = GameObject.Find("Player");
        totem = GameObject.Find("Totem");
        closest = UnityEngine.Random.Range(0, 2) == 0 ? player : totem;
        theta = 0f;
    }

    private void Update() {
        //closest = GetClosest();
        if (canPatrol) { Patrol(); }
        if (canShoot && !isShooting) { StartCoroutine(Shoot()); }
        transform.up = -1 * (closest.transform.position - this.transform.position);
    }

    GameObject GetClosest() {
        float playerDist = Vector3.Distance(this.transform.position, player.transform.position);
        float totemDist = Vector3.Distance(this.transform.position, totem.transform.position);
        return playerDist > totemDist ? player : totem;
    }

    void Patrol() {
        float distance = Vector2.Distance(this.transform.position, closest.transform.position);
        isAtRadius = Mathf.Abs(distance - patrolRadius) <= 5f;
        //Debug.Log(distance - patrolRadius);
        Vector3 targetDir, targetVelocity;

        
        if (isAtRadius) {
            canShoot = true;
            float mult = Vector2.Distance(this.transform.position, closest.transform.position) > patrolRadius ? 1 : -1;
            targetDir = mult * (closest.transform.position - this.transform.position).normalized;
            //targetVelocity = targetDir * patrolSpeed / 3;
            //Debug.DrawRay(this.transform.position, targetVelocity, Color.blue, .25f);
            theta = Mathf.Atan2(this.transform.position.y - closest.transform.position.y,
                this.transform.position.x - closest.transform.position.x) + .02f;
            //Debug.Log(theta);
            targetDir = ((new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0) * patrolRadius) + closest.transform.position - this.transform.position).normalized;
            targetVelocity = targetDir * patrolSpeed / 1.5f;
        } else {
            if (distance < patrolRadius) {
                canShoot = true;
            }
            float mult = Vector2.Distance(this.transform.position, closest.transform.position) > patrolRadius ? 1 : -1;
            targetDir = mult * (closest.transform.position - this.transform.position).normalized;
            targetVelocity = targetDir * patrolSpeed;
        }
        rb.position += Time.deltaTime * new Vector2(targetVelocity.x, targetVelocity.y);
        //yield break;

        //Debug.DrawRay(this.transform.position, targetVelocity, Color.red, .25f);
    }


    IEnumerator Shoot() {
        sr.sprite = aggroSprite;
        rb.velocity = Vector2.zero;
        canPatrol = false;
        isShooting = true;
        yield return new WaitForSeconds(fireDelay);
        Vector3 direction = (closest.transform.position - this.transform.position).normalized;
        float rads = Mathf.Atan2(direction.y, direction.x);
        GameObject inst = Instantiate(pBullet, this.transform.position, this.transform.rotation, GameObject.Find("BulletContainer").transform);
        inst.GetComponent<EnemyBullet>().Init(rads, bulletSpeed, bulletDmg);
        rb.velocity = -direction * 45;
        while (rb.velocity.magnitude > 2f) {
            yield return new WaitForSeconds(.2f);
            rb.velocity /= 2;
        }
        rb.velocity = Vector2.zero;
        canPatrol = true;
        sr.sprite = passiveSprite;
        yield return new WaitForSeconds(fireTimeout);
        isShooting = false;
    }
}
