using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EffectSplitBullets : Effect
{
    public float damage;
    public float speed = 30;
    public float numBullets;
    [SerializeField]
    GameObject pMiniBullet;

    public override void Init(float[] args) {
        pMiniBullet = Resources.Load("Prefabs/MiniBullet") as GameObject;
        numBullets = float.Parse(args[0].ToString());
        damage = (float)args[1];
    }

    public override void OnBulletDestroy(GameObject target) {
        if (target.GetComponent<Bullet>().canSpawnMoreBullets) {
            Rigidbody2D targetRb = target.GetComponent<Rigidbody2D>();
            SpawnBullets(target, target.transform.position - new Vector3(targetRb.velocity.x * .025f, targetRb.velocity.y * .025f), Mathf.Atan2(targetRb.velocity.y, targetRb.velocity.x));
        }
        
    }

    public override bool Consolidate(float[] args)
    {
        this.numBullets += args[0];
        this.damage += args[1];

        return true;
    }

    private void SpawnBullets(GameObject target, Vector3 spawnPt, float snapshotRads) {
        for (int i = 0; i < numBullets; i++) {
            GameObject inst = Instantiate(pMiniBullet, spawnPt, new Quaternion(), GameObject.Find("BulletContainer").transform);
            inst.GetComponent<Bullet>().Init((2*Mathf.PI*i/numBullets) + snapshotRads + (Mathf.PI/2), speed, damage * target.gameObject.GetComponent<Bullet>().damage, this.gameObject);
        }
    }
}
