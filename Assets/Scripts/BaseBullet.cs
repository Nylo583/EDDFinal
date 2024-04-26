using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : Bullet
{
    private Rigidbody2D rb;
    public GameObject effectContainer;

    public override void Init(float dirRads, float spd, float dmg, GameObject effects) {
        directionRads = dirRads;
        speed = spd;
        damage = dmg;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Mathf.Cos(directionRads)*speed, Mathf.Sin(directionRads)*speed);
        effectContainer = effects;
        OnBulletSpawn();
    }

    private void FixedUpdate() {
        OnBulletTravel();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<HealthComponent>().RemoveHealth(damage);
            OnBulletHit();
            OnBulletDestroy();
        } else if (collision.gameObject.tag == "WorldPlatform") {
            OnBulletDestroy();
        }
    }

    void OnBulletSpawn() {
        foreach(Effect effect in effectContainer.GetComponents<MonoBehaviour>()) {
            effect.OnBulletSpawn(this.gameObject);
        }
    }

    void OnBulletTravel() {
        foreach (Effect effect in effectContainer.GetComponents<MonoBehaviour>()) {
            effect.OnBulletTravel(this.gameObject);
        }
    }

    void OnBulletHit() {
        foreach (Effect effect in effectContainer.GetComponents<MonoBehaviour>()) {
            effect.OnBulletHit(this.gameObject);
        }
    }

    void OnBulletDestroy() {
        foreach (Effect effect in effectContainer.GetComponents<MonoBehaviour>()) {
            effect.OnBulletDestroy(this.gameObject);
        }

        Destroy(this.gameObject);
    }
}
