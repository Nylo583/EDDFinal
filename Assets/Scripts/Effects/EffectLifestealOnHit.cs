using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLifestealOnHit : Effect
{
    public float pctHeal;

    public override void Init(float[] args) {
        pctHeal = (float)args[0];
    }

    public override void OnBulletHit(GameObject target) {
        float heal = pctHeal * target.gameObject.GetComponent<BaseBullet>().damage;
        GameObject.Find("Player").GetComponent<HealthComponent>().AddHealth(heal);
    }
}
