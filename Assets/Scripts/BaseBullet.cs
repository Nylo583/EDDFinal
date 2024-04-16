using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{

    private float directionRads;
    private float speed;
    public float damage;

    private Rigidbody2D rb;
    private GameObject effectContainer;

    public void Init(float dirRads, float spd, float dmg, GameObject effects) {
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
            Destroy(this.gameObject);
        } else if (collision.gameObject.tag == "Platform") {
            OnBulletDestroy();
            Destroy(this.gameObject);
        }
    }

    void OnBulletSpawn() {
        foreach(EffectInterface effect in effectContainer.GetComponents<MonoBehaviour>()) {
            effect.OnBulletSpawn(this.gameObject);
        }
    }

    void OnBulletTravel() {
        foreach (EffectInterface effect in effectContainer.GetComponents<MonoBehaviour>()) {
            effect.OnBulletTravel(this.gameObject);
        }
    }

    void OnBulletHit() {
        foreach (EffectInterface effect in effectContainer.GetComponents<MonoBehaviour>()) {
            effect.OnBulletHit(this.gameObject);
        }
    }

    void OnBulletDestroy() {
        foreach (EffectInterface effect in effectContainer.GetComponents<MonoBehaviour>()) {
            effect.OnBulletDestroy(this.gameObject);
        }
    }
}
