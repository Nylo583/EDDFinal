using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    float maxHealth;

    float health;

    private void Start() {
        health = maxHealth;
    }

    private void Update() {
        if (health <= 0) {
            Destroy(gameObject);
        }

        health = Mathf.Min(health, maxHealth);
    }

    public void AddHealth(float add) {
        health += add;
    }

    public void RemoveHealth(float remove) {
        health -= remove;
    }

    public float GetHealth() {
        return health;
    }
}
