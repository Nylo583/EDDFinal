using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectReplaceToHoming : Effect
{
    public float damage;
    public GameObject pBullet;
    public override void Init(float[] args) {
        damage = args[0];
        pBullet = Resources.Load("Prefabs/HomingBullet") as GameObject;
    }

    public override void OnBulletSpawn(GameObject target) {
        if (target.name == "BaseBullet(Clone)") {
            GameObject t = Instantiate(pBullet, target.transform.position, new Quaternion(), GameObject.Find("BulletContainer").transform);
            BaseBullet bb = target.GetComponent<BaseBullet>();
            t.GetComponent<HomingBullet>().Init(bb.directionRads, bb.speed, bb.damage * damage, bb.effectContainer);

            Destroy(target);
        }
        
    }

    public override bool Consolidate(float[] args) {
        damage += args[0];
        return true;
    }
}
