using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSplitBullets : Effect
{
    public float damage;
    public float speed;
    public float numBullets;

    [SerializeField]
    float delay;

    [SerializeField]
    GameObject pMiniBullet;

    public override void Init(params object[] args) {
        damage = (float)args[0];
        speed = (float)args[1];
        numBullets = (float)args[2];
    }

    public override void OnBulletDestroy(GameObject target) {
        if (target.GetComponent<BaseBullet>().canSpawnMoreBullets) { 
            Rigidbody2D targetRb = target.GetComponent<Rigidbody2D>();
            SpawnBullets(target.transform.position - new Vector3(targetRb.velocity.x * .025f, targetRb.velocity.y * .025f), Mathf.Atan2(targetRb.velocity.y, targetRb.velocity.x));
        }
    }

    private void SpawnBullets(Vector3 spawnPt, float snapshotRads) {
        for (int i = 0; i < numBullets; i++) {
            GameObject inst = Instantiate(pMiniBullet, spawnPt, new Quaternion(), GameObject.Find("BulletContainer").transform);
            inst.GetComponent<BaseBullet>().Init((2*Mathf.PI*i/numBullets) + snapshotRads + (Mathf.PI/2), speed, damage, this.gameObject);
        }
    }
}
