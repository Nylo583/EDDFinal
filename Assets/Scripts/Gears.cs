using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gears : MonoBehaviour
{
    public float timer;
    public float damage;
    bool cdPlayer, cdTotem;

    private void Start() {
        cdPlayer = true;
        cdTotem = true;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Player" && cdPlayer) {
            StartCoroutine(Damage(collision.gameObject.GetComponent<HealthComponent>(), true));
        } else if (collision.tag == "Totem" && cdTotem) {
            StartCoroutine(Damage(collision.gameObject.GetComponent<HealthComponent>(), false));
        }
    }

    IEnumerator Damage(HealthComponent target, bool isPlayer) {
        if (isPlayer) { cdPlayer = false; }
        else { cdTotem = false; }
        target.RemoveHealth(damage);
        yield return new WaitForSeconds(timer);
        if (isPlayer) { cdPlayer = true; } 
        else { cdTotem = true; }
    }
}
