using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Bullet
{
    private Rigidbody2D rb;
    private GameObject effectContainer;

    public float homingRadius;
    public float homingStrength;

    public override void Init(float dirRads, float spd, float dmg, GameObject effects) {
        directionRads = dirRads;
        speed = spd;
        damage = dmg;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Mathf.Cos(directionRads) * speed, Mathf.Sin(directionRads) * speed);
        effectContainer = effects;
        OnBulletSpawn();
    }

    public void FixedUpdate() {
        Collider2D[] cols = Physics2D.OverlapCircleAll(this.transform.position, homingRadius);
        GameObject closest = null;
        foreach (Collider2D col in cols) {
            if (col.tag == "Enemy") {
                if (closest != null) {
                    if (Vector2.Distance(this.transform.position, col.transform.position) <
                    Vector2.Distance(this.transform.position, closest.transform.position)) {

                        closest = col.gameObject;
                    }
                }
                else { closest = col.gameObject; }
            }
        }

        if (closest != null) {
            float theta = Mathf.Atan2(closest.transform.position.y - this.transform.position.y, closest.transform.position.x - this.transform.position.x);
            Vector2 add = (new Vector2(Mathf.Cos(theta), Mathf.Sin(theta))) * homingStrength;
            rb.velocity += add;
            rb.velocity = rb.velocity.normalized * speed;
        }
        
        

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
        foreach (Effect effect in effectContainer.GetComponents<MonoBehaviour>()) {
            if (effect.GetType() != typeof(EffectReplaceToHoming)) {
                effect.OnBulletSpawn(this.gameObject);
            }
        }
    }

    void OnBulletTravel() {
        foreach (Effect effect in effectContainer.GetComponents<MonoBehaviour>()) {
            if (effect.GetType() != typeof(EffectReplaceToHoming)) {
                effect.OnBulletTravel(this.gameObject);
            }
        }
    }

    void OnBulletHit() {
        foreach (Effect effect in effectContainer.GetComponents<MonoBehaviour>()) {
            if (effect.GetType() != typeof(EffectReplaceToHoming)) {
                effect.OnBulletHit(this.gameObject);
            }
        }
    }

    void OnBulletDestroy() {
        foreach (Effect effect in effectContainer.GetComponents<MonoBehaviour>()) {
            if (effect.GetType() != typeof(EffectReplaceToHoming)) {
                effect.OnBulletDestroy(this.gameObject);
            }
        }

        Destroy(this.gameObject);
    }
}
