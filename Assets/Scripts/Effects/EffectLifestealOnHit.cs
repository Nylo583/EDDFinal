using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLifestealOnHit : MonoBehaviour, EffectInterface
{
    public float pctHeal;

    public void Init(params object[] args) {
        pctHeal = (float)args[0];
    }
    public void OnBulletSpawn(GameObject target) { }
    public void OnBulletTravel(GameObject target) { }
    public void OnBulletHit(GameObject target) {
        float heal = pctHeal * target.gameObject.GetComponent<BaseBullet>().damage;
        GameObject.Find("Player").GetComponent<HealthComponent>().AddHealth(heal);
    }
    public void OnBulletDestroy(GameObject target) {
        OnBulletHit(target); //testing purposes
    }
}
