using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField]
    float maxHealth;

    float health;

    #nullable enable
    [SerializeField]
    GameObject? healthBarAssociated;
    #nullable disable

    [SerializeField]
    float baseScoreValue = 0;
    WorldVariables wv;
    private void Start() {
        health = maxHealth;
        wv = GameObject.Find("WorldVariables").GetComponent<WorldVariables>();
    }

    private void Update() {
        if (health <= 0) {
            wv.AddScore(baseScoreValue * wv.difficulty);
            Destroy(gameObject);
        }

        health = Mathf.Min(health, maxHealth);

        if (healthBarAssociated != null) {
            healthBarAssociated.GetComponent<HealthBar>().ModulateFill(health/maxHealth);
        }
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
