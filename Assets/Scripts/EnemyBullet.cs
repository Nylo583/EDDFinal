using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float directionRads;
    private float speed;
    public float damage;
    private Rigidbody2D rb;
    private GameObject effectContainer;

    public void Init(float dirRads, float spd, float dmg) {
        directionRads = dirRads;
        speed = spd;
        damage = dmg;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Mathf.Cos(directionRads) * speed, Mathf.Sin(directionRads) * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Totem") {
            collision.gameObject.GetComponent<HealthComponent>().RemoveHealth(damage);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "WorldPlatform") {
            Destroy(this.gameObject);
        }
    }
}
