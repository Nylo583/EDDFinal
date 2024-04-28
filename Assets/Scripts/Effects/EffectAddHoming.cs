using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAddHoming : Effect
{
    int num;
    float damage;
    GameObject pBullet;
    public override void Init(float[] args) {
        num = (int)args[0];
        damage = args[1];
        pBullet = Resources.Load("Prefabs/HomingBullet") as GameObject;
    }

    public override void OnBulletSpawn(GameObject target) {
        if (target.name == "BaseBullet(Clone)") {
            float dir = target.GetComponent<Bullet>().directionRads;
            int mod = 1;
            float inc = .1f;
            float add = inc;
            BaseBullet bb = target.GetComponent<BaseBullet>();
            for (int i = 0; i < num; i++) {
                GameObject g = Instantiate(pBullet, target.transform.position, new Quaternion(), GameObject.Find("BulletContainer").transform);

                g.GetComponent<Bullet>().Init(dir + add * mod, bb.speed, bb.damage * damage, bb.effectContainer);

                if (mod == 1) {
                    mod *= -1;
                } else {
                    add += inc;
                    mod *= -1;
                }
            }
        }
    }

    public override bool Consolidate(float[] args) {
        num += (int)args[0];
        damage += args[1];
        return true;
    }
}
